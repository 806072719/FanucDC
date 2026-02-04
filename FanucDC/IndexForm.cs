using AntdUI;
using FanucDC.db;
using FanucDC.fanuc;
using FanucDC.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FanucDC
{
    public partial class IndexForm : Form
    {
        #region 字段
        private readonly string _logRootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fanucdc_logs");
        private string _currentLogFile;
        private DateTime _currentLogDate;

        private static readonly ConcurrentDictionary<string, MicrosecondTimer> _equipmentTimerDict = new();
        private static readonly ConcurrentDictionary<string, pojo.Trace> _equipmentDataDict = new();
        private static readonly ConcurrentDictionary<string, object> _collectLockDict = new();

        private string _lastSelectedIp = string.Empty;
        private AntList<Equipment> _antList;
        private static System.Timers.Timer _uiRefreshTimer;
        #endregion

        public IndexForm()
        {
            InitializeComponent();
            InitLogSystem();
            initTableColumns();
            initData();
            initShowSelect();
            startConnection();
            LogInfo("系统启动完成");
            BindEventHandler();
        }

        #region 日志系统
        private void InitLogSystem()
        {
            if (!Directory.Exists(_logRootDir))
                Directory.CreateDirectory(_logRootDir);

            _currentLogDate = DateTime.Now.Date;
            _currentLogFile = GetLogFilePath(_currentLogDate);
            ClearOldLogFiles(7);
        }

        private string GetLogFilePath(DateTime date)
            => Path.Combine(_logRootDir, $"{date:yyyy-MM-dd}.log");

        private void ClearOldLogFiles(int keepDays)
        {
            try
            {
                var now = DateTime.Now.Date;
                foreach (var file in Directory.GetFiles(_logRootDir, "*.log"))
                {
                    var fn = Path.GetFileNameWithoutExtension(file);
                    if (DateTime.TryParse(fn, out var fdate) && (now - fdate).TotalDays > keepDays)
                        File.Delete(file);
                }
            }
            catch { }
        }

        private void CheckAndSwitchLogFile()
        {
            var today = DateTime.Now.Date;
            if (today != _currentLogDate)
            {
                _currentLogDate = today;
                _currentLogFile = GetLogFilePath(today);
                ClearOldLogFiles(7);
            }
        }

        public void LogInfo(string text) => Log(text, Color.Green);
        public void LogError(string text) => Log(text, Color.Red);
        public void LogWarn(string text) => Log(text, Color.Yellow);

        private void Log(string text, Color color)
        {
            if (IsDisposed) return;
            string msg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{color.Name.ToUpper()}] {text}";

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => WriteUiLog(msg, color)));
            }
            else
            {
                WriteUiLog(msg, color);
            }

            _ = Task.Run(() =>
            {
                try
                {
                    lock (_logRootDir)
                    {
                        CheckAndSwitchLogFile();
                        File.AppendAllText(_currentLogFile, msg + Environment.NewLine, System.Text.Encoding.UTF8);
                    }
                }
                catch { }
            });
        }

        private void WriteUiLog(string msg, Color color)
        {
            if (info == null || info.IsDisposed) return;
            try
            {
                info.Items.Add(new ListViewItem(msg) { ForeColor = color });
                if (info.Items.Count > 5000)
                {
                    info.Items.Clear();
                    LogWarn("日志超量自动清空");
                }
                info.TopIndex = info.Items.Count - 1;
            }
            catch { }
        }
        #endregion

        #region 初始化
        private void initTableColumns()
        {
            table1.Columns = new ColumnCollection()
            {
                new ColumnCheck("Selected"){ Fixed = true },
                new Column("Name","设备名称",ColumnAlign.Center){ Width="200",LineBreak=true },
                new Column("Ip","IP",ColumnAlign.Center){ Width="200",LineBreak=true },
                new Column("Port","端口"){ Width="120",LineBreak=true },
                new Column("Connect","连接状态",ColumnAlign.Center),
                new Column("Ret","返回码"){ Width="80" }, // 修复错误1：改为字符串，无int转string错
                new Column("LastTime","最后采集时间",ColumnAlign.Center){ Width="160" }
            };
        }

        private void initData()
        {
            _antList = new AntList<Equipment>(16);
            string sql = "select equipment_name,equipment_ip,equipment_port from _equipment_info order by equipment_code";
            var rows = SqlServerPool.ExecuteQueryList(sql);

            if (rows != null && rows.Count > 0)
            {
                foreach (var r in rows)
                {
                    var eq = new Equipment
                    {
                        Name = r[0]?.ToString()?.Trim() ?? "",
                        Ip = r[1]?.ToString()?.Trim() ?? "",
                        Port = short.TryParse(r[2]?.ToString(), out short p) ? p : (short)8193,
                        Connect = false,
                        LastTime = DateTime.Now,
                        Ret = -1
                    };
                    _antList.Add(eq);
                    _collectLockDict.TryAdd(eq.Ip, new object());
                }
            }
            table1.Binding<Equipment>(_antList);
        }

        private void BindEventHandler()
        {
            table1.CellClick += Table1_CellClick;
        }

        private void initShowSelect()
        {
            if (_uiRefreshTimer != null)
            {
                _uiRefreshTimer.Stop();
                _uiRefreshTimer.Elapsed -= OnUiRefreshTick;
                _uiRefreshTimer.Dispose();
            }

            _uiRefreshTimer = new System.Timers.Timer(3000);
            _uiRefreshTimer.AutoReset = true;
            _uiRefreshTimer.Elapsed += OnUiRefreshTick;
            _uiRefreshTimer.Enabled = true;
        }

        private void OnUiRefreshTick(object? s, ElapsedEventArgs e)
        {
            if (IsDisposed || string.IsNullOrWhiteSpace(_lastSelectedIp)) return;
            if (_equipmentDataDict.TryGetValue(_lastSelectedIp, out var t))
                showTrace(t);
        }
        #endregion

        #region 点击行立即刷新 + 重复点击拦截（你要的功能完全保留）
        private void Table1_CellClick(object? sender, TableClickEventArgs e)
        {
            if (e.Record is not Equipment eq || e.Button != MouseButtons.Left)
                return;

            // 重复点击同一行，直接返回，不处理、不打印日志
            if (eq.Selected && eq.Ip == _lastSelectedIp)
                return;

            // 取消其他行选中
            foreach (var item in _antList)
                item.Selected = false;

            eq.Selected = true;
            string ip = eq.Ip;

            // 只有真正切换才打日志
            if (ip != _lastSelectedIp)
            {
                _lastSelectedIp = ip;
                LogInfo($"切换设备 => {ip}");
            }

            // 立即刷新右侧数据，不等定时器
            if (_equipmentDataDict.TryGetValue(ip, out var trace))
                showTrace(trace);
            else
                showTrace(null);

            table1.Refresh();
        }
        #endregion

        #region 设备采集定时器（1秒，你最初的逻辑）
        private void startConnection()
        {
            foreach (var eq in _antList)
            {
                string ip = eq.Ip;
                short port = eq.Port;

                if (_equipmentTimerDict.ContainsKey(ip)) continue;

                var timer = new MicrosecondTimer
                {
                    Interval = 1000000, // 1秒
                    Enabled = false
                };

                var timerRef = timer;
                timer.Elapsed += (s, args) =>
                {
                    if (s is not MicrosecondTimer t || !t.Enabled || !_equipmentTimerDict.TryGetValue(ip, out var exist) || exist != timerRef)
                        return;

                    if (Monitor.TryEnter(_collectLockDict[ip], 100))
                    {
                        try { TraceDataCollection(ip, port); }
                        finally { Monitor.Exit(_collectLockDict[ip]); }
                    }
                };

                if (_equipmentTimerDict.TryAdd(ip, timer))
                {
                    timer.Enabled = true;
                    LogInfo($"设备 {ip} 采集定时器启动(1s)");
                }
                else
                {
                    timer.Dispose();
                }
            }
        }
        #endregion

        #region 采集逻辑
        public void TraceDataCollection(string ip, short port)
        {
            if (string.IsNullOrWhiteSpace(ip) || port < 1) return;

            FanucH fanucH = new FanucH { h = 0 };
            try
            {
                short ret = FanucDriver.cnc_allclibhndl3(ip, (ushort)port, 5, out fanucH.h);
                if (ret == Focas1.EW_OK)
                {
                    var trace = new pojo.Trace { Ip = ip };

                    FanucDriver.ODBPRO pro = new();
                    FanucDriver.cnc_rdprgnum(fanucH.h, pro);

                    FanucDriver.ODBEXEPRG exe = new();
                    if (FanucDriver.cnc_exeprgname(fanucH.h, exe) == Focas1.EW_OK)
                        trace.ProgramName = new string(exe.name).TrimEnd('\0');

                    FanucDriver.IODBPSD_2 p6711 = new();
                    if (FanucDriver.cnc_rdparam(fanucH.h, 6711, 0, 8, p6711) == Focas1.EW_OK)
                        trace.CurrentCount = p6711.rdata.prm_val;

                    FanucDriver.IODBPSD_2 p6712 = new();
                    if (FanucDriver.cnc_rdparam(fanucH.h, 6712, 0, 8, p6712) == Focas1.EW_OK)
                        trace.TotalCount = p6712.rdata.prm_val;

                    FanucDriver.IODBPSD_1 p6750 = new();
                    if (FanucDriver.cnc_rdparam(fanucH.h, 6750, 0, 40, p6750) == Focas1.EW_OK)
                        trace.OpenTime = p6750.ldata * 60;

                    FanucDriver.IODBPSD_1 p6751 = new(), p6752 = new();
                    if (FanucDriver.cnc_rdparam(fanucH.h, 6751, 0, 8, p6751) == Focas1.EW_OK &&
                        FanucDriver.cnc_rdparam(fanucH.h, 6752, 0, 8, p6752) == Focas1.EW_OK)
                    {
                        int sec = (int)(p6751.ldata / 1000);
                        trace.RunTime = p6752.ldata * 60 + sec;
                    }

                    FanucDriver.IODBPSD_1 param6755 = new FanucDriver.IODBPSD_1();
                    FanucDriver.IODBPSD_1 param6756 = new FanucDriver.IODBPSD_1();
                    if (FanucDriver.EW_OK == FanucDriver.cnc_rdparam(fanucH.h, 6755, 0, 8, param6755) &&
                        FanucDriver.EW_OK == FanucDriver.cnc_rdparam(fanucH.h, 6756, 0, 8, param6756))
                    {
                        int workingTimeSec = (int)(param6755.ldata / 1000); // 毫秒转秒
                        trace.CircleTime = param6756.ldata * 60 + workingTimeSec; // 总单次循环时间（秒）
                    }


                    FanucDriver.ODBST st = new();
                    if (FanucDriver.cnc_statinfo(fanucH.h, st) == Focas1.EW_OK)
                    {
                        trace.Status = st.alarm != 0 ? (short)5 : st.run;
                        trace.Alarm = st.alarm;
                    }

                    bool changed = false;
                    if (_equipmentDataDict.TryGetValue(ip, out var old))
                    {
                        if (old.CurrentCount != trace.CurrentCount || old.Status != trace.Status)
                        {
                            _equipmentDataDict[ip] = trace;
                            insertTrace2DB(trace);
                            changed = true;
                        }
                    }
                    else
                    {
                        _equipmentDataDict.TryAdd(ip, trace);
                        insertTrace2DB(trace);
                        changed = true;
                    }

                    if (changed)
                        LogInfo($"{ip} 数据更新");
                }
                else
                {
                    LogError($"{ip} 连接失败 错误码:{ret}");
                }
                refreshOnline(ip, ret);
            }
            catch (Exception ex)
            {
                LogError($"{ip} 采集异常:{ex.Message}");
            }
            finally
            {
                if (fanucH.h != 0)
                {
                    try { FanucDriver.cnc_freelibhndl(fanucH.h); }
                    catch { }
                    fanucH.h = 0;
                }
            }
        }
        #endregion

        #region 数据库写入
        public void insertTrace2DB(pojo.Trace t)
        {
            var dic = new Dictionary<string, object>
            {
                {"@ProgramName", string.IsNullOrWhiteSpace(t.ProgramName) ? DBNull.Value : t.ProgramName},
                {"@Status", t.Status},
                {"@ProductNum", t.CurrentCount},
                {"@Ip", t.Ip}
            };
            CustomThreadPool.QueueWorkItem(DoDbWork, dic);
        }

        private static void DoDbWork(object state)
        {
            if (state is not Dictionary<string, object> p) return;
            try
            {
                string sql = "INSERT INTO dbo._trace_info(program_name,status,product_num,ip) VALUES(@ProgramName,@Status,@ProductNum,@Ip)";
                SqlServerPool.ExecuteNonQuery(sql, p);
            }
            catch { }
        }
        #endregion

        #region UI刷新
        private void refreshOnline(string ip, int ret)
        {
            if (IsDisposed || table1.IsDisposed) return;

            if (table1.InvokeRequired)
            {
                table1.Invoke(new Action<string, int>(refreshOnline), ip, ret);
                return;
            }

            foreach (var eq in _antList)
            {
                if (eq.Ip == ip)
                {
                    eq.Connect = ret == Focas1.EW_OK;
                    eq.LastTime = DateTime.Now;
                    eq.Ret = (short)ret;
                    table1.Refresh();
                    break;
                }
            }
        }

        private void showTrace(pojo.Trace? t)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                Invoke(new Action<pojo.Trace?>(showTrace), t);
                return;
            }

            ipText.Text = t?.Ip ?? "";
            programText.Text = t?.ProgramName ?? "";
            currentText.Text = t?.CurrentCount.ToString() ?? "";
            totalText.Text = t?.TotalCount.ToString() ?? "";
            openText.Text = t?.OpenTime.ToString() ?? "";
            runText.Text = t?.RunTime.ToString() ?? "";
            circleText.Text = t?.CircleTime.ToString() ?? "";
            statusText.Text = t?.Status switch
            {
                0 => "空闲",
                1 => "运行",
                2 => "暂停",
                3 => "MDI",
                4 => "编辑",
                5 => "报警",
                _ => t?.Status.ToString() ?? ""
            };
            alarmText.Text = t?.Alarm.ToString() ?? "0";
        }
        #endregion

        #region 新增设备
        private void addBtn_Click(object sender, EventArgs e)
        {
            AddForm frm = new AddForm();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            if (!frm.isOk || frm.equipment == null) return;

            var eq = frm.equipment;
            eq.Connect = false;
            eq.LastTime = DateTime.Now;
            eq.Ret = -1;
            _antList.Add(eq);
            _collectLockDict.TryAdd(eq.Ip, new object());
            table1.Refresh();

            if (_equipmentTimerDict.ContainsKey(eq.Ip))
            {
                LogWarn($"{eq.Ip} 已存在");
                return;
            }

            var timer = new MicrosecondTimer { Interval = 1000000, Enabled = false };
            string ip = eq.Ip;
            short port = eq.Port;
            var tRef = timer;

            timer.Elapsed += (s, args) =>
            {
                if (s is not MicrosecondTimer t || !t.Enabled || !_equipmentTimerDict.TryGetValue(ip, out var exist) || exist != tRef)
                    return;

                if (Monitor.TryEnter(_collectLockDict[ip], 100))
                {
                    try { TraceDataCollection(ip, port); }
                    finally { Monitor.Exit(_collectLockDict[ip]); }
                }
            };

            if (_equipmentTimerDict.TryAdd(ip, timer))
            {
                timer.Enabled = true;
                LogInfo($"新增设备 {ip} 定时器启动(1s)");
            }
            else
            {
                timer.Dispose();
            }
        }
        #endregion

        #region 删除设备（回归你最初写法：只Disable+Dispose，绝对不碰事件 -=，彻底无CS0070）
        private void delBtn_Click(object sender, EventArgs e)
        {
            bool hasSel = _antList.Any(a => a.Selected);
            if (!hasSel)
            {
                // 修复错误3：命名冲突，加全命名空间，无歧义
                AntdUI.Message.warn(this, "请选择设备", autoClose: 2);
                return;
            }

            if (MessageBox.Show("确认删除？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            for (int i = _antList.Count - 1; i >= 0; i--)
            {
                var eq = _antList[i];
                if (!eq.Selected) continue;

                string ip = eq.Ip;
                try
                {
                    // 回归你最初的写法：只禁用、释放，不操作事件！！！完全不碰Elapsed，0错误
                    if (_equipmentTimerDict.TryRemove(ip, out var t))
                    {
                        t.Enabled = false;
                        t.Dispose();
                    }

                    _collectLockDict.TryRemove(ip, out _);
                    _equipmentDataDict.TryRemove(ip, out _);

                    if (_lastSelectedIp == ip)
                    {
                        _lastSelectedIp = string.Empty;
                        showTrace(null);
                    }

                    string sql = "DELETE FROM _equipment_info WHERE equipment_ip=@Ip";
                    var p = new Dictionary<string, object> { { "@Ip", ip } };
                    SqlServerPool.ExecuteNonQuery(sql, p);

                    _antList.RemoveAt(i);
                    LogInfo($"已删除 {ip}");
                }
                catch (Exception ex)
                {
                    LogError($"删除失败 {ip}:{ex.Message}");
                }
            }
            table1.Refresh();
            // 修复错误3
            AntdUI.Message.success(this, "删除完成", autoClose: 1);
        }
        #endregion

        #region 释放资源（同样回归最初写法：只禁用+释放，不操作事件）
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_uiRefreshTimer != null)
                {
                    _uiRefreshTimer.Stop();
                    _uiRefreshTimer.Elapsed -= OnUiRefreshTick;
                    _uiRefreshTimer.Dispose();
                }

                // 完全按你最初的写法：只禁用、Dispose，绝对不操作Elapsed事件，无任何报错
                foreach (var kv in _equipmentTimerDict)
                {
                    try
                    {
                        kv.Value.Enabled = false;
                        kv.Value.Dispose();
                    }
                    catch { }
                }

                _equipmentTimerDict.Clear();
                _equipmentDataDict.Clear();
                _collectLockDict.Clear();
                components?.Dispose();
            }
            LogInfo("系统已退出");
            base.Dispose(disposing);
        }
        #endregion

        #region 头部菜单-退出事件（新增）
        /// <summary>
        /// 帮助->退出 点击事件：释放所有资源并关闭窗体
        /// </summary>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 释放UI兜底定时器
                if (_uiRefreshTimer != null)
                {
                    _uiRefreshTimer.Stop();
                    _uiRefreshTimer.Elapsed -= OnUiRefreshTick;
                    _uiRefreshTimer.Dispose();
                    _uiRefreshTimer = null;
                }

                // 2. 释放所有设备采集定时器（核心资源）
                foreach (var kv in _equipmentTimerDict)
                {
                    kv.Value.Enabled = false;
                    kv.Value.Dispose();
                }
                _equipmentTimerDict.Clear();
                _collectLockDict.Clear();
                _equipmentDataDict.Clear();

                // 3. 释放窗体组件资源
                components?.Dispose();

                LogInfo("系统手动退出，所有资源已释放");
            }
            catch (Exception ex)
            {
                LogError("退出时释放资源异常：" + ex.Message);
            }
            finally
            {
                // 最终关闭窗体
                this.Close();
            }
        }
        #endregion

        #region 空方法
        private void Checkbox_rowstyle_CheckedChanged(object sender, BoolEventArgs e) { }
        private void Checkbox_CheckedChanged(object sender, BoolEventArgs e) { }
        private AntdUI.Table.CellStyleInfo table1_SetRowStyle(object sender, TableSetRowStyleEventArgs e) => null;
        private void ButtonADD_Click(object sender, EventArgs e) { }
        private void table1_CellButtonClick(object sender, TableButtonEventArgs e) { }
        private void ButtonDEL_Click(object sender, EventArgs e) { }
        #endregion

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
using AntdUI;
using FanucDC.fanuc;
using FanucDC.Models;
using FanucDC.pojo;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FanucDC
{
    public partial class IndexForm : Form
    {
        private string files = "D:\\logs\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
        private static ConcurrentDictionary<string, MicrosecondTimer> EQUIPMENT_TIMER_DICT = new ConcurrentDictionary<string, MicrosecondTimer>(); // 设备异常
        private static ConcurrentDictionary<string, Trace> EQUIPMENT_DATA_DICT = new ConcurrentDictionary<string,Trace>(); // 设备数据

        private string lastIp;
        
        AntList<Equipment> antList;

        private static System.Timers.Timer refreshTimer;

        public IndexForm()
        {
            InitializeComponent();
            initTableColumns();
            initData();
            initShowSelect();
            startConnection();
            BindEventHandler();
        }

        private void startConnection()
        {
            foreach (var equipment in antList)
            {
                var tim = new MicrosecondTimer();

                var ip = equipment.Ip;
                var port = equipment.Port;

                LogInfo(ip + " " + port + " 开始连接");

                tim.Interval = 1000;
                tim.Elapsed += (sender, e) =>
                {
                    TraceDataCollection(ip,port);
                };
                tim.Enabled = true;

                EQUIPMENT_TIMER_DICT.TryAdd(ip, tim);
            }
        }

        private void TraceDataCollection(string ip,short port)
        {
            var fanucH = new FanucH();
            short ret = FanucDriver.cnc_allclibhndl3(ip, Convert.ToUInt16(port), 5, out fanucH.h);
            try
            {
                if (ret == FanucDriver.EW_OK)
                {
                    pojo.Trace trace = new pojo.Trace();

                    FanucDriver.ODBPRO dbpro = new FanucDriver.ODBPRO();
                    if (FanucDriver.EW_OK == FanucDriver.cnc_rdprgnum(fanucH.h, dbpro))
                    {
                        short Mainpg = dbpro.mdata;//主程序号
                        short Currentpg = dbpro.data;//当前运行程序号（子程序号）
                    }

                    // 程序名称
                    FanucDriver.ODBEXEPRG buf = new FanucDriver.ODBEXEPRG();
                    ret = FanucDriver.cnc_exeprgname(fanucH.h, buf);
                    if (ret == FanucDriver.EW_OK)
                    {
                        trace.ProgramName = new string(buf.name);
                    }

                    // 获取产量
                    FanucDriver.IODBPSD_2 param6711 = new FanucDriver.IODBPSD_2();
                    ret = FanucDriver.cnc_rdparam(fanucH.h, 6711, 0, 8, param6711);
                    if (ret == FanucDriver.EW_OK)
                    {
                        trace.CurrentCount = param6711.rdata.prm_val;
                    }
                    // 全部产量
                    FanucDriver.IODBPSD_2 param6712 = new FanucDriver.IODBPSD_2();
                    ret = FanucDriver.cnc_rdparam(fanucH.h, 6712, 0, 8, param6712);
                    if (ret == FanucDriver.EW_OK)
                    {
                        trace.TotalCount = param6712.rdata.prm_val;
                    }
                    // 开机时间
                    FanucDriver.IODBPSD_1 param6750 = new FanucDriver.IODBPSD_1();
                    ret = FanucDriver.cnc_rdparam(fanucH.h, 6750, 0, 8 + 32, param6750);
                    if (ret == FanucDriver.EW_OK)
                    {
                        int PoweOnTime = param6750.ldata * 60;
                        trace.OpenTime = PoweOnTime;
                    }
                    // 生产时间
                    FanucDriver.IODBPSD_1 param6751 = new FanucDriver.IODBPSD_1();
                    FanucDriver.IODBPSD_1 param6752 = new FanucDriver.IODBPSD_1();
                    ret = FanucDriver.cnc_rdparam(fanucH.h, 6751, 0, 8, param6751);
                    if (ret == FanucDriver.EW_OK)
                    {
                        int workingTimeSec = param6751.ldata / 1000;
                        ret = FanucDriver.cnc_rdparam(fanucH.h, 6752, 0, 8, param6752);
                        if (ret == FanucDriver.EW_OK)
                        {
                            int workingTimeMin = param6752.ldata;
                            int CycSec = workingTimeMin * 60 + workingTimeSec;
                            trace.RunTime = CycSec;
                        }
                    }

                    // 循环时间
                    FanucDriver.IODBPSD_1 param6757 = new FanucDriver.IODBPSD_1();
                    FanucDriver.IODBPSD_1 param6758 = new FanucDriver.IODBPSD_1();
                    ret = FanucDriver.cnc_rdparam(fanucH.h, 6757, 0, 8, param6757);
                    if (ret == FanucDriver.EW_OK)
                    {
                        int workingTimeSec = param6757.ldata / 1000;
                        ret = FanucDriver.cnc_rdparam(fanucH.h, 6758, 0, 8, param6758);
                        if (ret == FanucDriver.EW_OK)
                        {
                            int workingTimeMin = param6758.ldata;
                            int CycSec = workingTimeMin * 60 + workingTimeSec;
                            trace.CircleTime = CycSec;
                        }
                    }

                    // 程序状态
                    FanucDriver.ODBST statinfo = new FanucDriver.ODBST();
                    ret = FanucDriver.cnc_statinfo(fanucH.h, statinfo);
                    if (ret == FanucDriver.EW_OK)
                    {
                        short run = statinfo.run;
                        short Alarm = statinfo.alarm;
                        //MTMode = statinfo.tmmode;
                        if (Alarm != 0)
                            run = 5;//5为设备报警状态

                        trace.Status = run;
                        trace.Alarm = Alarm;
                    }

                    trace.Ip = ip;
                    EQUIPMENT_DATA_DICT[ip] = trace;
                    int status = 0;
                    pojo.Trace value = null;
                    if (EQUIPMENT_DATA_DICT.TryGetValue(ip, out value))
                    {
                        // 说明生产数量没有变化
                        if (value.CurrentCount.CompareTo(trace.CurrentCount) == 0)
                        {
                        }
                        else
                        {
                            EQUIPMENT_DATA_DICT[ip] = trace;
                        }
                        if (value.Status != trace.Status)
                        {
                            LogInfo("状态发生变化 之前" + value.Status + "现在 " + trace.Status);
                        }
                    }
                    else
                    {
                        EQUIPMENT_DATA_DICT.TryAdd(ip, trace);
                    }

                    



                    // 如果在线设备包括本IP
                    refreshOnline(ip, ret);
                }
                else
                {
                    LogError($" [{Thread.CurrentThread.ManagedThreadId}] {ip} 异常代码： {ret} ");
                    refreshOnline(ip, ret);
                 
                }
            }
            finally
            {
                FanucDriver.cnc_freelibhndl(fanucH.h);
            }
        }

        private void refreshOnline(string ip,int ret)
        {
            foreach (var equipment in antList)
            {
                if (equipment.Ip.Equals(ip))
                {
                    equipment.Connect = (ret == 0);
                    equipment.LastTime = DateTime.Now;
                    equipment.Ret = ret;
                }
            }
        }


        public void initShowSelect()
        {
            refreshTimer = new System.Timers.Timer(300);
            // 设置Elapsed事件处理程序
            refreshTimer.Elapsed += refreshTraceForm;
            // 启用定时器
            refreshTimer.Enabled = true;
        }

        private void refreshTraceForm(object? sender, ElapsedEventArgs e)
        {
            if (lastIp == null || "".Equals(lastIp))
            {
                return;
            }
            Trace outVal = null;
            EQUIPMENT_DATA_DICT.TryGetValue(lastIp, out outVal);
            if (outVal != null)
            {
                showTrace(outVal);
            }
            else
            {
                //LogError("您选中的数据追溯不存在");
                showTrace(null);
            }
        }

        private void showTrace(Trace outVal)
        {
            if (this.IsDisposed)
            {
                return;
            }
            if (this.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new Action(() => {
                    showTrace(outVal);
                }));
                return;
            }
            if (outVal == null)
            {
                ipText.Text = "";
                programText.Text = "";
                currentText.Text = "";
                totalText.Text = "";
                openText.Text = "";
                runText.Text = "";
                circleText.Text = "";
                statusText.Text = "";
                alarmText.Text = "";
            } else
            {
                ipText.Text = outVal.Ip;
                programText.Text = outVal.ProgramName;
                currentText.Text = outVal.CurrentCount.ToString();
                totalText.Text = outVal.TotalCount.ToString();
                openText.Text = outVal.OpenTime.ToString();
                runText.Text = outVal.RunTime.ToString();
                circleText.Text = outVal.CircleTime.ToString();
                statusText.Text = outVal.Status.ToString();
                alarmText.Text = outVal.Alarm.ToString();
            }


        }

        public void LogInfo(string text)
        {
            Log(text, Color.Green);
        }

        public void LogError(string text)
        {
            Log(text, Color.Red);
        }

        public void LogWarn(string text)
        {
            Log(text, Color.Yellow);
        }

        private void Log(string text, Color color)
        {
            if (this.IsDisposed)
            {
                return;
            }
            if (this.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new Action(() => {
                    Log(text, color);
                }));
                return;
            }
            // 获取消息写日志
            string message = DateTime.Now.ToString() + " " + text;
            this.info.Items.Add(message);
            string logFilePath = files;
            if (!System.IO.File.Exists(logFilePath))
            {
                var stream = File.Create(files);
                stream.Close();
                stream.Dispose();
            }

            using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine(message);
            }
            // 如果大于5000清空
            if (this.info.Items.Count > 5000)
            {
                this.info.Items.Clear();
            }
        }


        private void initTableColumns()
        {
            table1.Columns = new ColumnCollection() {
                new ColumnCheck("Selected"){Fixed = true},
                new Column("Name", "设备名称", ColumnAlign.Center)
                {
                    Width = "200",
                    LineBreak = true
                },
                new Column("Ip", "设备IP", ColumnAlign.Center)
                {
                    Width = "200",
                    LineBreak = true
                },
                new Column("Port", "设备端口"){
                    Width = "120",
                    LineBreak = true,
                },
                new Column("Connect", "连接状态",ColumnAlign.Center),
                new Column("Ret", "连接返回代码",ColumnAlign.Center)
                {
                    Width = "80",
                },
                new Column("LastTime", "上次连接/读取时间",ColumnAlign.Center)
                {
                    Width = "150",
                },
                //new Column("CellText", "富文本")
                //{
                //    ColAlign = ColumnAlign.Center,//支持表头位置单独设置
                //},
                //new Column("CellProgress", "进度条",ColumnAlign.Center),
            };
        }

        private void BindEventHandler()
        {
            //buttonADD.Click += ButtonADD_Click;
            //buttonDEL.Click += ButtonDEL_Click;

            //checkbox_border.CheckedChanged += Checkbox_CheckedChanged;
            //checkbox_columndragsort.CheckedChanged += Checkbox_CheckedChanged;
            //checkbox_fixheader.CheckedChanged += Checkbox_CheckedChanged;
            //checkbox_rowstyle.CheckedChanged += Checkbox_rowstyle_CheckedChanged;
            //checkbox_sort.CheckedChanged += Checkbox_CheckedChanged;
            //checkbox_visibleheader.CheckedChanged += Checkbox_CheckedChanged;

            //table1.CellClick += table1_CellClick;
            //table1.CellButtonClick += table1_CellButtonClick;

            table1.CellClick += table1_CellClick;
        }

        private void Checkbox_rowstyle_CheckedChanged(object sender, BoolEventArgs e)
        {
            if (e.Value)
            {
                table1.SetRowStyle += table1_SetRowStyle;
                table1.Invalidate();
            }
            else
            {
                table1.SetRowStyle -= table1_SetRowStyle;
                table1.Invalidate();
            }
        }
        private void Checkbox_CheckedChanged(object sender, BoolEventArgs e)
        {
            //table1.Bordered = checkbox_border.Checked;
            //table1.ColumnDragSort = checkbox_columndragsort.Checked;
            //table1.FixedHeader = checkbox_fixheader.Checked;
            //table1.VisibleHeader = checkbox_visibleheader.Checked;

            foreach (var item in table1.Columns)
            {
                //item.SortOrder = checkbox_sort.Checked;
            }
        }

        private AntdUI.Table.CellStyleInfo table1_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                return new AntdUI.Table.CellStyleInfo
                {
                    BackColor = AntdUI.Style.Db.ErrorBg,
                };
            }
            return null;
        }

        private void ButtonADD_Click(object sender, EventArgs e)
        {
            User useradd = new User()
            {
                CellBadge = new CellBadge(TState.Processing, "测试中"),
                CellDivider = new CellDivider(),
                CellTags = new CellTag[] { new CellTag("测试", TTypeMini.Primary), new CellTag("测试", TTypeMini.Success), new CellTag("测试", TTypeMini.Warn) },
                CellText = new CellText("这是一个无图标的文本"),
                CellProgress = new CellProgress(0.5f),
                CellLinks = new CellLink[]{ new CellLink("https://gitee.com/antdui/AntdUI", "AntdUI"),
                    new CellButton(Guid.NewGuid().ToString(),"编辑",TTypeMini.Primary),
                    new CellButton(Guid.NewGuid().ToString(),"删除",TTypeMini.Error) },
            };
          

        }

        private void table1_CellClick(object sender, TableClickEventArgs e)
        {
            var record = e.Record;
            if (record is Equipment equipment)
            {
                //判断是否右键
                //if (e.Button == MouseButtons.Right)
                //{
                //    AntdUI.ContextMenuStrip.open(new AntdUI.ContextMenuStrip.Config(table1,
                //    (item) =>
                //    {
                //        if (item.Text == "开启")
                //        {
                //            user.Selected = true;
                //        }
                //        else if (item.Text == "关闭")
                //        {
                //            user.Selected = false;
                //        }
                //        else if (item.Text == "编辑")
                //        {
                         
                //        }
                //        else if (item.Text == "删除")
                //        {
                          
                //        }
                //        else
                //        {

                //        }
                //    },
                //        new IContextMenuStripItem[] {
                //            //根据行数据动态修改右键菜单
                //            user.Selected?  new ContextMenuStripItem("关闭")
                //            {
                //                IconSvg = "CloseOutlined"
                //            }:new ContextMenuStripItem("开启")
                //            {
                //                IconSvg = "CheckOutlined"
                //            },
                //            //new AntdUI.ContextMenuStripItem("编辑"){
                //            //    IconSvg = "<svg t=\"1725101535645\" class=\"icon\" viewBox=\"0 0 1024 1024\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" p-id=\"1082\" width=\"200\" height=\"200\"><path d=\"M867.22 413.07c-9.68 0-19.36-3.63-26.82-10.92-15.19-14.82-15.49-39.14-0.68-54.32 46.84-48.02 45.89-125.18-2.12-172.02-23.27-22.7-54.13-34.93-86.46-34.56-32.49 0.4-62.87 13.43-85.56 36.69-14.83 15.19-39.15 15.47-54.32 0.68-15.19-14.81-15.49-39.13-0.68-54.32C687 45.94 812.9 44.4 891.24 120.82c78.33 76.42 79.89 202.32 3.47 280.66-7.52 7.71-17.51 11.59-27.49 11.59z\" p-id=\"1083\"></path><path d=\"M819.09 462.01c-9.68 0-19.36-3.63-26.82-10.92L563.13 227.55c-15.19-14.82-15.49-39.14-0.68-54.32 14.82-15.2 39.15-15.47 54.32-0.68L845.92 396.1c15.19 14.82 15.49 39.14 0.68 54.32-7.54 7.72-17.52 11.59-27.51 11.59z\" p-id=\"1084\"></path><path d=\"M164.51 674.68c-9.68 0-19.36-3.63-26.82-10.92-15.19-14.82-15.49-39.14-0.68-54.32l473.74-485.6c14.82-15.2 39.15-15.47 54.33-0.67 15.18 14.82 15.48 39.14 0.67 54.33L192.01 663.09c-7.53 7.72-17.52 11.59-27.5 11.59z\" p-id=\"1085\"></path><path d=\"M111.34 958.62c-2.31 0-4.65-0.21-7.01-0.64-20.86-3.85-34.66-23.88-30.81-44.74l51.7-280.46c3.85-20.86 23.86-34.7 44.74-30.81 20.86 3.85 34.66 23.88 30.81 44.74l-51.7 280.46c-3.41 18.5-19.56 31.45-37.73 31.45z\" p-id=\"1086\"></path><path d=\"M393.86 898.44c-9.68 0-19.36-3.63-26.82-10.92-15.19-14.82-15.49-39.14-0.68-54.32L840.1 347.6c14.82-15.19 39.14-15.49 54.32-0.68 15.19 14.82 15.49 39.13 0.68 54.32l-473.74 485.6c-7.53 7.72-17.51 11.6-27.5 11.6z\" p-id=\"1087\"></path><path d=\"M111.3 958.66c-17.79 0-33.76-12.42-37.56-30.52-4.36-20.76 8.93-41.13 29.7-45.49l279.1-58.62c20.8-4.35 41.13 8.93 45.49 29.7 4.36 20.76-8.93 41.13-29.7 45.49l-279.1 58.62c-2.66 0.55-5.31 0.82-7.93 0.82z\" p-id=\"1088\"></path><path d=\"M912.71 959.5H592.59c-21.21 0-38.41-17.2-38.41-38.41 0-21.21 17.2-38.41 38.41-38.41h320.12c21.21 0 38.41 17.2 38.41 38.41 0 21.21-17.2 38.41-38.41 38.41z\" p-id=\"1089\"></path></svg>",
                //            //},
                //            //new AntdUI.ContextMenuStripItem("删除"){
                //            //    IconSvg = "<svg t=\"1725101558417\" class=\"icon\" viewBox=\"0 0 1024 1024\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" p-id=\"1250\" width=\"200\" height=\"200\"><path d=\"M783.72 958.39h-539c-41.75 0-75.72-33.46-75.72-74.6V242.5c0-21.18 17.17-38.36 38.36-38.36s38.36 17.17 38.36 38.36v639.17h537V242.5c0-21.18 17.17-38.36 38.36-38.36s38.36 17.17 38.36 38.36v641.29c0 41.14-33.97 74.6-75.72 74.6z\" p-id=\"1251\"></path><path d=\"M706.01 244.51c-21.19 0-38.36-17.17-38.36-38.36v-63.82H360.79v63.82c0 21.18-17.17 38.36-38.36 38.36-21.19 0-38.36-17.17-38.36-38.36v-65.93c0-41.83 27.11-74.6 61.71-74.6h336.87c34.6 0 61.71 32.77 61.71 74.6v65.93c0.01 21.18-17.16 38.36-38.35 38.36z\" p-id=\"1252\"></path><path d=\"M921.14 256.01H102.86c-21.18 0-38.36-17.17-38.36-38.36s17.17-38.36 38.36-38.36h818.29c21.19 0 38.36 17.17 38.36 38.36s-17.18 38.36-38.37 38.36zM514.22 763.27c-21.19 0-38.36-17.17-38.36-38.36V405.27c0-21.18 17.17-38.36 38.36-38.36 21.19 0 38.36 17.17 38.36 38.36v319.64c0 21.18-17.17 38.36-38.36 38.36zM360.79 699.34c-21.19 0-38.36-17.17-38.36-38.36V469.2c0-21.18 17.17-38.36 38.36-38.36s38.36 17.17 38.36 38.36v191.79c0 21.18-17.17 38.35-38.36 38.35zM667.65 699.34c-21.19 0-38.36-17.17-38.36-38.36V469.2c0-21.18 17.17-38.36 38.36-38.36 21.19 0 38.36 17.17 38.36 38.36v191.79c0 21.18-17.17 38.35-38.36 38.35z\" p-id=\"1253\"></path></svg>"
                //            //},
                //            new ContextMenuStripItemDivider(),
                //            //new AntdUI.ContextMenuStripItem("详情"){
                //            //    Sub = new IContextMenuStripItem[]{ new AntdUI.ContextMenuStripItem("打印", "Ctrl + P") { },
                //            //            new AntdUI.ContextMenuStripItem("另存为", "Ctrl + S") { } },
                //            //    IconSvg = "<svg t=\"1725101601993\" class=\"icon\" viewBox=\"0 0 1024 1024\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" p-id=\"1414\" width=\"200\" height=\"200\"><path d=\"M450.23 831.7c-164.87 0-316.85-108.51-366.94-269.68-30.4-97.82-20.9-201.62 26.76-292.29s127.79-157.35 225.6-187.75c97.83-30.42 201.61-20.9 292.29 26.76 90.67 47.67 157.35 127.79 187.75 225.61 35.78 115.12 16.24 237.58-53.6 335.99a383.494 383.494 0 0 1-43 50.66c-15.04 14.89-39.34 14.78-54.23-0.29-14.9-15.05-14.77-39.34 0.29-54.23a307.844 307.844 0 0 0 34.39-40.52c55.9-78.76 71.54-176.75 42.92-268.84-50.21-161.54-222.49-252.1-384.03-201.9-78.26 24.32-142.35 77.67-180.48 150.2-38.14 72.53-45.74 155.57-21.42 233.83 44.58 143.44 190.03 234.7 338.26 212.42 20.98-3.14 40.48 11.26 43.64 32.2 3.16 20.95-11.26 40.48-32.2 43.64a377.753 377.753 0 0 1-56 4.19z\" p-id=\"1415\"></path><path d=\"M919.84 959.5c-9.81 0-19.63-3.74-27.11-11.24L666.75 722.29c-14.98-14.97-14.98-39.25 0-54.23 14.97-14.98 39.26-14.98 54.23 0l225.97 225.97c14.98 14.97 14.98 39.25 0 54.23-7.48 7.5-17.3 11.24-27.11 11.24z\" p-id=\"1416\"></path></svg>",
                //            //}
                //        }));
                //}
                if (e.Button == MouseButtons.Left)
                {
                    equipment.Selected = !equipment.Selected;
                    if (equipment.Selected)
                    {
                        lastIp = equipment.Ip;
                    }

                    LogInfo("切换ip" + lastIp);
                }
            }
        }

        //表格内部按钮事件
        private void table1_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            var buttontext = e.Btn.Text;

            if (e.Record is User user)
            {
                switch (buttontext)
                {
                    //暂不支持进入整行编辑，只支持指定单元格编辑，推荐使用弹窗或抽屉编辑整行数据
                    case "编辑":
                        //var form = new UserEdit(window, user) { Size = new Size(500, 300) };
                        //AntdUI.Drawer.open(new AntdUI.Drawer.Config(window, form)
                        //{
                        //    OnLoad = () =>
                        //    {
                        //        AntdUI.Message.info(window, "进入编辑", autoClose: 1);
                        //    },
                        //    OnClose = () =>
                        //    {
                        //        AntdUI.Message.info(window, "结束编辑", autoClose: 1);
                        //    }
                        //});
                        break;
                    case "删除":
                        //var result = Modal.open(window, "删除警告！", "确认要删除选择的数据吗？", TType.Warn);
                        //if (result == DialogResult.OK)
                        //    antList.Remove(user);
                        break;
                    case "AntdUI":
                        //AntdUI.Message.info(window, user.CellLinks.FirstOrDefault().Id, autoClose: 1);
                        break;
                }
            }
        }

        private void ButtonDEL_Click(object sender, EventArgs e)
        {
            if (antList.Count == 0 || !antList.Any(x => x.Selected))
            {
                //AntdUI.Message.warn(window, "请选择要删除的行！", autoClose: 3);
                return;
            }
            //var result = Modal.open(window, "删除警告！", "确认要删除选择的数据吗？", TType.Warn);
            //if (result == DialogResult.OK)
            //{
            //    //使用反转for循环删除
            //    for (int i = antList.Count - 1; i >= 0; i--)
            //    {
            //        if (antList[i].Selected)
            //        {
            //            antList.Remove(antList[i]);
            //        }
            //    }
            //}
        }

        private void initData()
        {
            antList = new AntList<Equipment>(10);

            antList.Add(new Equipment
            {
                Name = "发那科设备01",
                Ip = "192.168.76.145",
                Port = 8193,
                Connect = false,
            });
            antList.Add(new Equipment
            {
                Name = "发那科设备02",
                Ip = "192.168.76.146",
                Port = 8193,
                Connect = false,
            });
            antList.Add(new Equipment
            {
                Name = "发那科设备03",
                Ip = "192.168.76.147",
                Port = 8193,
                Connect = false,
            });
            antList.Add(new Equipment
            {
                Name = "发那科设备04",
                Ip = "192.168.76.148",
                Port = 8193,
                Connect = false,
            });
            table1.Binding<Equipment>(antList);
        }
    }
}

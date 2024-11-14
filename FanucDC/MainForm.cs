using FanucDC.db;
using FanucDC.fanuc;
using FanucDC.timer;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace FanucDC
{
    public partial class MainForm : Form
    {

        private static string lastIp = "";

        private static ConcurrentDictionary<string, int> EQUIPMENT_STATUS_DICT = new ConcurrentDictionary<string, int>(); //设备名称,状态

        private static ConcurrentDictionary<string, pojo.Trace> EQUIPMENT_DATA_DICT = new ConcurrentDictionary<string, pojo.Trace>(); // 设备数据

        private static ConcurrentDictionary<string, string> EQUIPMENT_ERROR_DICT = new ConcurrentDictionary<string, string>(); // 设备异常

        private static ConcurrentDictionary<string, MicrosecondTimer> EQUIPMENT_TIMER_DICT = new ConcurrentDictionary<string, MicrosecondTimer>(); // 设备异常

        private static ConcurrentBag<string> ONLINE_EQUIPMENT = new ConcurrentBag<string>(); // 设备IP和设备状态

        private static System.Timers.Timer _timer;


        private static System.Timers.Timer refreshTimer;

        public MainForm()
        {
            InitializeComponent();
            initData();
            startConnection();
            refreshChooseTraceData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void oneCountLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        /**
         *  初始化数据
         *  
         */
        private void initData()
        {
            // 初始化时间



            _timer = new System.Timers.Timer(1000);
            // 设置Elapsed事件处理程序
            _timer.Elapsed += refreshTime;
            // 启用定时器
            _timer.Enabled = true;
            dataGridView1.Rows.Add("发那科设备1", "192.168.76.145", 8193, "");
            dataGridView1.Rows.Add("发那科设备2", "192.168.76.146", 8193, "");
            dataGridView1.Rows.Add("发那科设备3", "192.168.76.147", 8193, "");
            dataGridView1.Rows.Add("发那科设备4", "192.168.76.148", 8193, "");
            dataGridView1.Rows.Add("发那科设备5", "192.168.76.149", 8193, "");
            dataGridView1.Rows.Add("发那科设备6", "192.168.76.150", 8193, "");
            dataGridView1.Rows.Add("发那科设备7", "192.168.76.151", 8193, "");
        }



        private void startConnection()
        {
            // 初始化线程池,进行连接设备 
            // 如果连接上了进行采集
            // 主界面有个timer定时刷新数据
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                string ip = row.Cells[1].Value.ToString();
                string port = row.Cells[2].Value.ToString();
                var tim = new MicrosecondTimer();

                tim.Interval = 1000;
                tim.Elapsed += (sender, e) =>
                {
                    TraceDataCollection(ip);
                };
                tim.Enabled = true;

                EQUIPMENT_TIMER_DICT.TryAdd(ip, tim);
            }
        }

        private void info_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.info.Items.Clear();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int rowIndex = dataGridView1.CurrentRow.Index;
                string cellValue = dataGridView1.Rows[rowIndex].Cells["equipmentIp"].Value.ToString();
                if (lastIp.ToString().Equals(cellValue))
                {
                    return;
                }
                else
                {
                    lastIp = cellValue;
                }
                // 切换显示
                LogInfo(cellValue);
            }
        }

        /**
         * 读取文件
         */
        private void TraceDataCollection(string ip)
        {
            var fanucH = new FanucH();
            short ret = FanucDriver.cnc_allclibhndl3(ip, Convert.ToUInt16(8193), 5, out fanucH.h);
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
                    if (EQUIPMENT_STATUS_DICT.TryGetValue(ip, out status))
                    {
                        // 如果两次状态不相等
                        if (status.CompareTo(trace.Status) != 0)
                        {
                            LogInfo(ip + "状态变化 之前状态 " + status + " 当前状态 " + trace.Status);
                            EQUIPMENT_STATUS_DICT[ip] = trace.Status;
                        }
                    }
                    else
                    {
                        EQUIPMENT_STATUS_DICT.TryAdd(ip, trace.Status);
                    }



                   


                    pojo.Trace value = null;
                    if (EQUIPMENT_DATA_DICT.TryGetValue(ip, out value))
                    {
                        // 说明生产数量没有变化
                        if (value.CurrentCount.CompareTo(trace.CurrentCount) == 0)
                        {
                        }
                        else
                        {
                            LogInfo(trace.ToString());
                            EQUIPMENT_DATA_DICT[ip] = trace;
                        }
                    }
                    else
                    {
                        EQUIPMENT_DATA_DICT.TryAdd(ip, trace);
                    }
                 


                    // 如果在线设备包括本IP
                    if (ONLINE_EQUIPMENT.Contains(ip))
                    {
                    }
                    else
                    {
                        ONLINE_EQUIPMENT.Add(ip);
                        refreshOnline(ip, ret);
                    }
                }
                else
                {
                    LogError($" [{Thread.CurrentThread.ManagedThreadId}] {ip} 异常代码： {ret} ");
                    refreshOnline(ip, ret);
                    // 如果本设备在线
                    //if (ONLINE_EQUIPMENT.Contains(ip))
                    //{
                    //    // 更新表格和集合
                    //    var r = ONLINE_EQUIPMENT.TryTake(ip);
                    //    if (r != null)
                    //    {
                    //        refreshOnline(ip, ret);
                    //    }
                    //}
                }
            }
            finally
            {
                FanucDriver.cnc_freelibhndl(fanucH.h);
            }
        }


        public void insertSql(string text)
        {
            Task insertTask = new Task(() =>
            {
                SqlServerPool.ExecuteNonQuery(text);
            });
        }



        public void refreshChooseTraceData()
        {
            refreshTimer = new System.Timers.Timer(1000);
            // 设置Elapsed事件处理程序
            refreshTimer.Elapsed += refreshTraceData;
            // 启用定时器
            refreshTimer.Enabled = true;
        }

        public void refreshTraceData(object? sender, ElapsedEventArgs e)
        {
            if (lastIp == null || "".Equals(lastIp))
            {
                return;
            }
            pojo.Trace outVal = null;
            EQUIPMENT_DATA_DICT.TryGetValue(lastIp, out outVal);
            if (outVal != null)
            {
                LogInfo(outVal.ToString());
                refreshData(outVal);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace FanucDC.pojo
{
    public class Trace
    {
        private string? programName; // 程序名

        private int currentCount; // 当前总量

        private int totalCount; // 全部总量

        private int openTime; // 开机时间

        private int runTime; // 运行时间

        private int circleTime; // 循环时间

        private short alarm; // 警告标志

        private short status; // 当前状态

        private DateTime datetime; // 当前时间

        private String ip; //ip

        public string? ProgramName { get => programName; set => programName = value; }
        public int CurrentCount { get => currentCount; set => currentCount = value; }
        public int TotalCount { get => totalCount; set => totalCount = value; }
        public int OpenTime { get => openTime; set => openTime = value; }
        public int RunTime { get => runTime; set => runTime = value; }
        public int CircleTime { get => circleTime; set => circleTime = value; }
        public short Alarm { get => alarm; set => alarm = value; }
        public short Status { get => status; set => status = value; }
        public DateTime Datetime { get => datetime; set => datetime = value; }
        public string Ip { get => ip; set => ip = value; }

        public override string ToString()
        {
            string r = $" 当前生产数量 {CurrentCount} 总生产数量 {TotalCount} 开机时间 {OpenTime} 运行时间 {RunTime} 循环时间 {CircleTime} 状态  {Status}";
            return r;
        }
    }
}

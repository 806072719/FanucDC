using AntdUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanucDC.Models
{

    public class Equipment : NotifyProperty
    {
        private bool selected = false;

        private string code;

        private string name;

        private string ip;

        private short port;

        private DateTime lastTime;

        private bool connect = false;

        private int ret;


        public string Code
        {
            get { return code; }
            set
            {
                if (code == value) return;
                code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        public int Ret
        {
            get { return ret; }
            set
            {
                if (ret == value) return;
                ret = value;
                OnPropertyChanged(nameof(Ret));
            }
        }

        public bool Selected
        {
            get { return selected; }
            set
            {
                if (selected == value) return;
                selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Ip
        {
            get { return ip; }
            set
            {
                if (ip == value) return;
                ip = value;
                OnPropertyChanged(nameof(Ip));
            }
        }

        public short Port
        {
            get { return port; }
            set
            {
                if (port == value) return;
                port = value;
                OnPropertyChanged(nameof(Port));
            }
        }


        public DateTime LastTime
        {
            get { return lastTime; }
            set
            {
                if (lastTime == value) return;
                lastTime = value;
                OnPropertyChanged(nameof(LastTime));
            }
        }

        public bool Connect
        {
            get { return connect; }
            set
            {
                if (connect == value) return;
                connect = value;
                OnPropertyChanged(nameof(Connect));
            }
        }

    }
}

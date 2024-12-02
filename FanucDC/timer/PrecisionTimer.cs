using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PrecisionTimer
{
    /// <summary>
    /// 模式
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// 单次触发模式
        /// </summary>
        OneShot,
        /// <summary>
        /// 周期触发模式
        /// </summary>
        Periodic
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct TimerCaps
    {
        /// <summary>
        /// 最小周期
        /// </summary>
        public int periodMin;
        /// <summary>
        /// 最大周期
        /// </summary>
        public int periodMax;
    }

    public class TimerException : ApplicationException
    {
        public TimerException(string message) : base(message)
        { }
    }

    /// <summary>
    /// 高精度定时器 Timer
    /// </summary>
    public sealed class Timer : IComponent
    {
        #region delegate
        private delegate void TimeProc(int id, int msg, int user, int param1, int param2);
        private delegate void EventRaiser(EventArgs e);
        #endregion

        #region 多媒体定时器API
        /// <summary>
        /// 多媒体定时器API:用于获取系统定时器的能力信息
        /// </summary>
        /// <param name="caps"></param>
        /// <param name="sizeOfTimerCaps"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int timeGetDevCaps(ref TimerCaps caps, int sizeOfTimerCaps);

        /// <summary>
        /// 多媒体定时器API:创建一个定时事件
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="resolution"></param>
        /// <param name="proc"></param>
        /// <param name="user"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimeProc proc, int user, int mode);

        /// <summary>
        /// 多媒体定时器API:终止一个定时事件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);
        #endregion
        #region Private Properties
        private const int TIMERR_NOERROR = 0;
        private int timerID;
        private volatile Mode mode;
        private volatile int period;
        private volatile int resolution;
        private TimeProc timeProcPeriodic;
        private TimeProc timeProcOneShot;
        private EventRaiser tickRaiser;
        private bool running = false;
        private volatile bool disposed = false;
        private ISynchronizeInvoke synchronizingObject = null;
        private ISite site = null;
        private static TimerCaps caps;
        #endregion

        #region Public Events
        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler Tick;
        #endregion

        #region Ctor
        static Timer()
        {
            // Get multimedia timer capabilities.
            timeGetDevCaps(ref caps, Marshal.SizeOf(caps));
        }
        public Timer(IContainer container)
        {
            container.Add(this);
            Initialize();
        }
        public Timer()
        {
            Initialize();
        }
        ~Timer()
        {
            if (IsRunning)
            {
                // Stop and destroy timer.
                timeKillEvent(timerID);
            }
        }

        private void Initialize()
        {
            this.mode = Mode.Periodic;
            this.period = Capabilities.periodMin;
            this.resolution = 1;
            running = false;
            timeProcPeriodic = new TimeProc(TimerPeriodicEventCallback);
            timeProcOneShot = new TimeProc(TimerOneShotEventCallback);
            tickRaiser = new EventRaiser(OnTick);
        }
        #endregion
        #region 触发事件
        public void Start()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Timer");
            }
            if (IsRunning)
            {
                return;
            }
            if (Mode == Mode.Periodic)
            {
                timerID = timeSetEvent(Period, Resolution, timeProcPeriodic, 0, (int)Mode);
            }
            else
            {
                timerID = timeSetEvent(Period, Resolution, timeProcOneShot, 0, (int)Mode);
            }
            if (timerID != 0)
            {
                running = true;
                if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
                {
                    SynchronizingObject.BeginInvoke(
                        new EventRaiser(OnStarted),
                        new object[] { EventArgs.Empty });
                }
                else
                {
                    OnStarted(EventArgs.Empty);
                }
            }
            else
            {
                throw new TimerException("Unable to start timer.");
            }
        }
        public void Stop()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Timer");
            }
            if (!running)
            {
                return;
            }
            int result = timeKillEvent(timerID);
            Debug.Assert(result == TIMERR_NOERROR);
            running = false;
            if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
            {
                SynchronizingObject.BeginInvoke(
                    new EventRaiser(OnStopped),
                    new object[] { EventArgs.Empty });
            }
            else
            {
                OnStopped(EventArgs.Empty);
            }
        }
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }
            if (IsRunning)
            {
                Stop();
            }
            disposed = true;
            OnDisposed(EventArgs.Empty);
        }
        private void TimerPeriodicEventCallback(int id, int msg, int user, int param1, int param2)
        {
            if (synchronizingObject != null)
            {
                synchronizingObject.BeginInvoke(tickRaiser, new object[] { EventArgs.Empty });
            }
            else
            {
                OnTick(EventArgs.Empty);
            }
        }
        private void TimerOneShotEventCallback(int id, int msg, int user, int param1, int param2)
        {
            if (synchronizingObject != null)
            {
                synchronizingObject.BeginInvoke(tickRaiser, new object[] { EventArgs.Empty });
                Stop();
            }
            else
            {
                OnTick(EventArgs.Empty);
                Stop();
            }
        }
        // 触发 Disposed 事件.
        private void OnDisposed(EventArgs e)
        {
            EventHandler handler = Disposed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // 触发 Started 事件.
        private void OnStarted(EventArgs e)
        {
            EventHandler handler = Started;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        // 触发 Stopped 事件.
        private void OnStopped(EventArgs e)
        {
            EventHandler handler = Stopped;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // 触发 Tick 事件.
        private void OnTick(EventArgs e)
        {
            EventHandler handler = Tick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 获取或设置用于调度事件处理程序调用的对象。
        /// </summary>
        public ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                #region Require
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                #endregion
                return synchronizingObject;
            }
            set
            {
                #region Require
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                #endregion
                synchronizingObject = value;
            }
        }

        #endregion
        #region Public Properties
        /// <summary>
        /// 周期
        /// </summary>
        public int Period
        {
            get
            {
                #region Require
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                #endregion
                return period;
            }
            set
            {
                #region Require
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                else if (value < Capabilities.periodMin || value > Capabilities.periodMax)
                {
                    throw new ArgumentOutOfRangeException("Period", value,
                        "Multimedia Timer period out of range.");
                }
                #endregion
                period = value;
                if (IsRunning)
                {
                    Stop();
                    Start();
                }
            }
        }

        /// <summary>
        /// 定时器事件触发的最小间隔时间
        /// </summary>
        public int Resolution
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                return resolution;
            }
            set
            {
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                else if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Resolution", value, "timer resolution out of range.");
                }

                resolution = value;
                if (IsRunning)
                {
                    Stop();
                    Start();
                }
            }
        }
        /// <summary>
        /// 触发模式
        /// </summary>
        public Mode Mode
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                return mode;
            }
            set
            {

                if (disposed)
                {
                    throw new ObjectDisposedException("Timer");
                }
                mode = value;
                if (IsRunning)
                {
                    Stop();
                    Start();
                }
            }
        }

        /// <summary>
        /// 是否正则运行
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return running;
            }
        }

        /// <summary>
        /// 获取定时器的能力信息
        /// </summary>
        public static TimerCaps Capabilities
        {
            get
            {
                return caps;
            }
        }
        #endregion        
        #region IComponent
        public event System.EventHandler Disposed;
        public ISite Site
        {
            get
            {
                return site;
            }
            set
            {
                site = value;
            }
        }
        #endregion
    }
}
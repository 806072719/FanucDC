using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanucDC.db
{
    public static class CustomThreadPool
    {
        private static readonly int minWorkerThreads = 2;
        private static readonly int maxWorkerThreads = 10;
        private static readonly int minIoThreads = 2;
        private static readonly int maxIoThreads = 10;

        static CustomThreadPool()
        {
            // 设置线程池的最小和最大线程数
            ThreadPool.SetMinThreads(minWorkerThreads, minIoThreads);
            ThreadPool.SetMaxThreads(maxWorkerThreads, maxIoThreads);
        }

        public static void QueueWorkItem(WaitCallback callBack)
        {
            ThreadPool.QueueUserWorkItem(callBack);
        }

        public static void QueueWorkItem(WaitCallback callBack, object state)
        {
            ThreadPool.QueueUserWorkItem(callBack, state);
        }

    }
}

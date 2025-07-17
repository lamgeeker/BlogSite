using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file
{
    public class Logger
    {
        public ConcurrentQueue<string> logs = new ConcurrentQueue<string>();
        private Thread Mythread;
        public Logger() 
        {
            Mythread = new Thread(LogProcessor);
            Mythread.IsBackground = true;
            Mythread.Start();
        }

        

        public void AddLog(string message)
        {
            logs.Enqueue(message);
        }


        public void LogProcessor()
        {
           
            while (true)
            {
                if(logs.Any())
                {
                    if(logs.TryDequeue(out string message))
                    {
                        Console.WriteLine(message);
                    }
                    Thread.Sleep(200);
                }
            }
        }
    }
}

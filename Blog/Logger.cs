using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file
{
    public class Logger
    {
        public ConcurrentQueue<string> logs = new ConcurrentQueue<string>();
      
        public Logger() 
        {
            Task.Run(LogProcessor);
        }

        

        public void AddLog(string message)
        {
            logs.Enqueue(message);
        }


        public async Task LogProcessor()
        {
           
           
            while (true)
            {
                if(logs.Any())
                {
                    if(logs.TryDequeue(out string message))
                    {
                        Console.WriteLine(message);
                    }
                    await Task.Delay(500);
                }
            }
        }
    }
}

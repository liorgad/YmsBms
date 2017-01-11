using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class EventQueue<T> : IDisposable
    {
        ConcurrentQueue<T> queue;

        AutoResetEvent add;
        ManualResetEvent stop;        

        WaitHandle[] waitHandles = new WaitHandle[]
        {
            new AutoResetEvent(false),
            new ManualResetEvent(false)
        };

        Action<T> action;

        Task pollTask;

        public EventQueue(Action<T> action)
        {
            queue = new ConcurrentQueue<T>();
            add = (AutoResetEvent)waitHandles[0];
            stop = (ManualResetEvent)waitHandles[1];
                      
            this.action = action;
            pollTask = Task.Factory.StartNew(TaskAction);
        }

        private void TaskAction()
        {
            try
            {
                while (true)
                {
                    var waitIndex = WaitHandle.WaitAny(waitHandles, -1);
                    // stop event
                    if(waitIndex == 1)
                    {
                        break;
                    }
                    // add event
                    if(waitIndex == 0)
                    {
                        T item;
                        bool dequeueSucceeded = queue.TryDequeue(out item);
                        if(dequeueSucceeded)
                        {
                            action(item);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void Add(T item)
        {
            queue.Enqueue(item);
            add.Set();
        }

        public void Dispose()
        {
            stop.Set();            
            add.Dispose();
            stop.Dispose();
        }
    
    }
}

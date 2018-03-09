using Supervisor.Watchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "/console")
            {
                //CallContext.LogicalSetData()
                ExecuteAllWatchers();
                Console.ReadLine();
            }
            else {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new SupervisorService()
                };
                ServiceBase.Run(ServicesToRun);
            }

        }

        private static void ExecuteAllWatchers()
        {
            ExecuteWatcher(new IntradayExceptionWatcher());

           // ExecuteWatcher(new XyzWatcher());
        }

        private static void ExecuteWatcher(AbstractWatcher watcher)
        {
            var thread = new System.Threading.Thread(watcher.Execute);
            thread.Start();
        }
    }
}

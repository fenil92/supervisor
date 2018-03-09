using Logging;
using Logging.Core;
using Supervisor.Watchers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor
{
    partial class SupervisorService : ServiceBase
    {
        private readonly List<AbstractWatcher> _watchers;
        private readonly ISupervisorHelper _helper;
        private readonly ILogger _logger;
        public SupervisorService()
        {
            _logger = Logger.Instance;
            _helper = SupervisorHelper.Instance;
            _watchers = new List<AbstractWatcher>()
                        {
                            new IntradayExceptionWatcher()
                        };
            InitializeComponent();
            CanPauseAndContinue = false;
        }
        //public SupervisorService()
        //{
        //    InitializeComponent();
        //}

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            _watchers.ForEach(this.ExecuteWatcher);
        }

        private void ExecuteWatcher(AbstractWatcher watcher)
        {
            try {
                Task.Factory.StartNew(watcher.Execute);
            }
            catch(Exception ex)
            { 
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}

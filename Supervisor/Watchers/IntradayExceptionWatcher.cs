using Logging;
using Logging.Core;
using Supervisor.Repository;
using Supervisor.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor.Watchers
{
    class IntradayExceptionWatcher : AbstractWatcher
    {
        private readonly ISupervisorAppsettings _supervisorAppsettings;
        public IntradayExceptionWatcher():this(Logger.Instance, new SupervisorAppsettings(),new IntradayExceptionRepository(),SupervisorHelper.Instance)
        {

        }
        public IntradayExceptionWatcher(ILogger logger,ISupervisorAppsettings supervisorAppsettings, IIntradayExceptionRepository intradayExceptionRepository,ISupervisorHelper supervisorHelper):base(logger,supervisorAppsettings,intradayExceptionRepository.IntradayExceptionMonitor, supervisorHelper)
        {
            _supervisorAppsettings = supervisorAppsettings;
        }
        public override string WatcherName
        {
            get
            {
                return Constants.IntradayExceptionWatcher;
            }
        }

        public override double ElapsedInterval()
        {
            return _supervisorAppsettings.IntradayExceptionWatcherInterval;
        }
    }
}

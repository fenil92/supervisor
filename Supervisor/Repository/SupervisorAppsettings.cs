using Configuration;
using Supervisor.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor.Repository
{
   public class SupervisorAppsettings : ConfigAppSettings, ISupervisorAppsettings
    {
        public int IntradayExceptionWatcherInterval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int SupervisorScheduleWatcherInterval
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

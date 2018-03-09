using Configuration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor.Repository.Core
{
    public interface ISupervisorAppsettings : IConfigAppSettings
    {

        int SupervisorScheduleWatcherInterval { get; }

        int IntradayExceptionWatcherInterval { get; }
    }
}

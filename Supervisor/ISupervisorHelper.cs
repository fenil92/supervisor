using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor
{
    public interface ISupervisorHelper
    {
        string GetUserIdentityName();

        string GetMachineName();

        DateTime GetClockServerTime();

        string GetSupervisorLoggingText(string key,object[] parameters = null);
    }
}

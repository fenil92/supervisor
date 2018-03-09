using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor
{
    public class SupervisorHelper : ISupervisorHelper
    {
        #region Variables

        private static ISupervisorHelper _instance;
        #endregion

        public static ISupervisorHelper Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                else
                {
                    _instance = new SupervisorHelper();
                    return _instance;
                }
            }
        }

        public DateTime GetClockServerTime()
        {
            throw new NotImplementedException();
        }

        public string GetMachineName()
        {
            return Environment.MachineName;
        }

        public string GetSupervisorLoggingText(string key, object[] parameters = null)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdentityName()
        {
            return Dns.GetHostName().ToString();
        }
    }
}

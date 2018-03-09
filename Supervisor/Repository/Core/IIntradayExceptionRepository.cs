using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supervisor.Repository.Core
{
    public interface IIntradayExceptionRepository
    {
        void IntradayExceptionMonitor(DateTime clockdateTimeParam);
    }
}

using System;
using System.Timers;
using Logging.Core;
using Supervisor.Repository.Core;
using Logging;

namespace Supervisor.Watchers
{
    public abstract class AbstractWatcher
    {
        
        private static Timer _supervisorTimer;
        private readonly ILogger _logger;

        private readonly object _lock = new object();
        private readonly ISupervisorAppsettings _supervisorAppsetting;
        private readonly Action _repository;
        private readonly Action <DateTime>_repositoryParam;
        private readonly Action<int> _repositoryParamInt;
        private DateTime _dateTime;
        private readonly ISupervisorHelper _helper;
      
        public object StackTrace { get; private set; }

        public abstract string WatcherName { get; }
        public abstract double ElapsedInterval();
        protected AbstractWatcher(ILogger logger, ISupervisorAppsettings supervisorAppsetting,Action repository,ISupervisorHelper helper)
        {
            _logger = logger;
            _supervisorAppsetting = supervisorAppsetting;
            _repository = repository;
            _helper = helper;
        }
        protected AbstractWatcher(ILogger logger, ISupervisorAppsettings supervisorAppsetting, Action<DateTime> repositoryParam, ISupervisorHelper helper)
        {
            _logger = logger;
            _supervisorAppsetting = supervisorAppsetting;
            _repositoryParam = repositoryParam;
            _helper = helper;
        }
        protected AbstractWatcher(ILogger logger, ISupervisorAppsettings supervisorAppsetting, Action<int> repositoryParamInt, ISupervisorHelper helper)
        {
            _logger = logger;
            _supervisorAppsetting = supervisorAppsetting;
            _repositoryParamInt = repositoryParamInt;
            _helper = helper;
        }

        public void Execute()
        {
            try
            {
                _supervisorTimer = new Timer { Interval = this.ElapsedInterval(),Enabled = true};
                _supervisorTimer.Elapsed += this.SupervisorTimeElapsed;
                this.SupervisorTimeElapsed(null, null);
                _supervisorTimer.Start();

            }
            catch (BaseException)
            { }
            catch (Exception ex)
            {
                _logger.LogInfo(
                    () => _helper.GetSupervisorLoggingText("Key",new object[] { StackTrace.GetType().Name})
                    );
            }
        }

        private void SupervisorTimeElapsed(object sender, ElapsedEventArgs e)
        {
            lock(_lock)
            {
                if (_repository != null)
                {
                    _logger.LogInfo(
                        ()=> _helper.GetSupervisorLoggingText("Key",new object[] { "MethodName"})
                        );

                    _repository();
                }
                else if(_repositoryParam != null && _repositoryParam.GetType()== typeof(Action<DateTime>))
                {
                    string methodName = _repositoryParam.Method.Name;

                    switch (methodName)
                    {
                        case Constants.IntradayExceptionWatcher :
                            {
                                _dateTime = _helper.GetClockServerTime().Subtract(TimeSpan.FromSeconds(_supervisorAppsetting.IntradayExceptionWatcherInterval));
                                break;
                            }
                        case Constants.SupervisorScheduleWatcher:
                            {
                                _dateTime = _helper.GetClockServerTime().Subtract(TimeSpan.FromSeconds(_supervisorAppsetting.SupervisorScheduleWatcherInterval));
                                break;
                            }
                    }
                    _repositoryParam(_dateTime);
                }

            }
        }
    }
}
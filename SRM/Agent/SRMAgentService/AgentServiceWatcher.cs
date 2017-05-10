using System.ServiceProcess;
using SRM.Agent.Services;
using SRM.Commons;

namespace SRM.Agent.Service
{
    public sealed partial class AgentServiceWatcher : ServiceBase
    {
        private WatcherLoader _wl;

        public AgentServiceWatcher()
        {
            JLogger.LogInfo(this, "AgentServiceWatcher() Start");
            _wl = new WatcherLoader();

            ServiceName = "SRMAgentServiceWatcher";
            EventLog.Log = "Application";

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            CanHandlePowerEvent = true;
            CanHandleSessionChangeEvent = true;
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;

            InitializeComponent();

            JLogger.LogInfo(this, "AgentServiceWatcher() End");
        }

        protected override void OnStart(string[] args)
        {
            JLogger.LogInfo(this, "OnStart() Start");

            if (_wl == null)
            {
                _wl = new WatcherLoader();
            }

            _wl.StartWatchers();

            JLogger.LogInfo(this, "OnStart() End");
        }

        protected override void OnStop()
        {
            JLogger.LogInfo(this, "OnStop() Start");
            base.OnStop();
            if (_wl != null)
            {
                _wl.StopWatchers();
                _wl = null;
            }
            JLogger.LogInfo(this, "OnStop() End");
        }

        protected override void OnPause()
        {
            JLogger.LogInfo(this, "OnPause() Start");
            base.OnPause();
            JLogger.LogInfo(this, "OnPause() End");
        }

        protected override void OnContinue()
        {
            JLogger.LogInfo(this, "OnContinue() Start");
            base.OnContinue();
            JLogger.LogInfo(this, "OnContinue() End");
        }

        protected override void OnShutdown()
        {
            JLogger.LogInfo(this, "OnShutdown() Start");
            base.OnShutdown();
            JLogger.LogInfo(this, "OnShutdown() End");
        }

        protected override void OnCustomCommand(int command)
        {
            JLogger.LogInfo(this, "OnCustomCommand() Start");
            //  A custom command can be sent to a service by using this method:
            //#  int command = 128; //Some Arbitrary number between 128 & 256
            //#  ServiceController sc = new ServiceController("NameOfService");
            //#  sc.ExecuteCommand(command);

            base.OnCustomCommand(command);
            JLogger.LogInfo(this, "OnCustomCommand() End");
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            JLogger.LogInfo(this, "OnPowerEvent() Start");
            return base.OnPowerEvent(powerStatus);
            //JLogger.LogInfo(this, "OnPowerEvent() End");
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            JLogger.LogInfo(this, "OnSessionChange() Start");
            base.OnSessionChange(changeDescription);
            JLogger.LogInfo(this, "OnSessionChange() End");
        }
    }
}
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SRMAgentService
{
    public partial class AgentServiceCommand : ServiceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AgentServiceCommand()
        {
            Log.Info("AgentServiceCommand() Start");

            this.ServiceName = "SRMAgentService_Command";
            this.EventLog.Log = "Application";

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            InitializeComponent();

            Log.Info("AgentServiceCommand() End");
        }

        protected override void OnStart(string[] args)
        {
            Log.Info("OnStart() Start");
            base.OnStart(args);
            Log.Info("OnStart() End");
        }

        protected override void OnStop()
        {
            Log.Info("OnStop() Start");
            base.OnStop();
            Log.Info("OnStop() End");
        }

        protected override void OnPause()
        {
            Log.Info("OnPause() Start");
            base.OnPause();
            Log.Info("OnPause() End");
        }

        protected override void OnContinue()
        {
            Log.Info("OnContinue() Start");
            base.OnContinue();
            Log.Info("OnContinue() End");
        }

        protected override void OnShutdown()
        {
            Log.Info("OnShutdown() Start");
            base.OnShutdown();
            Log.Info("OnShutdown() End");
        }

        protected override void OnCustomCommand(int command)
        {
            Log.Info("OnCustomCommand() Start");
            //  A custom command can be sent to a service by using this method:
            //#  int command = 128; //Some Arbitrary number between 128 & 256
            //#  ServiceController sc = new ServiceController("NameOfService");
            //#  sc.ExecuteCommand(command);

            base.OnCustomCommand(command);
            Log.Info("OnCustomCommand() End");
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            Log.Info("OnPowerEvent() Start");
            return base.OnPowerEvent(powerStatus);
            Log.Info("OnPowerEvent() End");
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            Log.Info("OnSessionChange() Start");
            base.OnSessionChange(changeDescription);
            Log.Info("OnSessionChange() End");
        }
    }
}

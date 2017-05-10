using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using SRM.Agent.Services;
using SRM.Commons;

namespace SRM.Agent.Service
{
    public sealed partial class AgentServiceCommand : ServiceBase
    {
        private ServiceHost _host;

        public AgentServiceCommand()
        {
            JLogger.LogInfo(this, "AgentServiceCommand() Start");

            ServiceName = "SRMAgentServiceCommand";
            EventLog.Log = "Application";

            CanHandlePowerEvent = true;
            CanHandleSessionChangeEvent = true;
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;

            InitializeComponent();

            JLogger.LogInfo(this, "AgentServiceCommand() End");
        }

        protected override void OnStart(string[] args)
        {
            JLogger.LogInfo(this, "OnStart() Start");

            base.OnStart(args);
            _host?.Close();

            try
            {
                JLogger.LogDebug(this, "Se crea el service host con la direccion base.");
                var baseAddress = new Uri("http://localhost:8080/CommandService");
                _host = new ServiceHost(typeof (CommandService), baseAddress);

                JLogger.LogDebug(this, "Se configura para presentar el metadata.");
                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    MetadataExporter = {PolicyVersion = PolicyVersion.Policy15}
                };
                _host.Description.Behaviors.Add(smb);

                JLogger.LogDebug(this, "Se Abre la comunicacion.");
                _host.Open();
            }
            catch (Exception ex)
            {
                JLogger.LogError(this, "Error al crear el host.", ex);
            }

            JLogger.LogInfo(this, "OnStart() End");
        }

        protected override void OnStop()
        {
            JLogger.LogInfo(this, "OnStop() Start");

            base.OnStop();
            if (_host != null)
            {
                _host.Close();
                _host = null;
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
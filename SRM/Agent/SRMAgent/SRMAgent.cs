using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using SRM.Agent.Services;
using SRM.Commons;
using SRM.Agent.Commons;

namespace SRM.Agent
{
    public class SRMAgent
    {
        private static SRMServerAccess _factHandlerClient;

        private ServiceHost _commandService;
        private WatcherLoader _watcherLoader;

        public void Start()
        {
            JLogger.LogInfo(this, "Start() In");

            //INIT REMOTE OPERATIONS
            _factHandlerClient = new SRMServerAccess();

            //Enroll Machine
            //JMachine.Id = _factHandlerClient.EnrollMachine(JMachine.GetMachine());
            
            //Get Configuration
            //_factHandlerClient.GetMachineConfiguration(_machine);

            //Start CommandService
            try
            {
                JLogger.LogDebug(this, "Se crea el service host con la direccion base.");
                var baseAddress = new Uri("http://localhost:8080/CommandService");
                _commandService = new ServiceHost(typeof(CommandService), baseAddress);

                JLogger.LogDebug(this, "Se configura para presentar el metadata.");
                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                };
                _commandService.Description.Behaviors.Add(smb);

                JLogger.LogDebug(this, "Se Abre la comunicacion.");
                _commandService.Open();
            }
            catch (Exception ex)
            {
                JLogger.LogError(this, "Error al crear el host.", ex);
            }

            //Start Watchers
            _watcherLoader = new WatcherLoader();
            _watcherLoader.StartWatchers();

            JLogger.LogInfo(this, "Stop() Out");
        }

        public void Stop()
        {
            JLogger.LogInfo(this, "Stop() In");

            //STOP COMMANDSERVICE
            if (_commandService != null)
            {
                _commandService.Close();
                _commandService = null;
            }

            //STOP WATCHERS
            if (_watcherLoader != null)
            {
                _watcherLoader.StopWatchers();
                _watcherLoader = null;
            }

            JLogger.LogInfo(this, "Stop() Out");
        }
    }
}

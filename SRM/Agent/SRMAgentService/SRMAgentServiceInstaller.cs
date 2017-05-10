using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

//installutil.exe SRMAgentService.exe 

namespace SRM.Agent.Service
{
    [RunInstaller(true)]
    public partial class SRMAgentServiceInstaller : Installer
    {
        public SRMAgentServiceInstaller()
        {
            //# Service Account Information
            var serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem,
                Username = null,
                Password = null
            };

            //# Service Information
            var serviceInstallerCommand = new ServiceInstaller
            {
                ServiceName = "SRMAgentServiceCommand",
                DisplayName = "SRMAgentServiceCommand",
                Description = "SRMAgentService process request commands",
                StartType = ServiceStartMode.Automatic,
                ServicesDependedOn = new[] {"SRMAgentServiceWatcher"}
            };

            var serviceInstallerWatcher = new ServiceInstaller
            {
                ServiceName = "SRMAgentServiceWatcher",
                DisplayName = "SRMAgentServiceWatcher",
                Description = "SRMAgentService Watcher events",
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstallerCommand);
            Installers.Add(serviceInstallerWatcher);

            InitializeComponent();
        }
    }
}
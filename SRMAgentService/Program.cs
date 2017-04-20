using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SRMAgentService
{
    static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Log.Info("SRMAgentService Main Start...");

            Log.Debug("SRMAgentService creating services instances array");
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new AgentServiceMonitor(),
                new AgentServiceCommand()
            };
            Log.Debug("SRMAgentService run services instances array");
            ServiceBase.Run(ServicesToRun);

            Log.Info("SRMAgentService Main End...");
        }
    }
}

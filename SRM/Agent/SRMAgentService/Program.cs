using System.ServiceProcess;
using SRM.Commons;

namespace SRM.Agent.Service
{
    internal static class Program
    {
        //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            JLogger.LogInfo(null, "SRMAgentService Main Start...");

            JLogger.LogDebug(null, "SRMAgentService creating services instances array");
            ServiceBase[] servicesToRun = new ServiceBase[]
            {
                new AgentServiceCommand(),
                new AgentServiceWatcher()
            };
            JLogger.LogDebug(null, "SRMAgentService run services instances array");
            ServiceBase.Run(servicesToRun);

            JLogger.LogInfo(null, "SRMAgentService Main End...");
        }
    }
}
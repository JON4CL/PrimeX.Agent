using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using SRM.Agent.Commons;
using SRM.Agent.Commons.FactHandler;
using SRM.Agent.Services;

namespace SRMWatcherServiceTest
{
    internal class Program
    {
        private static readonly SRMServerAccess ServerAccess = new SRMServerAccess();

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            //ENROLL MACHINE
            var hostName = Dns.GetHostName();
            var ipHostInfo = Dns.GetHostEntry(hostName);
            var ipAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));

            int id = ServerAccess.EnrollMachine(new Machine() {Ip = ipAddress, Name = hostName });
            //GET CONFIGURATION

            //ENABLE WATCHERS
            var wl = new WatcherLoader();
            wl.StartWatchers();

            Console.WriteLine("Press Any Key to stop");
            Console.ReadKey();
            wl.StopWatchers();
        }
    }
}
using System;
using SRM.Agent.Services;

namespace SRMWatcherServiceTest
{
    internal class Program
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var wl = new WatcherLoader();
            wl.StartWatchers();

            Console.WriteLine("Press Any Key to stop");
            Console.ReadKey();
            wl.StopWatchers();
        }
    }
}
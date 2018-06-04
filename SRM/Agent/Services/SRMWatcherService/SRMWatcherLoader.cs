using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SRM.Agent;
using SRM.Agent.Commons;
using SRM.Agent.Commons.FactHandler;
using SRM.Commons;

namespace SRM.Agent.Services
{
    public class WatcherLoader
    {
        //CONFIG FIELD
        private static readonly JConfig Config = new JConfig("SRMWatcherLoader.config");
        private readonly object _objectLock = new object();
        //FACT LOG FILE
        private readonly string _savedFactFileName = Config.GetValueByKey("FACTWATCHER_LOG");
        private readonly string _factStorePath = SRMAgentPaths.SRMFacts;
        private readonly List<IFactWatcher> _watchers;
        //
        private readonly SRMServerAccess _serverAccess = new SRMServerAccess();

        public WatcherLoader()
        {
            JLogger.LogInfo(this, "Class SRMWatcherLoader()");
            _watchers = new List<IFactWatcher>();
            LoadWatchers();
        }

        private void LoadWatchers()
        {
            JLogger.LogInfo(this, "LoadWatchers()");
            var watchers = Directory.EnumerateFiles(SRMAgentPaths.SRMComponents, "*.dll").ToList();
            foreach (var watcher in watchers)
            {
                JLogger.LogDebug(this, "Component found: {0}", watcher);
                try
                {
                    JLogger.LogDebug(this, "Loading assembly from file");
                    var ptrAssembly = Assembly.LoadFile(watcher);
                    foreach (var item in ptrAssembly.GetTypes())
                    {
                        if (!item.IsClass)
                        {
                            continue;
                        }
                        JLogger.LogDebug(this, "Filter for correct interface");
                        if (item.GetInterfaces().Contains(typeof (IFactWatcher)))
                        {
                            var inst = (IFactWatcher) Activator.CreateInstance(item);
                            _watchers.Add(inst);
                            JLogger.LogDebug(this, "Watcher '{0}' added to list", inst.GetFactWatcherName());
                        }
                    }
                }
                catch (Exception ex)
                {
                    JLogger.LogError(this, "LoadWatchers() Exception found: {0}", ex.Message);
                }
            }
        }

        public void StartWatchers()
        {
            JLogger.LogInfo(this, "StartWatchers()");

            foreach (var watcher in _watchers)
            {
                JLogger.LogDebug(this, "Attached 'OnNewFactProcess' to Watcher '{0}'", watcher.GetFactWatcherName());
                watcher.OnNewFact += OnNewFactProcess;
                JLogger.LogDebug(this, "Starting Watcher '{0}'", watcher.GetFactWatcherName());
                watcher.Start();
            }
        }

        public void StopWatchers()
        {
            JLogger.LogInfo(this, "StoptWatchers()");

            foreach (var watcher in _watchers)
            {
                JLogger.LogDebug(this, "Stoping Watcher '{0}'", watcher.GetFactWatcherName());
                watcher.Stop();
                JLogger.LogDebug(this, "Disattached 'OnNewFactProcess' to Watcher '{0}'", watcher.GetFactWatcherName());
                watcher.OnNewFact -= OnNewFactProcess;
            }
        }

        private void OnNewFactProcess(object sender, EventArgs e)
        {
            JLogger.LogInfo(this, "OnNewFactProcess()");
            var ea = (FactWatcherEventArgs)e;

            var sbFactMessage = new StringBuilder();
            sbFactMessage.AppendFormat("{0}-{1}-{2}", DateTime.Now.ToString("yyyyMMddHHmmssff",
                    CultureInfo.InvariantCulture),
                ea.FactWatcherName,
                ea.FactData);

            //string factFileName = _factStorePath + DateTime.Now.ToString("yyyyMMddHHmmssffff", CultureInfo.InvariantCulture) + "-" + 
            //    ea.FactDatetime.ToString("yyyyMMddHHmmssffff", CultureInfo.InvariantCulture) + ".log";
            //JLogger.LogDebug(this, "Opening {0} file to write", factFileName);
            //using (var factFile = File.Open(factFileName, FileMode.CreateNew, FileAccess.Write))
            //{
            //    JLogger.LogDebug(this, "Writing to file", factFileName);
            //    factFile.Write(Encoding.ASCII.GetBytes(sbFactMessage.ToString()), 0, sbFactMessage.Length);
            //}

            _serverAccess.ProcessFact(new Fact(){
                MachineId = 0,
                AgentTimestamp = DateTime.Now.ToString("yyyyMMddHHmmssff", CultureInfo.InvariantCulture),
                Watcher = ea.FactWatcherName,
                Datetime = ea.FactDatetime.ToString("yyyyMMddHHmmssff", CultureInfo.InvariantCulture),
                Data = ea.FactData
            });

            SaveFactToLog(ea.FactWatcherName, ea.FactData);
        }

        private void SaveFactToLog(string watcherName, string message)
        {
            JLogger.LogInfo(this, "SaveFactToLog()");

            var sbFactMessage = new StringBuilder();
            sbFactMessage.AppendFormat("{0}-{1}-{2}{3}", DateTime.Now.ToString("yyyyMMddHHmmssff",
                CultureInfo.InvariantCulture),
                watcherName,
                message.Replace(Environment.NewLine, " ").Replace('\r', ' '),
                Environment.NewLine);

            JLogger.LogDebug(this, "validate if it is configured log file {0}", _savedFactFileName);
            if (!string.IsNullOrEmpty(_savedFactFileName))
            {
                lock (_objectLock)
                {
                    JLogger.LogDebug(this, "Opening {0} file to write", _savedFactFileName);
                    using (var factFile = File.Open(SRMAgentPaths.SRMData + _savedFactFileName, FileMode.Append, FileAccess.Write))
                    {
                        JLogger.LogDebug(this, "Writing to file", _savedFactFileName);
                        factFile.Write(Encoding.ASCII.GetBytes(sbFactMessage.ToString()), 0, sbFactMessage.Length);
                    }
                }
            }

            //PRINT TO CONSOLE
            if (!string.IsNullOrEmpty(Config.GetValueByKey("TO_CONSOLE")))
            {
                Console.WriteLine(sbFactMessage.ToString());
            }
        }
    }
}
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SRM.Agent.Commons;
using SRM.Commons;

namespace SRM.Agent.Watchers
{
    public class SRMBeatWatcher : IFactWatcher
    {
        private static readonly string FactWatcherName = "BeatWatcher";
        private static readonly string FactWatcherDescription = "Get a BEAT in a certain amount of time.";
        private static readonly string FactWatcherVersion = "0.0.1";
        //CONFIG FIELD
        public static JConfig Config = new JConfig(Assembly.GetExecutingAssembly().GetName().Name + ".config");
        private CancellationTokenSource _cts;
        public event EventHandler OnNewFact;

        public string GetFactWatcherDescription()
        {
            return FactWatcherDescription;
        }

        public string GetFactWatcherName()
        {
            return FactWatcherName;
        }

        public string GetFactWatcherVersion()
        {
            return FactWatcherVersion;
        }

        public void Start()
        {
            JLogger.LogInfo(this, "Start()");
            int count = 0;
            int sleepTime;
            int.TryParse(Config.GetValueByKey("BEAT_TIME_MS"), out sleepTime);

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    //DO YOUR WORK
                    Thread.Sleep(sleepTime);
                    OnNewFact?.Invoke(this, new FactWatcherEventArgs(GetFactWatcherName(), DateTime.Now, "BEAT" + count));
                    count++;
                }
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop()
        {
            JLogger.LogInfo(this, "Stop()");

            _cts?.Cancel();
        }
    }
}
using System;

namespace SRM.Agent.Commons
{
    public interface IFactWatcher
    {
        string GetFactWatcherName();
        string GetFactWatcherDescription();
        string GetFactWatcherVersion();
        void Start();
        void Stop();
        event EventHandler OnNewFact;
    }
}
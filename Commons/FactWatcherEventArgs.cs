using System;

namespace SRM.Agent.Commons
{
    public class FactWatcherEventArgs : EventArgs
    {
        public string FactData;
        public DateTime FactDatetime;
        public string FactWatcherName;

        public FactWatcherEventArgs(string factWatcherName, DateTime factDateTime, string factData)
        {
            FactWatcherName = factWatcherName;
            FactDatetime = factDateTime;
            FactData = factData;
        }
    }
}
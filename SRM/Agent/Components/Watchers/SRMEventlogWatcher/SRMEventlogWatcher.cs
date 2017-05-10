using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;
using SRM.Agent.Commons;
using SRM.Commons;

namespace SRM.Agent.Watchers
{
    public class SRMEventlogFactWatcher : IFactWatcher
    {
        private static readonly string FactWatcherName = "EventLogWatcher";
        private static readonly string FactWatcherDescription = "Get Windows EventLog entry and new events.";
        private static readonly string FactWatcherVersion = "0.0.1";
        //CONFIG FIELD
        public static JConfig Config = new JConfig(Assembly.GetExecutingAssembly().GetName().Name + ".config");
        public static JConfig Persistence = new JConfig(Assembly.GetExecutingAssembly().GetName().Name + ".persistence");
        private readonly object _objectLock = new object();
        //FACT LOG FILE
        private readonly string _savedFactFileName = Config.GetValueByKey("FACT_FILE_NAME");
        private List<EventLog> _eventLogs;
        public event EventHandler OnNewFact;

        public void Start()
        {
            JLogger.LogInfo(this, "Start()");

            //GET BASE CONFIGURATION
            string[] savedEventLogsName = Config.GetValueByKey("EVENT_LOGS")?.Split(',');
            int savedLastEntryIndex; //Latest index value processed and saved
            int.TryParse(Persistence.GetValueByKey("LAST_ENTRY_INDEX"), out savedLastEntryIndex);
            var lastEntryIndex = 0; //Latest index value from actual event logs

            DateTime lastEntryDatetime = new DateTime();
            DateTime savedLastEntryDatetime;
            DateTime.TryParse(Persistence.GetValueByKey("LAST_ENTRY_DATETIME"), out savedLastEntryDatetime);

            //CREATE EVENT LOG LIST
            _eventLogs = new List<EventLog>();

            //VALIDATE AND ENABLE EVENT CATCH
            if (savedEventLogsName != null)
                foreach (var eventLogName in savedEventLogsName)
                {
                    JLogger.LogDebug(this, "Checking if EventLog {0} has registry configuration", eventLogName);
                    //CHECK FOR LOGFILE
                    var regEventLog =
                        Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\EventLog\\" + eventLogName);
                    if (regEventLog != null)
                    {
                        JLogger.LogDebug(this, "EventLog {0} has registry configuration", eventLogName);
                        var temp = regEventLog.GetValue("File");
                        if (temp != null)
                        {
                            JLogger.LogDebug(this, "EventLog {0} has EventLog file created", eventLogName);

                            //CREATING AN EVENTLOG OBJECT FROM THE VALIDATED STRING
                            JLogger.LogDebug(this, "Creating EventLog object for {0}", eventLogName);
                            var eventLog = new EventLog(eventLogName);
                            _eventLogs.Add(eventLog);

                            //ATTACH EVENT
                            JLogger.LogDebug(this, "Attaching event handler to EventLog {0}", eventLogName);
                            eventLog.EntryWritten += OnEntryWritten;
                            eventLog.EnableRaisingEvents = true;

                            //GETTING THE LAST ENTRY FROM THE EVENTLOG
                            JLogger.LogDebug(this, "Getting the last entry from the EventLog {0}", eventLogName);
                            var lastEntry =
                                (from EventLogEntry ent in eventLog.Entries orderby ent.TimeGenerated select ent)
                                    .LastOrDefault();
                            if (lastEntry != null && lastEntry.Index > lastEntryIndex)
                            {
                                JLogger.LogDebug(this, "EventLog {0} last entry was {1}", eventLogName, lastEntry.Message);
                                lastEntryIndex = lastEntry.Index;
                            }

                            if (lastEntry != null && lastEntry.TimeGenerated >= lastEntryDatetime)
                            {
                                lastEntryDatetime = lastEntry.TimeGenerated;
                            }
                        }
                        else
                        {
                            //NO FILE ASSIGNED FOR THE EVENTLOG
                            JLogger.LogError(this, "EventLog {0} has not file assigned", eventLogName);
                        }
                    }
                    else
                    {
                        //NO REGISTRY KEY CREATED FOR THE EVENTLOG
                        JLogger.LogError(this, "EventLog {0} has not registry key created", eventLogName);
                    }
                }

            //PROCESS ALL EVENTS
            JLogger.LogDebug(this, "Processing events in chequed EventLogs");
            foreach (var eventLog in _eventLogs)
            {
                JLogger.LogDebug(this, "Processing events in EventLog {0}", eventLog.LogDisplayName);
                var filteredEventEntrys = (from EventLogEntry ent
                    in
                    eventLog.Entries.Cast<EventLogEntry>()
                        .Where(x => x.TimeGenerated <= lastEntryDatetime && x.TimeGenerated >= savedLastEntryDatetime)
                    orderby ent.TimeGenerated
                    select ent);
                //var filteredEventEntrys = (from EventLogEntry ent
                //           in eventLog.Entries.Cast<EventLogEntry>().Where(x => x.Index <= lastEntryIndex && x.Index > savedLastEntryIndex)
                //                           orderby ent.Index
                //                           select ent);

                JLogger.LogDebug(this, "eventslog:{0} FILTERED:{1} TOTAL:{2}",
                    eventLog.LogDisplayName, filteredEventEntrys.Count(), eventLog.Entries.Count);
                foreach (var filteredEventEntry in filteredEventEntrys)
                {
                    JLogger.LogDebug(this, "Start() Processing entry: {0}", filteredEventEntry.Index);
                    var fieldSeparator = (char) 28;
                    var sbFactMessage = new StringBuilder();
                    sbFactMessage.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                        eventLog.LogDisplayName, fieldSeparator,
                        filteredEventEntry.Index, fieldSeparator,
                        filteredEventEntry.Source, fieldSeparator,
                        filteredEventEntry.TimeGenerated, fieldSeparator,
                        filteredEventEntry.Message);

                    //FIRE NEW EVENT
                    JLogger.LogDebug(this, "Start() Firing Event: {0}", sbFactMessage.ToString());
                    OnNewFact?.Invoke(this,
                        new FactWatcherEventArgs(GetFactWatcherName(), filteredEventEntry.TimeGenerated,
                            sbFactMessage.ToString()));

                    //SAVE LOG FACTS
                    JLogger.LogDebug(this, "Validate if is configured fact file");
                    if (!string.IsNullOrEmpty(_savedFactFileName))
                    {
                        SaveFactToLog(sbFactMessage.ToString());
                    }

                    //STORE THE LATEST INDEX PROCESSED
                    Persistence.SetValueByKey("LAST_ENTRY_INDEX", filteredEventEntry.Index.ToString());
                    Persistence.SetValueByKey("LAST_ENTRY_DATETIME", filteredEventEntry.TimeGenerated.ToString(CultureInfo.InvariantCulture));
                    //Config.Save();
                }
            }
        }

        public void Stop()
        {
            JLogger.LogInfo(this, "Stop()");
            //CLOSE ALL THE EVENT LOGS FILES OPENED
            foreach (var eventLog in _eventLogs)
            {
                JLogger.LogDebug(this, "Stoping {0} eventlog", eventLog.LogDisplayName);
                eventLog.EnableRaisingEvents = false;
                eventLog.Close();
            }
        }

        public string GetFactWatcherName()
        {
            return FactWatcherName;
        }

        public string GetFactWatcherDescription()
        {
            return FactWatcherDescription;
        }

        public string GetFactWatcherVersion()
        {
            return FactWatcherVersion;
        }

        //
        private void OnEntryWritten(object source, EntryWrittenEventArgs e)
        {
            JLogger.LogInfo(this, "OnEntryWritten() eventId:{0}", e.Entry.Index);

            //ALL THE EVENT LOGS CAN FIRE THIS EVENT HANDLER SO WE NEED TO 
            //GET EVENTLOG NAME FROM EVENTLOG SOURCE (DIRTY WAY)
            var eventLogName = (string) source.GetType().GetProperty("Log").GetValue(source, null);

            var fieldSeparator = (char) 28;
            var sbFactMessage = new StringBuilder();
            sbFactMessage.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                eventLogName, fieldSeparator,
                e.Entry.Index, fieldSeparator,
                e.Entry.Source, fieldSeparator,
                e.Entry.TimeGenerated, fieldSeparator,
                e.Entry.Message);

            //FIRE NEW EVENT
            JLogger.LogInfo(this, "Firing the event");
            OnNewFact?.Invoke(this,
                new FactWatcherEventArgs(GetFactWatcherName(), e.Entry.TimeGenerated, sbFactMessage.ToString()));

            //SAVE LOG FACTS
            JLogger.LogDebug(this, "Validate if is configured fact file");
            if (!string.IsNullOrEmpty(_savedFactFileName))
            {
                SaveFactToLog(sbFactMessage.ToString());
            }

            //STORE THE LATEST INDEX PROCESSED
            Persistence.SetValueByKey("LAST_ENTRY_INDEX", e.Entry.Index.ToString());
            Persistence.SetValueByKey("LAST_ENTRY_DATETIME", e.Entry.TimeGenerated.ToString(CultureInfo.InvariantCulture));
            //Persistence.Save();
        }

        private void SaveFactToLog(string message)
        {
            JLogger.LogInfo(this, "SaveFactToLog()");

            var sbFactMessage = new StringBuilder();
            sbFactMessage.AppendFormat("{0}-{1}{2}",
                DateTime.Now.ToString("yyyyMMddHHmmssff", CultureInfo.InvariantCulture), message, Environment.NewLine);

            JLogger.LogDebug(this, "Opening {0} file to write", _savedFactFileName);
            lock (_objectLock)
            {
                using (var factFile = File.Open(SRMPaths.SRMData + _savedFactFileName, FileMode.Append, FileAccess.Write)
                    )
                {
                    JLogger.LogDebug(this, "Writing to file", _savedFactFileName);
                    factFile.Write(Encoding.ASCII.GetBytes(sbFactMessage.ToString()), 0, sbFactMessage.Length);
                }
            }
        }
    }
}
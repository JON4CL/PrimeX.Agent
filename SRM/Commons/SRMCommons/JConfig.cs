using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SRM.Commons
{
    public class JConfigItem
    {
        public string Key;
        public string Value;
    }

    public class JConfig
    {
        private static readonly object ObjectLock = new object();

        private readonly string _configFileBasePath =
            Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\config\");
        
        private readonly string _configFileName;
        private JConfigItem[] _jconfigItems;

        public JConfig(string fileName)
        {
            JLogger.LogInfo(this, "JConfig(fileName): {0}", fileName);
            _configFileName = fileName + ".jconf";
            JLogger.LogDebug(this, "JConfig() FileName:{0}", FileName);
            ReadJson();
        }

        public JConfig(string fileName, string filePath)
        {
            JLogger.LogInfo(this, "JConfig(fileName,filePath): {0}, {1}", filePath, fileName);
            _configFileBasePath = filePath;
            _configFileName = fileName + ".jconf";
            JLogger.LogDebug(this, "JConfig() FileName:{0}", FileName);
            ReadJson();
        }

        private string FileName => _configFileBasePath + _configFileName;

        private void ReadJson()
        {
            JLogger.LogInfo(this, "ReadJson() Filename:{0}", FileName);
            var serializer = new JsonSerializer();
            if (File.Exists(FileName))
            {
                JLogger.LogDebug(this, "Config file exist");
                using (var reader = new StreamReader(FileName))
                {
                    JLogger.LogDebug(this, "Opening stream reader {0}", FileName);
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        JLogger.LogDebug(this, "Creating JsonTextReader");
                        jsonReader.SupportMultipleContent = true;
                        while (jsonReader.Read())
                        {
                            JLogger.LogDebug(this, "Config loaded");
                            _jconfigItems = serializer.Deserialize<JConfigItem[]>(jsonReader);
                        }
                    }
                }
            }
            else
            {
                JLogger.LogDebug(this, "Config file doesn't exist, creating a new one");
                _jconfigItems = new JConfigItem[0];
            }
        }

        public void Save()
        {
            JLogger.LogInfo(this, "Save()");
            var serializer = new JsonSerializer();
            lock (ObjectLock)
            {
                using (var writer = new StreamWriter(FileName))
                {
                    JLogger.LogDebug(this, "StreamWriter opened {0}", FileName);
                    var jsonWriter = new JsonTextWriter(writer) {Formatting = Formatting.Indented};
                    serializer.Serialize(jsonWriter, _jconfigItems);
                    JLogger.LogDebug(this, "StreamWriter saved");
                }
            }
        }

        private JConfigItem GetConfItemByKey(string key)
        {
            JLogger.LogInfo(this, "GetConfItemByKey() key:{0}", key);
            var item = _jconfigItems.FirstOrDefault(x => x.Key == key);
            if (item != null)
            {
                JLogger.LogDebug(this, "GetConfItemByKey() value:{0}", item.Value);
                return item;
            }
            return null;
        }

        public void SetValueByKey(string key, string value)
        {
            JLogger.LogInfo(this, "SetValueByKey() key:{0}, value:{1}", key, value);
            var item = _jconfigItems.FirstOrDefault(x => x.Key == key);
            if (item != null)
            {
                JLogger.LogDebug(this, "Item exist change the value", key, value);
                item.Value = value;
            }
            else
            {
                JLogger.LogDebug(this, "Item doesn't exist creating a new one", key, value);
                var list = _jconfigItems.ToList();
                JLogger.LogDebug(this, "Adding item to the list", key, value);
                list.Add(new JConfigItem {Key = key, Value = value});
                JLogger.LogDebug(this, "Setting the value to the item", key, value);
                _jconfigItems = list.ToArray<JConfigItem>();
            }
            JLogger.LogDebug(this, "Saving the new persistence", key, value);
            Save();
        }

        public string GetValueByKey(string key)
        {
            JLogger.LogInfo(this, "GetValueByKey() key:{0}", key);
            var value = _jconfigItems.FirstOrDefault(x => x.Key == key)?.Value;
            JLogger.LogDebug(this, "GetValueByKey() value:{0}", value);
            return value;
        }
    }
}
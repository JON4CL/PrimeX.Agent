using System.Runtime.Serialization;

namespace SRM.Server.Service.DataContract
{
    [DataContract]
    public class ServerCommandRequest
    {
        [DataMember] public string MachineName;

        [DataMember] public string ServiceCommand;

        [DataMember] public string ServiceName;

        [DataMember] public string[] ValueParam;
    }
}
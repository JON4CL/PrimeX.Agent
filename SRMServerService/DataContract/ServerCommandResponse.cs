using System.Runtime.Serialization;

namespace SRM.Server.Service.DataContract
{
    [DataContract]
    public class ServerCommandResponse
    {
        [DataMember] private string _code;

        [DataMember] private string _data;

        public ServerCommandResponse(string code, string data)
        {
            _code = code;
            _data = data;
        }
    }
}
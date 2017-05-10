using System.ServiceModel;
using System.ServiceModel.Web;
using SRM.Server.Service.DataContract;

namespace SRM.Server.Service.ServiceContract
{
    [ServiceContract]
    internal interface IServerCommandService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        ServerCommandResponse[] ExecuteCommand(ServerCommandRequest req);
    }
}
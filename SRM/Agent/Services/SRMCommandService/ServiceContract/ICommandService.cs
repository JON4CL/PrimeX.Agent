using System.ServiceModel;
using SRM.Agent.Services.DataContract;

namespace SRM.Agent.Services.ServiceContract
{
    [ServiceContract]
    public interface ICommandService
    {
        [OperationContract]
        CommandResponse[] ExecuteCommand(CommandRequest req);
    }
}
using SRM.Commons;
using SRM.Server.Service.DataContract;
using SRM.Server.Service.ServiceContract;

namespace SRM.Server.Service
{
    public class ServerCommandService : IServerCommandService
    {
        public ServerCommandResponse[] ExecuteCommand(ServerCommandRequest req)
        {
            JLogger.LogInfo(this, "ExecuteCommand: " + req.ServiceCommand);
            return null;
        }
    }
}
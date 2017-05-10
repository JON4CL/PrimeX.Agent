using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SRM.Agent.Commons;
using SRM.Agent.Services.DataContract;
using SRM.Agent.Services.ServiceContract;
using SRM.Commons;

namespace SRM.Agent.Services
{
    public class CommandService : ICommandService
    {
        public CommandResponse[] ExecuteCommand(CommandRequest req)
        {
            JLogger.LogInfo(this, "ExecuteCommand:{0}", req.ToString());

            CommandResponse[] resp;
            try
            {
                var cmd = GetCommand(req.ServiceName, req.ServiceCommand);
                if (cmd != null)
                {
                    resp = (CommandResponse[]) cmd.RunCommand(req);
                }
                else
                {
                    resp = new[]
                    {
                        new CommandResponse(CommandResponseCodes.ErrorCommandNotFound[0],
                            CommandResponseCodes.ErrorCommandNotFound[1])
                    };
                }
            }
            catch (Exception ex)
            {
                resp = new[]
                {
                    new CommandResponse(CommandResponseCodes.ErrorExceptionFound[0],
                        CommandResponseCodes.ErrorExceptionFound[1] + ex.Message)
                };
            }
            return resp;
        }

        private ICommand GetCommand(string id, string command)
        {
            JLogger.LogInfo(this, "getCommand(): id:{0} command:{1}", id, command);

            var assembly = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\commands\" + id + ".dll");
            JLogger.LogDebug("assembly:{0}", assembly);

            try
            {
                var ptrAssembly = Assembly.LoadFile(assembly);
                //foreach (var item in ptrAssembly.GetTypes())
                //{
                //    if (!item.IsClass) continue;
                //    if (item.GetInterfaces().Contains(typeof (ICommand)))
                //    {
                //        if (((ICommand) Activator.CreateInstance(item)).GetCommandName().ToUpperInvariant() ==
                //            command.ToUpperInvariant())
                //        {
                //            return (ICommand) Activator.CreateInstance(item);
                //        }
                //    }
                //}
                foreach (var item in from item in ptrAssembly.GetTypes()
                                     where item.IsClass
                                     where item.GetInterfaces().Contains(typeof(ICommand))
                                     where string.Equals(((ICommand)Activator.CreateInstance(item)).GetCommandName(), command, StringComparison.InvariantCultureIgnoreCase)
                                     select item)
                {
                    return (ICommand)Activator.CreateInstance(item);
                }
            }
            catch (Exception ex)
            {
                JLogger.LogError(this, "Error trying to LoadAssembly:{0}", ex.Message);
            }

            return null;
        }
    }
}
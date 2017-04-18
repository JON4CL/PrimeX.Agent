using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SRMCommandService
{
    public class CommandService : ICommandService
    {
        public CommandResponse[] ExecuteCommand(CommandRequest req)
        {
            CommandResponse[] resp = null;
            try
            { 
                ICommand cmd = getCommand(req.serviceName, req.serviceCommand);
                if (cmd != null)
                { 
                    resp = cmd.runCommand(req);
                }
                else
                {
                    resp = new CommandResponse[] {
                        new CommandResponse(CommandResponseCodes.ERROR_COMMAND_NOT_FOUND[0], 
                                            CommandResponseCodes.ERROR_COMMAND_NOT_FOUND[1])
                    };
                }
            } catch (Exception ex)
            {
                resp = new CommandResponse[] {
                    new CommandResponse(CommandResponseCodes.ERROR_EXCEPTION_FOUND[0], 
                                        CommandResponseCodes.ERROR_EXCEPTION_FOUND[1] + ex.Message)
                };
            }
            return resp;
        }

        private ICommand getCommand(string id, string command)
        {
            string assembly = Path.GetFullPath(@".\services\" + id + ".dll");
            try
            {
                Assembly ptrAssembly = Assembly.LoadFile(assembly);
                foreach (Type item in ptrAssembly.GetTypes())
                {
                    if (!item.IsClass) continue;
                    if (item.GetInterfaces().Contains(typeof(ICommand)))
                    {
                        if (((ICommand)Activator.CreateInstance(item)).CommandName.ToUpperInvariant() == command.ToUpperInvariant())
                        {
                            return (ICommand)Activator.CreateInstance(item);
                        }
                    }
                }
            } catch (Exception ex)
            {
                throw ex;
            }
            
            return null;
        }
    }
}

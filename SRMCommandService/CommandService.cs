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
            ICommand cmd = getCommand(req.serviceName, req.serviceCommand);
            if (cmd != null)
            { 
                resp = cmd.runCommand(req);
            }
            else
            {
                resp = new CommandResponse[1] { new CommandResponse("E0001", "Command not found.") };
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
                        if (((ICommand)Activator.CreateInstance(item)).CommandName == command)
                        {
                            return (ICommand)Activator.CreateInstance(item);
                        }
                    }
                }
            } catch (Exception ex)
            {
                //NOTHING
            }
            
            return null;
        }
    }
}

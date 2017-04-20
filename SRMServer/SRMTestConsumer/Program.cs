using Newtonsoft.Json;
using SRMFileSystemCommand;
using System;
using System.Collections.Generic;

namespace SRMTestConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CommandService.CommandServiceClient cmdService = new CommandService.CommandServiceClient())
            {
                cmdService.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(10);

                CommandService.CommandRequest req = new CommandService.CommandRequest();
                req.serviceName = DrivesInfoCommand.commandServiceName;
                req.serviceCommand = DrivesInfoCommand.commandName;
                req.numParam = 2;
                req.valueParam = new string[2];
                req.valueParam[0] = "PARAM81726354";
                req.valueParam[1] = "01928374645--";

                CommandService.CommandResponse[] resp = cmdService.ExecuteCommand(req);
                if (resp.Length > 0)
                {
                    Console.WriteLine("RESPUESTA CODIGO: {0}, DATA: {1}", resp[0].code, resp[0].data);
                    List<JDriveInfo> respObject = JsonConvert.DeserializeObject<List<JDriveInfo>>(resp[0].data);
                    Console.WriteLine("respObject[0].DEVICEID " + respObject.ToArray()[0].DEVICEID);
                }
                else
                {
                    Console.WriteLine("ERROR");
                }
            }
            Console.ReadLine();
        }
    }
}

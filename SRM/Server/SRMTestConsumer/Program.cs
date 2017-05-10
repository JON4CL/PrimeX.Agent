using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SRM.Agent.Commands;
using SRMTestConsumer.CommandService;

namespace SRMTestConsumer
{
    internal class Program
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            using (var cmdService = new CommandServiceClient())
            {
                cmdService.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(10);

                var req = new CommandRequest
                {
                    serviceName = DrivesInfoCommand.CommandServiceName,
                    serviceCommand = DrivesInfoCommand.CommandName,
                    numParam = 2,
                    valueParam = new string[2]
                };
                req.valueParam[0] = "PARAM81726354";
                req.valueParam[1] = "01928374645--";

                var resp = cmdService.ExecuteCommand(req);
                if (resp.Length > 0)
                {
                    Console.WriteLine("RESPUESTA CODIGO: {0}, DATA: {1}", resp[0].code, resp[0].data);
                    var respObject = JsonConvert.DeserializeObject<List<JDriveInfo>>(resp[0].data);
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
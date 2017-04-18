using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                req.serviceName = "DrivesInfoCommand";
                req.serviceCommand = "getDrivesInfo";
                req.numParam = 2;
                req.valueParam = new string[2];
                req.valueParam[0] = "PARAM81726354";
                req.valueParam[1] = "01928374645--";

                CommandService.CommandResponse[] resp = cmdService.ExecuteCommand(req);
                if (resp.Length > 0)
                {
                    Console.WriteLine("RESPUESTA CODIGO: {0}, DATA: {1}", resp[0].code, resp[0].data);
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

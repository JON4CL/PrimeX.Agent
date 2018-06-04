using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SRM.Agent.Commons
{
    public class SRMServerAccess
    {
        public SRMServerAccess()
        {
            
        }

        public int EnrollMachine(Machine machine)
        {
            var hostName = Dns.GetHostName();
            var ipHostInfo = Dns.GetHostEntry(hostName);
            var ipAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));

            var client = new RestClient("http://localhost:3000");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("agent/enrollmachine", Method.POST);
            request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            
            // add parameters for all properties on an object
            request.AddObject(object);

            // or just whitelisted properties
            request.AddObject(object, "PersonId", "Name", ...);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            request.AddFile("file", path);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse<Person> response2 = client.Execute<Person>(request);
            var name = response2.Data.Name;

            // or download and save file to disk
            client.DownloadData(request).SaveAs(path);

            // easy async support
            client.ExecuteAsync(request, response => {
                Console.WriteLine(response.Content);
            });

            // async with deserialization
            var asyncHandle = client.ExecuteAsync<Person>(request, response => {
                Console.WriteLine(response.Data.Name);
            });

            return _factHandlerClient.EnrollMachine(new Machine() { Ip = ipAddress, Name = hostName });
        }

        public Task<int> EnrollMachineAsync(Machine machine)
        {
            return Task.Run(() => EnrollMachine(machine));
        }

        public bool ProcessFact(Fact fact)
        {
            return _factHandlerClient.ProcessFact(fact);
        }

        public Task<bool> ProcessFactAsync(Fact fact)
        {
            return Task.Run(() => ProcessFact(fact));
        }

        public Task<MachineConfiguration> GetMachineConfigurationAsync(Machine machine)
        {
            return Task.Run(() => GetMachineConfiguration(machine));
        }

        public MachineConfiguration GetMachineConfiguration(Machine machine)
        {
            throw new NotImplementedException();
        }
    }
}

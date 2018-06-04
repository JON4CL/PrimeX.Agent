using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using SRM.Commons;

namespace SRM.Agent
{
    public static class JMachine
    {
        private static int _id;

        public static int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static string Ip
        {
            get
            {
                var ipHostInfo = Dns.GetHostEntry(Name);
                return Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));
            }
        }

        public static string Name => Dns.GetHostName();

        public static Machine GetMachine()
        {
            var machine = new Machine()
            {
                Ip = Ip,
                Name = Name
            };
            return machine;
        }
    }
}

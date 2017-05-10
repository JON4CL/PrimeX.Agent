using System.Runtime.Serialization;
using System.Text;
using SRM.Agent.Commons;

namespace SRM.Agent.Services.DataContract
{
    [DataContract]
    public class CommandRequest : ICommandRequest
    {
        [DataMember] public byte[][] BinaryParams;

        [DataMember] public string[] Params;

        [DataMember] public string ServiceCommand;

        [DataMember] public string ServiceName;

        [DataMember] public int[] SizeBinaryParams;

        public string GetServiceName()
        {
            return ServiceName;
        }

        public string GetServiceCommand()
        {
            return ServiceCommand;
        }

        public string[] GetParams()
        {
            return Params;
        }

        public int[] GetSizeBinaryParams()
        {
            return SizeBinaryParams;
        }

        public byte[][] GetBinaryParams()
        {
            return BinaryParams;
        }

        public override string ToString()
        {
            var sbParams = new StringBuilder();
            sbParams.Append("[");
            foreach (var param in Params)
            {
                sbParams.AppendFormat("{0},", param);
            }
            sbParams.Remove(sbParams.Length - 1, 1);
            sbParams.Append("]");

            var sbSizeBinaryParams = new StringBuilder();
            sbSizeBinaryParams.Append("[");
            foreach (var sizeBinaryParams in SizeBinaryParams)
            {
                sbSizeBinaryParams.AppendFormat("{0},", sizeBinaryParams);
            }
            sbSizeBinaryParams.Remove(sbSizeBinaryParams.Length - 1, 1);
            sbSizeBinaryParams.Append("]");
            string format = "{\"ServiceName\":\"{0}\",\"ServiceCommand\":\"{1}\",\"Params\":\"{2}\"}";
            return string.Format(format, ServiceName, ServiceCommand, sbParams);
        }
    }
}
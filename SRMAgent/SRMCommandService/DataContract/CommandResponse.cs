using System.Runtime.Serialization;
using System.Text;
using SRM.Agent.Commons;

namespace SRM.Agent.Services.DataContract
{
    [DataContract]
    public class CommandResponse : ICommandResponse
    {
        [DataMember] private byte[][] _binaryData;

        [DataMember] private string _code;

        [DataMember] private string _data;

        [DataMember] private int[] _sizeBinaryData;

        public CommandResponse(string code, string data)
        {
            _code = code;
            _data = data;
            _sizeBinaryData = null;
            _binaryData = null;
        }

        public CommandResponse(string code, int[] sizeBinaryData, byte[][] binaryData)
        {
            _code = code;
            _data = null;
            _sizeBinaryData = (int[]) sizeBinaryData.Clone();
            _binaryData = (byte[][]) binaryData.Clone();
        }

        public CommandResponse(string code, string data, int[] sizeBinaryData, byte[][] binaryData)
        {
            _code = code;
            _data = data;
            _sizeBinaryData = (int[]) sizeBinaryData?.Clone();
            _binaryData = (byte[][]) binaryData?.Clone();
        }

        public string GetCode()
        {
            return _code;
        }

        public string GetData()
        {
            return _data;
        }

        public int[] GetSizeBinaryData()
        {
            return _sizeBinaryData;
        }

        public byte[][] GetBinaryData()
        {
            return _binaryData;
        }

        public override string ToString()
        {
            var sbSizeBinaryParams = new StringBuilder();
            sbSizeBinaryParams.Append("[");
            foreach (var sizeBinaryData in _sizeBinaryData)
            {
                sbSizeBinaryParams.AppendFormat("{0},", sizeBinaryData);
            }
            sbSizeBinaryParams.Remove(sbSizeBinaryParams.Length - 1, 1);
            sbSizeBinaryParams.Append("]");
            string format = "{\"Code\":\"{0}\",\"Data\":\"{1}\",\"SizeBinaryData\":{2}}";
            return string.Format(format, _code, _data, sbSizeBinaryParams);
        }
    }
}
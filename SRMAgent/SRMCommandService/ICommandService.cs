using System.Runtime.Serialization;
using System.ServiceModel;

namespace SRMCommandService
{
    [ServiceContract]
    public interface ICommandService
    {
        [OperationContract]
        CommandResponse[] ExecuteCommand(CommandRequest req);
    }

    [DataContract]
    public class CommandRequest
    {
        [DataMember]
        public string serviceName;

        [DataMember]
        public string serviceCommand;

        [DataMember]
        public int numParam;

        [DataMember]
        public string[] valueParam;

        [DataMember]
        public int numBinaryParam;

        [DataMember]
        public int[] sizeBinaryParam;

        [DataMember]
        public byte[][] binaryParam;
    }

    [DataContract]
    public class CommandResponse
    {
        [DataMember]
        string code;

        [DataMember]
        string data;

        [DataMember]
        int numBinaryData;

        [DataMember]
        int[] sizeBinaryData;

        [DataMember]
        byte[][] binaryData;

        public CommandResponse(string Code, string Data)
        {
            code = Code;
            data = Data;
            numBinaryData = 0;
            sizeBinaryData = null;
            binaryData = null;
        }

        public CommandResponse(string Code, int NumBinaryData, int[] SizeBinaryData, byte[][] BinaryData)
        {
            code = Code;
            data = null;
            numBinaryData = NumBinaryData;
            sizeBinaryData = (int[])SizeBinaryData.Clone();
            binaryData = (byte[][])BinaryData.Clone();
        }

        public CommandResponse(string Code, string Data, int NumBinaryData, int[] SizeBinaryData, byte[][] BinaryData)
        {
            code = Code?.ToString();
            data = Data?.ToString();
            numBinaryData = NumBinaryData;
            sizeBinaryData = (int[])SizeBinaryData?.Clone();
            binaryData = (byte[][])BinaryData?.Clone();
        }
    }
}

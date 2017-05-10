namespace SRM.Agent.Commons
{
    public interface ICommandResponse
    {
        string GetCode();
        string GetData();
        int[] GetSizeBinaryData();
        byte[][] GetBinaryData();
    }
}
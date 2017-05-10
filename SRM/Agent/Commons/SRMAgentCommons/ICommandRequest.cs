namespace SRM.Agent.Commons
{
    public interface ICommandRequest
    {
        string GetServiceName();
        string GetServiceCommand();
        string[] GetParams();
        int[] GetSizeBinaryParams();
        byte[][] GetBinaryParams();
    }
}
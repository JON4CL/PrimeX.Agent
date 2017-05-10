namespace SRM.Agent.Commons
{
    public interface ICommand
    {
        string GetCommandServiceName();
        string GetCommandName();
        string GetCommandDescription();
        string GetCommandVersion();
        ICommandResponse[] RunCommand(ICommandRequest request);
    }
}
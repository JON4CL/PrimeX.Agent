namespace SRMCommandService
{
    public interface ICommand
    {
        string CommandName { get; }
        string CommandDescription { get; }
        string CommandVersion { get; }

        CommandResponse[] runCommand(CommandRequest request);
    }
}

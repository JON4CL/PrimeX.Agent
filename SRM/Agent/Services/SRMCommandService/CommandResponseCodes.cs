namespace SRM.Agent.Services
{
    public class CommandResponseCodes
    {
        public static readonly string[] ErrorCommandNotFound = {"E0001", "Command not found."};
        public static readonly string[] ErrorExceptionFound = {"E0002", "Exception found. "};
        public static readonly string[] OkCommandSuccess = {"O0000", "Command executed successfully. "};

        private CommandResponseCodes()
        {
        }
    }
}
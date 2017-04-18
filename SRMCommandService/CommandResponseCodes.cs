using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMCommandService
{
    public class CommandResponseCodes
    {
        public static readonly string[] ERROR_COMMAND_NOT_FOUND = { "E0001", "Command not found." };
        public static readonly string[] ERROR_EXCEPTION_FOUND = { "E0002", "Exception found. " };

        public static readonly string[] OK_COMMAND_SUCCESS = { "O0000", "Command executed successfully. " };
    }
}

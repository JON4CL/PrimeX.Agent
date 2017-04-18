using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRMCommandService;
using System.IO;
using Newtonsoft.Json;

namespace SRMFileSystemCommand
{
    public class DrivesInfoCommand : ICommand
    {
        private string commandName = "getDrivesInfo";
        private string commandDescription = "Get the drives information in a JSON format.";
        private string commandVersion = "0.0.1";

        private const string NO_EMPTY_DRIVES = "NO_EMPTY_DRIVES";
        private const string NO_REMOVABLE_DRIVES = "NO_REMOVABLE_DRIVES";

        public string CommandName
        {
            get { return commandName; }
        }

        public string CommandDescription
        {
            get { return commandDescription; }
        }

        public string CommandVersion
        {
            get { return commandVersion; }
        }

        public CommandResponse[] runCommand(CommandRequest request)
        {
            // ----------------------------------------------------------------
            // THIS COMMAND LIST ALL THE AVAILABLE DRIVES IN THE MACHINE
            // ----------------------------------------------------------------
            // POSSIBLE PARAMS:
            //     NO_EMPTY_DRIVES     - DON'T RETURN EMPTY DRIVES
            //     NO_REMOVABLE_DRIVES - DON'T RETURN REMOVABLE DRIVES
            // ----------------------------------------------------------------

            //request.valueParam.Contains<string>("NO_EMPTY_DRIVES");

            DriveInfo[] di = DriveInfo.GetDrives();
            string json = JsonConvert.SerializeObject(di);

            return new CommandResponse[1] { new CommandResponse("O0001", json) };
        }
    }
}

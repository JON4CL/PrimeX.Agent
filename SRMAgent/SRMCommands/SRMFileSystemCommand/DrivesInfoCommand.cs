using System;
using System.Collections.Generic;
using SRMCommandService;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace SRMFileSystemCommand
{
    public class JDriveInfo
    {
        public string DEVICEID = "";
        public string FREESPACE = "";
        public string TOTALSIZE = "";
        public string USEDSPACE = "";
    }

    public class DrivesInfoCommand : ICommand
    {
        public static string commandServiceName = "SRMFileSystemCommand";
        public static string commandName = "GetDrivesInfo";
        public static string commandDescription = "Get the drives information in a JSON format.";
        public static string commandVersion = "0.0.1";

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
            CommandResponse[] cmdResp = null;
            List<JDriveInfo> driveInfoList = new List<JDriveInfo>();
            try
            {
                string respData = "";
                
                foreach (DriveInfo di in DriveInfo.GetDrives())
                {
                    if (!di.IsReady)
                    {
                        continue;
                    }
                    if (di.DriveType != DriveType.Fixed && request.valueParam.Contains<string>("NO_REMOVABLE_DRIVES") == true)
                    {
                        continue;
                    }
                    if (di.TotalSize == 0 && request.valueParam.Contains<string>("NO_EMPTY_DRIVES") == true)
                    {
                        continue;
                    }
                    var data = new JDriveInfo()
                    {
                        DEVICEID = di.Name,
                        FREESPACE = di.TotalFreeSpace.ToString(),
                        TOTALSIZE = di.TotalSize.ToString(),
                        USEDSPACE = (di.TotalSize - di.TotalFreeSpace).ToString()
                    };
                    driveInfoList.Add(data);
                }

                respData = JsonConvert.SerializeObject(driveInfoList.ToArray());
                cmdResp = new CommandResponse[1] {
                    new CommandResponse(CommandResponseCodes.OK_COMMAND_SUCCESS[0],
                                        respData)
                };
            }
            catch (Exception ex)
            {
                cmdResp = new CommandResponse[1] {
                    new CommandResponse(CommandResponseCodes.ERROR_EXCEPTION_FOUND[0], 
                                        CommandResponseCodes.ERROR_EXCEPTION_FOUND[1] + ex.Message)
                };
            }

            return cmdResp;
        }
    }
}

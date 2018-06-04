using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SRM.Agent.Commons;
using SRM.Agent.Services;
using SRM.Agent.Services.DataContract;
using SRM.Commons;

namespace SRM.Agent.Commands
{
    public class DrivesInfoCommand : ICommand
    {
        //PARAMETERS
        private const string NoEmptyDrives = "NO_EMPTY_DRIVES";
        private const string NoRemovableDrives = "NO_REMOVABLE_DRIVES";

        public static string CommandServiceName = "SRMFileSystemCommand";
        public static string CommandName = "GetDrivesInfo";
        public static string CommandDescription = "Get the drives information in a JSON format.";
        public static string CommandVersion = "0.0.1";
        // ----------------------------------------------------------------
        // THIS COMMAND LIST ALL THE AVAILABLE DRIVES IN THE MACHINE
        // ----------------------------------------------------------------
        public ICommandResponse[] RunCommand(ICommandRequest request)
        {
            JLogger.LogInfo(this, "runCommand:{0}", request.ToString());

            ICommandResponse[] cmdResp;
            var driveInfoList = new List<JDriveInfo>();
            try
            {
                //foreach (var di in DriveInfo.GetDrives())
                //{
                //    if (!di.IsReady)
                //    {
                //        continue;
                //    }

                //    //NO_REMOVABLE_DRIVES - DON'T RETURN REMOVABLE DRIVES
                //    if (di.DriveType != DriveType.Fixed && request.GetParams().Contains("NO_REMOVABLE_DRIVES"))
                //    {
                //        continue;
                //    }

                //    //NO_EMPTY_DRIVES - DON'T RETURN EMPTY DRIVES
                //    if (di.TotalSize == 0 && request.GetParams().Contains("NO_EMPTY_DRIVES"))
                //    {
                //        continue;
                //    }
                //    var data = new JDriveInfo
                //    {
                //        DEVICEID = di.Name,
                //        FREESPACE = di.TotalFreeSpace.ToString(),
                //        TOTALSIZE = di.TotalSize.ToString(),
                //        USEDSPACE = (di.TotalSize - di.TotalFreeSpace).ToString()
                //    };
                //    driveInfoList.Add(data);
                //}

                driveInfoList.AddRange(from di in DriveInfo.GetDrives()
                    where di.IsReady
                    where di.DriveType == DriveType.Fixed || !request.GetParams().Contains(NoRemovableDrives)
                    where di.TotalSize != 0 || !request.GetParams().Contains(NoEmptyDrives)
                    select new JDriveInfo
                    {
                        DEVICEID = di.Name, FREESPACE = di.TotalFreeSpace.ToString(), TOTALSIZE = di.TotalSize.ToString(), USEDSPACE = (di.TotalSize - di.TotalFreeSpace).ToString()
                    });

                var respData = JsonConvert.SerializeObject(driveInfoList.ToArray());
                cmdResp = new ICommandResponse[]
                {
                    new CommandResponse(CommandResponseCodes.OkCommandSuccess[0],
                        respData)
                };
            }
            catch (Exception ex)
            {
                cmdResp = new ICommandResponse[]
                {
                    new CommandResponse(CommandResponseCodes.ErrorExceptionFound[0],
                        CommandResponseCodes.ErrorExceptionFound[1] + ex.Message)
                };
            }

            JLogger.LogInfo(this, "runCommand Response:{0}", cmdResp.ToString());

            return cmdResp;
        }

        public string GetCommandName()
        {
            return CommandName;
        }

        public string GetCommandDescription()
        {
            return CommandDescription;
        }

        public string GetCommandVersion()
        {
            return CommandVersion;
        }

        public string GetCommandServiceName()
        {
            return CommandServiceName;
        }
    }
}
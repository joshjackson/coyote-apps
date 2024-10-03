// 
//  SystemInfo.cs
//  
//  Author:
//       Joshua Jackson <jjackson@vortech.net>
// 
//  Date:
//      10/3/2024        
//
//  Product:
//       Coyote Linux https://www.coyotelinux.com
// 	
//  Copyright (c) 1999-2024 Vortech Consulting, LLC, All rights reserved
//
//  This file is part of the Coyote Linux distribution. Please see the Coyote
//  Linux web site for usage and licensing information.

using CoyoteLinux.SysUtils;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class SystemInfo {

        public string Hostname { get; set; }
        public string FirmwareVersion { get; set; }
        public DateTime FirmwareBuildDate { get; set; }
        public string KernelVersion { get; set; }
        public string LoaderVersion { get; set; }
        public int ConfigVersion { get; set; }
        public string SystemGroupName { get; set; }
        public Guid SystemGroupID { get; set; }
        public string CoyoteBaseDir { get; set; }

        public SystemInfo() {
            Hostname = "coyote";
            FirmwareVersion = SysProc.GetCoyoteVersion();
            FirmwareBuildDate = DateTime.Now;
            KernelVersion = Environment.OSVersion.Version.ToString();
            LoaderVersion = "4.0.100";
            ConfigVersion = 1;
            SystemGroupID = CoyoteConstants.SystemGroupID;
            SystemGroupName = CoyoteConstants.SystemGroupName;

            CoyoteBaseDir = CoyoteConstants.COYOTE_SYSTEM_BASE_DIR;
        }

    }
}

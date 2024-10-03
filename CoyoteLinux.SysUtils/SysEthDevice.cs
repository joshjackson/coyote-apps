// 
//  SysExec.cs
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

namespace CoyoteLinux.SysUtils {
    public class SysEthDevice {

        public string DeviceName { get; private set; }
        public string DevicePath {
            get {
                return "/sys/class/net/" + DeviceName;
            }
        }

        public SysEthDevice(string dev) {
            //if (!Directory.Exists("/sys/class/net/" + dev)) {
            //    throw new Exception("SysEthDevice(): The specified device " + dev + " does not appear to be present in the system");
            //}

            DeviceName = dev;
        }


        public string HardwareAddress() {
            return File.ReadAllText(DevicePath + "/address");
        }

        public string Speed() {
            if (IsLinkUp()) {
                return File.ReadAllText(DevicePath + "/speed");
            } else {
                return "unknown";
            }
        }

        public bool IsLinkUp() {
            //            string ret = File.ReadAllText(DevicePath + "/operstate");
            string ret = "up";
            return (ret.Contains("up"));
        }
    }
}

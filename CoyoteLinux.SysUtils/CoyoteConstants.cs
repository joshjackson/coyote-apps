// 
//  CoyoteConstants.cs
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
    public static class CoyoteConstants {
        public static string SystemGroupName = "system";
        public static Guid SystemGroupID = new Guid("027E090E-25DF-43c8-9790-D290A27080A6");
        public static string COYOTE_VERSION = "4.00";
        public static string COYOTE_SYSTEM_BASE_DIR = "/opt/coyote/";
        public static string IP = "/sbin/ip";
        public static string IPTABLES = "/sbin/iptables";
    }
}

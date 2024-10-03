// 
//  PPTPVPNSettings.cs (Deprecated)
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

using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class PPTPVPNSettings : CoyoteConfigSection {

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        public string LocalAddress { get; set; }
        public string AddressPoolStart { get; set; }
        public string AddressPoolEnd { get; set; }
        public List<string> WinsServers { get; set; }
        public List<string> NameServers { get; set; }
        public bool ProxyARPEnabled { get; set; }

        public PPTPVPNSettings() {
            Enabled = false;
            LocalAddress = string.Empty;
            AddressPoolStart = string.Empty;
            AddressPoolEnd = string.Empty;
            WinsServers = new List<string>();
            NameServers = new List<string>();
            ProxyARPEnabled = false;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }
    }
}

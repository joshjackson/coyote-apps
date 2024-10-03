// 
//  SyslogSettings.cs
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
    public class SyslogSettings : CoyoteConfigSection {

        [XmlAttribute("remote")]
        public bool RemoteSyslog { get; set; }
        [XmlAttribute("remoteHost")]
        public string RemoteSyslogServer { get; set; }

        public bool LogLocalRejects { get; set; }
        public bool LogLocalAccepts { get; set; }
        public bool LogForwardRejects { get; set; }
        public bool LogForwardAccepts { get; set; }

        public SyslogSettings() {
            RemoteSyslog = false;
            RemoteSyslogServer = String.Empty;
        }
    }
}

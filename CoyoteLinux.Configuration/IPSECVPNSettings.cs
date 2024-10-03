// 
//  IPSECVPNSettings.cs
//  
//  Author:
//      Joshua Jackson <jjackson@vortech.net>
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
    public class IPSECTunnel {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string TunnelName { get; set; }

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        public IPSECTunnel() {
            Enabled = false;
            TunnelName = String.Empty;
            id = Guid.NewGuid();
        }
    }

    [Serializable]
    public class IPSECVPNSettings {

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        [XmlArray("Tunnels")]
        [XmlArrayItem("Tunnel")]
        public List<IPSECTunnel> Tunnels { get; set; }


        public IPSECVPNSettings() {
            Enabled = false;
            Tunnels = new List<IPSECTunnel>();
        }
    }
}

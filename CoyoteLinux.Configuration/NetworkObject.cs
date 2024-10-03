// 
//  NetworkObject.cs
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
    public enum NetworkObjectClass {
        Host,
        Network,
        VirtualServer
    }

    [Serializable]
    public class NetworkObject {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string ObjectName { get; set; }

        [XmlAttribute("owner")]
        public string ObjectOwner { get; set; }

        [XmlAttribute("class")]
        public NetworkObjectClass ObjectClass { get; set; }

        public string Address { get; set; }
        public string Netmask { get; set; }

        public NetworkObject() {
            Address = String.Empty;
            Netmask = String.Empty;
            ObjectName = String.Empty;
            ObjectOwner = String.Empty;
            id = Guid.NewGuid();
        }
    }
}

// 
//  NetworkObjectGroup.cs
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
    public class NetworkObjectGroup {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string GroupName { get; set; }

        [XmlAttribute("owner")]
        public string Owner { get; set; }

        [XmlArray()]
        [XmlArrayItem("Member")]
        public List<String> ObjectList { get; set; }

        public NetworkObjectGroup() {
            ObjectList = new List<string>();
            id = Guid.NewGuid();
            GroupName = string.Empty;
            Owner = string.Empty;

        }
    }
}

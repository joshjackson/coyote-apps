// 
//  NetworkRoutes.cs
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
    public class NetworkRoute : CoyoteConfigSection {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string RouteName { get; set; }

        public bool Enabled { get; set; }
        public string Destination { get; set; }
        public string DestinationPrefix { get; set; }
        public string Gateway { get; set; }
        public int Metric { get; set; }
        public string Device { get; set; }

        public NetworkRoute() {
            Enabled = true;
            Metric = 1;
            id = Guid.NewGuid();
            RouteName = string.Empty;
            Destination = string.Empty;
            DestinationPrefix = string.Empty;
            Gateway = string.Empty;
            Device = string.Empty;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }

    }
}

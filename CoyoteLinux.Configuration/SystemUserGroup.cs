// 
//  SystemUserGroup.cs
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
    public class SystemUserGroup : CoyoteConfigSection {

        [XmlAttribute("id")]
        public Guid ID;
        public string GroupName;

        public bool Enabled;

        public SystemUserGroup() {
            ID = Guid.NewGuid();
            GroupName = string.Empty;
            Enabled = false;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }

    }
}

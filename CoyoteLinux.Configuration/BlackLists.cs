// 
//  BlackLists.cs
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
    public class BlackList {

        public Guid id { get; set; }
        public bool enabled { get; set; }
        public string Comment { get; set; }
        public Guid Group { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }

        public BlackList() {
            id = Guid.NewGuid();
            enabled = false;
            Comment = String.Empty;
            Group = CoyoteConstants.SystemGroupID;
            Source = String.Empty;
            Destination = String.Empty;
        }

    }
}

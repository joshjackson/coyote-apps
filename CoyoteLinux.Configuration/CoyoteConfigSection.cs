﻿// 
//  ConfigSection.cs
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
namespace CoyoteLinux.Configuration {
    public abstract class CoyoteConfigSection : ICoyoteConfigSection {

        public virtual string GenerateConfigText() {
            return "";
        }

    }

    public interface ICoyoteConfigSection {

        string GenerateConfigText();

    }
}

// 
//  NetworkConfigFrame.cs
//  
//  Author:
//       Joshua Jackson <jjackson@vortech.net>
// 
//  Product:
//       Coyote Linux
// 	
//  Copyright (c) 2011 Vortech Consulting, LLC, All rights reserved
// 
//  This file is part of the Coyote Linux distribution and is covered under the
//  Vortech Software Use and Distribution License. Use and/or distrbution of
//  this file without prior written consent from Vortech Consulting, LLC is in 
//  violation of United States and International copyright laws.
using System;
using CoyoteLinux.Terminal;

namespace CoyoteConsole {
    public class NetworkConfigFrame : CoyoteConsoleFrame {

        public NetworkConfigFrame() : base("Network Configuration", CoyoteMenuType.NETWORK) {
        }

        public bool InterfaceConfiguration() {
            return false;
        }

        public override void GetMenuItems() {
            menu.AddHandler(CoyoteMenuItemType.NETWORK_INTERFACES, InterfaceConfiguration);

        }
    }
}


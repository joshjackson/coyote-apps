// 
//  AdminConsoleFrame.cs
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
using System.Collections.Generic;
using Mono.Terminal;

namespace CoyoteConsole {

    public class AdminConsoleFrame : CoyoteConsoleFrame {

        public bool NetworkConfiguration() {
            RunState.PushState(ConsoleRunState.rsNetConfig);
            return false;
        }

        public bool SystemMonitor() {
            RunState.PushState(ConsoleRunState.rsAdminMonitor);
            return false;
        }

        public bool Lockdown() {
            if (Application.Confirm("System Lockdown", "Are you sure you want to put the system in lockdown mode?")) {
                RunState.SetState(ConsoleRunState.rsReboot);
            }
            return false;
        }

        public bool FirmwareUpdates() {
            return true;
        }

        public bool RebootSystem() {
            if (Application.Confirm("System Reboot", "Are you sure you wish to reboot the system?")) {
                RunState.SetState(ConsoleRunState.rsReboot);
                return false;
            }

            return true;
        }

        public bool ShutdownSystem() {
            if (Application.Confirm("System Shutdown", "Are you sure you wish to halt the system?")) {
                RunState.SetState(ConsoleRunState.rsReboot);
                return false;
            }

            return true;
        }

        public AdminConsoleFrame() : base("Admin Menu", CoyoteMenuType.ADMIN) {
        }

        public override void GetMenuItems() {
            menu.AddHandler(CoyoteMenuItemType.ADMIN_NETWORKING, NetworkConfiguration);
            menu.AddHandler(CoyoteMenuItemType.ADMIN_MONITOR, SystemMonitor);
            menu.AddHandler(CoyoteMenuItemType.ADMIN_LOCKDOWN, Lockdown);
            menu.AddHandler(CoyoteMenuItemType.ADMIN_FIRMWARE, FirmwareUpdates);
            menu.AddHandler(CoyoteMenuItemType.ADMIN_REBOOT, RebootSystem);
            menu.AddHandler(CoyoteMenuItemType.ADMIN_SHUTDOWN, ShutdownSystem);
        }

    }
}


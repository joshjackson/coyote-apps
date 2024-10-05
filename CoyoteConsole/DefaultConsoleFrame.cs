// 
//  DefaultConsoleFrame.cs
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
using Mono.Terminal;
using CoyoteLinux.SysUtils;

namespace CoyoteConsole {

    public class DefaultConsoleFrame : Frame {


        public DefaultConsoleFrame() : base(0, 0, Application.Cols, Application.Lines, null) {
            Add(new Label(10, 3, String.Format("Coyote Linux Version:  {0}", "4.00.1234")));
            Add(new Label(10, 5, String.Format("System Kernel Version: {0}", Environment.OSVersion.VersionString.Remove(0, 5))));
            Add(new Label(10, 6, String.Format("CLR Execution Engine:  {0}", Environment.Version)));
            Add(new Label(10, 8, String.Format("Processors: {0}x {1}  ", Environment.ProcessorCount, SysProc.GetProcessorModel())));
            Add(new Label(10, 9, String.Format("System RAM:   ", SysProc.GetTotalSystemRAM())));
            Add(new Label(1, 21, "<F2> Login"));
            Add(new Label(56, 21, "<F10> Shutdown/Reboot"));
            RunState.ResetState();
        }

        public override bool ProcessKey(int key) {
            switch (key) {
                case Curses.KeyF2:
                    // Admin Login
                    ConsoleLogin login = new ConsoleLogin();
                    if (login.Run()) {
                        RunState.SetState(ConsoleRunState.rsAdmin);
                        Running = false;
                    }
                    Redraw();
                    return true;
                case Curses.KeyF10:
                    ConsoleLogin shutdown = new ConsoleLogin();
                    if (shutdown.Run()) {
                        RunState.SetState(ConsoleRunState.rsShutdown);
                        Running = false;
                    }
                    return true;
            }

            return base.ProcessKey(key);
        }

    }
}


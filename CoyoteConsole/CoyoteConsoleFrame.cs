// 
//  CoyoteConsoleFrame.cs
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
    public abstract class CoyoteConsoleFrame : Frame {

        protected CoyoteConsoleMenu menu;

        public CoyoteConsoleFrame(string Title, CoyoteMenuType MenuType) : base(0, 0, Application.Cols, Application.Lines, Title) {
            menu = new CoyoteConsoleMenu(MenuType);
            Add(new Label(1, 21, String.Format("<ESC> {0}", (RunState.Previous == ConsoleRunState.rsDefault) ? "Logout" : "Exit Menu")));

            GetMenuItems();

            ListView lv = new ListView(1, 1, 25, 15, menu);
            Add(lv);
        }

        public abstract void GetMenuItems();

        public override void Redraw() {
            base.Redraw();
            for (int i = 1; i < Application.Lines - 1; i++) {
                Curses.move(i, 30);
                Curses.addch(Curses.ACS_VLINE);
            }

        }

        public override bool ProcessKey(int key) {
            if (key == 27) {
                Running = false;
                RunState.PopState();
                return true;
            }

            if ((key == 10) || (key == 13)) {
                if (menu.items[menu.view.Selected].Action != null) {
                    Running = menu.items[menu.view.Selected].Action();
                }
                return true;
            }

            return base.ProcessKey(key);
        }


    }
}


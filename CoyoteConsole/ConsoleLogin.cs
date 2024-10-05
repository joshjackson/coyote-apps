// 
//  ConsoleLogin.cs
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

namespace CoyoteConsole {
    public class ConsoleLogin : Dialog {
        private Entry UsernameEntry;
        private Entry PasswordEntry;
        private bool DoCancel = false;

        public ConsoleLogin() : base(46, 9, "System Login") {

            Add(new Label(1, 1, "Username:"));
            Add(new Label(1, 2, "Password:"));
            UsernameEntry = new Entry(11, 1, 30, String.Empty);
            PasswordEntry = new Entry(11, 2, 30, String.Empty);
            PasswordEntry.Secret = true;
            Add(UsernameEntry);
            Add(PasswordEntry);

            Button bOk = new Button("Login", true);
            bOk.Clicked += HandleBOkClicked;
            Button bCancel = new Button("Cancel");
            bCancel.Clicked += delegate {
                DoCancel = true;
                bCancel.Container.Running = false;
            };
            AddButton(bOk);
            AddButton(bCancel);
        }

        void HandleBOkClicked(object sender, EventArgs e) {



            ((Button)sender).Container.Running = false;
        }

        public override bool ProcessKey(int key) {
            if (key == 27) {
                DoCancel = true;
            }
            return base.ProcessKey(key);
        }

        public bool Run() {
            DoCancel = false;
            Application.Run(this);
            return !DoCancel;
        }
    }
}


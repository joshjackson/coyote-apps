// 
//  SystemUser.cs
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
    public enum UserAccessRole {
        User,
        GroupAdmin,
        Administrator
    }

    [Serializable]
    public class UserPassword {
        [XmlAttribute("encrypted")]
        public bool Encrypted;
        [XmlText]
        public string Value;

        public UserPassword() {
            Encrypted = false;
            Value = string.Empty;
        }
    }

    [Serializable]
    public class SystemUser : Object {

        [XmlAttribute("id")]
        public Guid id;

        [XmlAttribute("name")]
        public string Username;
        public UserPassword Password;
        public string FullName;
        public string EmailAddress;
        public bool IsAdmin;
        public UserAccessRole AccessRole;
        public bool PPTPAccess;

        public Guid UserGroup;

        public SystemUser() {
            Password = new UserPassword();
            id = Guid.NewGuid();
            PPTPAccess = false;
            AccessRole = UserAccessRole.User;
            Username = string.Empty;
            EmailAddress = string.Empty;
            IsAdmin = false;
            AccessRole = UserAccessRole.GroupAdmin;
            FullName = string.Empty;
        }
    }
}

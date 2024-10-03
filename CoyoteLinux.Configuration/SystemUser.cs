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
        }
    }
}

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
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }

    }
}

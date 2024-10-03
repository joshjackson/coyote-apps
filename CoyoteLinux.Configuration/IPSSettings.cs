using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class IPSSettings {

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        public IPSSettings() {
            Enabled = false;
        }
    }
}

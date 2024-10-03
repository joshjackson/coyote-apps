using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class PPTPVPNSettings : CoyoteConfigSection {

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        public string LocalAddress { get; set; }
        public string AddressPoolStart { get; set; }
        public string AddressPoolEnd { get; set; }
        public List<string> WinsServers { get; set; }
        public List<string> NameServers { get; set; }
        public bool ProxyARPEnabled { get; set; }

        public PPTPVPNSettings() {
            Enabled = false;
            WinsServers = new List<string>();
            NameServers = new List<string>();
            ProxyARPEnabled = false;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }
    }
}

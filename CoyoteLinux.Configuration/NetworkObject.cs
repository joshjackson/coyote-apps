using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public enum NetworkObjectClass {
        Host,
        Network,
        VirtualServer
    }

    [Serializable]
    public class NetworkObject {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string ObjectName { get; set; }

        [XmlAttribute("owner")]
        public string ObjectOwner { get; set; }

        [XmlAttribute("class")]
        public NetworkObjectClass ObjectClass { get; set; }

        public string Address { get; set; }
        public string Netmask { get; set; }

        public NetworkObject() {
            id = Guid.NewGuid();
        }
    }
}

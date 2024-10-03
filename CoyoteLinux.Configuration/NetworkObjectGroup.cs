using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class NetworkObjectGroup {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string GroupName { get; set; }

        [XmlAttribute("owner")]
        public string Owner { get; set; }

        [XmlArray()]
        [XmlArrayItem("Member")]
        public List<String> ObjectList { get; set; }

        public NetworkObjectGroup() {
            ObjectList = new List<string>();
            id = Guid.NewGuid();
        }
    }
}

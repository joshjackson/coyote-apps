using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class NetworkRoute : CoyoteConfigSection {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string RouteName { get; set; }

        public bool Enabled { get; set; }
        public string Destination { get; set; }
        public string DestinationPrefix { get; set; }
        public string Gateway { get; set; }
        public int Metric { get; set; }
        public string Device { get; set; }

        public NetworkRoute() {
            Enabled = true;
            Metric = 1;
            id = Guid.NewGuid();
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }

    }
}

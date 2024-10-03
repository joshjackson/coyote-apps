using CoyoteLinux.SysUtils;
using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public enum TrafficACLTarget {
        Accept,
        Drop,
        Reject,
        Tarpit
    }

    [Serializable]
    public enum TrafficACLParent {
        INPUT,
        FORWARD,
        OUTPUT,
        PREROUTING,
        POSTROUTING
    }

    [Serializable]
    public enum TrafficACLTable {
        filter,
        nat,
        mangle
    }

    [Serializable]
    public class TrafficACLRule : CoyoteConfigSection {
        [XmlAttribute("id")]
        public Guid id;
        [XmlAttribute("enabled")]
        public bool Enabled;
        public TrafficACLTarget Target;

        public TrafficACLRule() {
            id = new Guid();
            Enabled = true;
            Target = TrafficACLTarget.Drop;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }
    }

    [Serializable]
    public class TrafficACL : CoyoteConfigSection {

        [XmlAttribute("id")]
        public Guid id;

        [XmlAttribute("name")]
        public string Name;
        [XmlAttribute("enabled")]
        public bool Enabled;

        public Guid Group;

        [XmlArray("Rules")]
        [XmlArrayItem("Rule")]
        public List<TrafficACLRule> Rules;

        public string Protocol;
        public string Source;
        public string Destination;

        public TrafficACLTable Table;
        public TrafficACLParent ParentChain;

        public TrafficACLTarget Target;

        public TrafficACL() {
            Target = TrafficACLTarget.Drop;
            Rules = new List<TrafficACLRule>();
            Enabled = true;
            id = Guid.NewGuid();
            Group = CoyoteConstants.SystemGroupID;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }

    }
}

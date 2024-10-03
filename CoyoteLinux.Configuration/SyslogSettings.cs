using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class SyslogSettings : CoyoteConfigSection {

        [XmlAttribute("remote")]
        public bool RemoteSyslog { get; set; }
        [XmlAttribute("remoteHost")]
        public string RemoteSyslogServer { get; set; }

        public bool LogLocalRejects { get; set; }
        public bool LogLocalAccepts { get; set; }
        public bool LogForwardRejects { get; set; }
        public bool LogForwardAccepts { get; set; }

        public SyslogSettings() {
        }
    }
}

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class SystemSettings : CoyoteConfigSection {

        public bool WebAdminEnabled { get; set; }
        public int WebAdminPort { get; set; }
        public bool SSHAdminEnabled { get; set; }
        public int SSHAdminPort { get; set; }
        public bool WebServiceEnabled { get; set; }
        public int WebServicePort { get; set; }

        public string Timezone { get; set; }
        public string TimeServerHost { get; set; }

        public string Hostname { get; set; }
        public string DomainName { get; set; }

        public bool HSPMode { get; set; }

        public SystemSettings() {
            Hostname = "coyote";
            DomainName = "localdomain";
            Timezone = "EST";
            WebAdminPort = 443;
            WebAdminEnabled = true;
            SSHAdminPort = 22;
            SSHAdminEnabled = true;
            WebServiceEnabled = true;
            WebServicePort = 8080;
            HSPMode = false;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }

    }
}

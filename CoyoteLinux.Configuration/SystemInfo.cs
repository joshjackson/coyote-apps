using CoyoteLinux.SysUtils;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class SystemInfo {

        public string Hostname { get; set; }
        public string FirmwareVersion { get; set; }
        public DateTime FirmwareBuildDate { get; set; }
        public string KernelVersion { get; set; }
        public string LoaderVersion { get; set; }
        public int ConfigVersion { get; set; }
        public string SystemGroupName { get; set; }
        public Guid SystemGroupID { get; set; }
        public string CoyoteBaseDir { get; set; }

        public SystemInfo() {
            Hostname = "coyote";
            FirmwareVersion = SysProc.GetCoyoteVersion();
            FirmwareBuildDate = DateTime.Now;
            KernelVersion = Environment.OSVersion.Version.ToString();
            LoaderVersion = "4.0.100";
            ConfigVersion = 1;
            SystemGroupID = CoyoteConstants.SystemGroupID;
            SystemGroupName = CoyoteConstants.SystemGroupName;

            CoyoteBaseDir = CoyoteConstants.COYOTE_SYSTEM_BASE_DIR;
        }

    }
}

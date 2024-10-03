using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteLinux.SysUtils {
    public static class SysProc {

        public static List<String> GetNetworkInterfaces() {

            List<String> ret = new List<String>();

            if (File.Exists("/proc/net/dev")) {
                try {
                    String[] devs = File.ReadAllLines("/proc/net/dev");
                    foreach (String s in devs) {
                        string sl = s.Trim();
                        if (sl.StartsWith("eth")) {
                            string d = sl.Substring(0, sl.IndexOf(":", 0));
                            ret.Add(d);
                        }
                    }
                } catch {
                    return null;
                }
            }
            return ret;
        }

        public static long GetTotalSystemRAM() {
            return 0;
        }

        public static string GetCoyoteVersion() {
            if (File.Exists(CoyoteConstants.COYOTE_SYSTEM_BASE_DIR + "etc/coyote.version")) {
                string ret = File.ReadAllText(CoyoteConstants.COYOTE_SYSTEM_BASE_DIR + "etc/coyote.version");
                return ret;
            } else {
                return CoyoteConstants.COYOTE_VERSION;
            }
        }

        public static string GetProcessorModel() {

            string ret = "Unknown processor model";

            if (File.Exists("/proc/cpuinfo")) {
                try {
                    string[] cpuData = File.ReadAllLines("/proc/cpuinfo");
                    foreach (string s in cpuData) {
                        if (s.StartsWith("model name")) {
                            string s2 = s.Substring(s.IndexOf(":") + 1).Trim();
                            while (ret != s2) {
                                ret = s2;
                                s2 = ret.Replace("  ", " ");
                            }
                            break;
                        }
                    }
                } catch {
                }
            }
            return ret;
        }

    }
}

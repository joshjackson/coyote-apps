using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteLinux.SysUtils {
    public static class SysExec {

        public static Process StartProcess(string ProcName, string Args) {
#if DEBUG
            Console.WriteLine(String.Format("[DEBUG] SysExec: {0} {1}", ProcName, Args));
#endif

#if DEVEL
			return null;
#else // DEVEL
            Process PSI = new Process();
            PSI.StartInfo.UseShellExecute = false;
            //PSI.StartInfo.RedirectStandardInput = true;
            //PSI.StartInfo.RedirectStandardOutput = true;
            PSI.StartInfo.FileName = ProcName;
            PSI.StartInfo.Arguments = Args;
            try {
                if (!PSI.Start()) {
                    PSI.Dispose();
                    return null;
                }
            } catch {
#if DEBUG
                Console.WriteLine("[DEBUG] StartProcess() failed for {0} {1}", ProcName, Args);
#endif
                PSI.Dispose();
                return null;
            }
            return PSI;
#endif // DEVEL
        }

        public static void WaitForProcess(Process aProc) {
            if (aProc != null) {
                aProc.WaitForExit();
            }
        }

        public static bool WaitForProcess(Process aProc, int WaitTime) {
            if (aProc != null) {
                return aProc.WaitForExit(WaitTime);
            } else {
#if DEVEL
				return true;
#else
                return false;
#endif
            }
        }
    }
}

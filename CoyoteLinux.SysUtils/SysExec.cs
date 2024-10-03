// 
//  SysExec.cs
//  
//  Author:
//       Joshua Jackson <jjackson@vortech.net>
// 
//  Date:
//      10/3/2024        
//
//  Product:
//       Coyote Linux https://www.coyotelinux.com
// 	
//  Copyright (c) 1999-2024 Vortech Consulting, LLC, All rights reserved
//
//  This file is part of the Coyote Linux distribution. Please see the Coyote
//  Linux web site for usage and licensing information.

using System.Diagnostics;

namespace CoyoteLinux.SysUtils {
    public static class SysExec {

        public static Process? StartProcess(string ProcName, string Args) {
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

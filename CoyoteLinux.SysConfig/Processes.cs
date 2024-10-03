using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteLinux.SysConfig {
    internal class Processes {
        // Global DEBUG_MODE equivalent
        private static int DEBUG_MODE = 1;

        // Method to execute a command and return the exit code
        public static int DoExec(string cmd) {
            int errcode = 0;

            switch (DEBUG_MODE) {
                case 1:
                    Console.WriteLine(cmd);
                    ExecuteCommand(cmd, out errcode);
                    break;

                case 2:
                    Console.WriteLine(cmd);
                    break;

                case 0:
                    ExecuteCommand(cmd, out errcode);
                    break;
            }

            return errcode;
        }

        // Helper method to run a shell command using Process
        public static void ExecuteCommand(string command, out int exitCode) {
            try {
                Process process = new Process {
                    StartInfo = new ProcessStartInfo {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{command}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                process.WaitForExit();
                exitCode = process.ExitCode;
            } catch (Exception ex) {
                Console.WriteLine($"Failed to execute command: {command}. Error: {ex.Message}");
                exitCode = -1;
            }
        }

        // Method to run the depmod command
        public static void CheckDepmod() {
            DoExec("depmod -a -q 1> /dev/null 2> /dev/null");
        }

        // Method to load kernel modules using modprobe
        public static void LoadModule(object modname) {
            string modstr;

            // Check if the input is a string or a list of strings
            if (modname is List<string>) {
                modstr = string.Join(" ", (List<string>)modname);
            } else {
                modstr = modname.ToString();
            }

            // Execute the modprobe command
            DoExec($"modprobe -qs {modstr} 1> /dev/null 2> /dev/null");
        }

        // Method to get the PID from a specified file
        public static int GetServicePID(string pidfile) {
            int pidbuf = 0;

            try {
                if (File.Exists(pidfile)) {
                    string pidText = File.ReadAllText(pidfile).Trim();
                    pidbuf = int.Parse(pidText);

                    if (DEBUG_MODE > 0) {
                        Console.WriteLine($"Read PID {pidbuf} from {pidfile}");
                    }
                } else {
                    Console.WriteLine($"PID file not found: {pidfile}");
                }
            } catch (Exception ex) {
                Console.WriteLine($"Failed to read PID from {pidfile}: {ex.Message}");
            }

            return pidbuf;
        }

        // Main method for testing the functions
        public static void Main(string[] args) {
            // Set DEBUG_MODE
            DEBUG_MODE = 1;

            // Test DoExec
            Console.WriteLine("Executing 'ls' command:");
            DoExec("ls");

            // Test CheckDepmod
            Console.WriteLine("Running depmod:");
            CheckDepmod();

            // Test LoadModule with a single module name
            Console.WriteLine("Loading 'dummy' module:");
            LoadModule("dummy");

            // Test LoadModule with multiple modules
            Console.WriteLine("Loading multiple modules: dummy and e1000");
            LoadModule(new List<string> { "dummy", "e1000" });

            // Test GetServicePID with a dummy PID file
            string testPidFile = "test.pid";
            File.WriteAllText(testPidFile, "1234");
            int pid = GetServicePID(testPidFile);
            Console.WriteLine($"PID retrieved: {pid}");

            // Cleanup the test PID file
            if (File.Exists(testPidFile)) {
                File.Delete(testPidFile);
            }
        }

    }
}

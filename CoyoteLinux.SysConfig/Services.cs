using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteLinux.SysConfig {
    internal class Services {

        // Function to start the SSH service
        public static bool StartSSHService(Config config) {
            if (File.Exists("/var/run/dropbear.pid")) {
                return false;  // SSH service already running
            }

            string sshOpts = $"-p {config.ssh.port}";

            // Start the SSH service
            return (Processes.DoExec($"/usr/sbin/sshd {sshOpts}") == 0);
        }

        // Function to stop the SSH service
        public static bool StopSSHService() {
            if (!File.Exists("/var/run/dropbear.pid")) {
                return false;  // SSH service not running
            }

            int pid = GetServicePID("/var/run/dropbear.pid");

            if (pid > 0) {
                try {
                    Process process = Process.GetProcessById(pid);
                    process.Kill();
                    return true;
                } catch (Exception) {
                    return false;  // Failed to kill the process
                }
            }
            return false;
        }

        // Function to start the DynDNS service
        public static void StartDynDNSService(Config config) {
            if (config.dyndns.enable) {
                Processes.DoExec("/etc/ez-ipupdate.conf");
            }
        }

        // Function to stop the DynDNS service
        public static void StopDynDNSService() {
            Processes.DoExec("killall -HUP ez-ipupdate");
        }

        // Function to start the UPNP service
        public static void StartUPNPService(Config config) {
            string extNic = config.public_interface;
            string intNic = config.private_interface;

            Processes.DoExec($"/usr/sbin/upnpd -e {extNic} -i {intNic}");
        }

        // Function to stop the UPNP service
        public static void StopUPNPService() {
            Processes.DoExec("killall -HUP upnpd");
        }

        // Helper function to get PID from a file
        private static int GetServicePID(string pidFilePath) {
            try {
                if (File.Exists(pidFilePath)) {
                    string pidString = File.ReadAllText(pidFilePath).Trim();
                    return int.Parse(pidString);
                }
            } catch (Exception) {
                Console.WriteLine($"Unable to read or parse PID from {pidFilePath}");
            }

            return -1;  // Return invalid PID if parsing fails
        }

        // Main function for testing the services
        public static void Main(string[] args) {
            Config config = new Config {
                ssh = new Config.SSHConfig { port = "2222" },
                dyndns = new Config.DynDNSConfig { enable = true },
                public_interface = "eth0",
                private_interface = "eth1"
            };

            Console.WriteLine("Starting SSH Service...");
            if (StartSSHService(config))
                Console.WriteLine("SSH Service Started");
            else
                Console.WriteLine("Failed to Start SSH Service");

            Console.WriteLine("Stopping SSH Service...");
            if (StopSSHService())
                Console.WriteLine("SSH Service Stopped");
            else
                Console.WriteLine("Failed to Stop SSH Service");

            Console.WriteLine("Starting DynDNS Service...");
            StartDynDNSService(config);
            Console.WriteLine("DynDNS Service Started");

            Console.WriteLine("Stopping DynDNS Service...");
            StopDynDNSService();
            Console.WriteLine("DynDNS Service Stopped");

            Console.WriteLine("Starting UPNP Service...");
            StartUPNPService(config);
            Console.WriteLine("UPNP Service Started");

            Console.WriteLine("Stopping UPNP Service...");
            StopUPNPService();
            Console.WriteLine("UPNP Service Stopped");
        }
    }
}

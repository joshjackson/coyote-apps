// Class: CoyoteLinux.SysConfig.NetworkUtils
// Purpose: Hardware detection routines
//
// Author: Joshua Jackson <jjackson@vortech.net>
// Date: 10/03/2024

using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;


// TODO: Need to implement IP Address Calc routines

namespace CoyoteLinux.SysConfig {
    public class NetworkUtils {
        // Equivalent of the run_ipcalc function in PHP
        public static Dictionary<string, string> RunIpCalc(string parameters) {
            var result = new Dictionary<string, string>
            {
                { "BROADCAST", "" },
                { "PREFIX", "" },
                { "NETMASK", "" },
                { "NETWORK", "" },
                { "retcode", "0" }
            };

            // Run the ipcalc command and capture its output
            try {
                Process process = new Process {
                    StartInfo = new ProcessStartInfo {
                        FileName = "ipcalc",
                        Arguments = parameters,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();

                while (!process.StandardOutput.EndOfStream) {
                    string line = process.StandardOutput.ReadLine();
                    var split = line.Split('=');
                    if (split.Length == 2) {
                        result[split[0].Trim()] = split[1].Trim();
                    }
                }
                result["retcode"] = process.ExitCode.ToString();
            } catch (Exception ex) {
                Console.WriteLine($"Error running ipcalc: {ex.Message}");
                result["retcode"] = "-1";
            }

            return result;
        }

        // Equivalent of the get_hostname function in PHP
        public static string GetHostname() {
            return Environment.MachineName;
        }

        // Enables IP forwarding (simulated for Windows/Linux environments)
        public static void EnableIpForwarding() {
            SetIpForwarding(true);
        }

        // Disables IP forwarding (simulated for Windows/Linux environments)
        public static void DisableIpForwarding() {
            SetIpForwarding(false);
        }

        // Simulated method for enabling/disabling IP forwarding by modifying a system file
        private static void SetIpForwarding(bool enable) {
            try {
                string path = "/proc/sys/net/ipv4/ip_forward";
                if (File.Exists(path)) {
                    File.WriteAllText(path, enable ? "1" : "0");
                } else {
                    Console.WriteLine("IP forwarding configuration file not found.");
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error modifying IP forwarding setting: {ex.Message}");
            }
        }

        // Get IP address of a specific network interface
        public static string GetInterfaceIp(int interfaceIndex = 0) {
            try {
                var interfaces = NetworkInterface.GetAllNetworkInterfaces();

                // Check for a valid index
                if (interfaceIndex < 0 || interfaceIndex >= interfaces.Length) {
                    Console.WriteLine($"Invalid interface index: {interfaceIndex}. Using index 0 as fallback.");
                    interfaceIndex = 0;
                }

                var selectedInterface = interfaces[interfaceIndex];
                var ipProperties = selectedInterface.GetIPProperties();

                foreach (var ip in ipProperties.UnicastAddresses) {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork) // Only IPv4 addresses
                    {
                        return ip.Address.ToString();
                    }
                }

                Console.WriteLine("No IPv4 address found for the selected interface.");
                return "No IPv4 Address";
            } catch (Exception ex) {
                Console.WriteLine($"Error retrieving interface IP: {ex.Message}");
                return "Error";
            }
        }

        // Main function for testing the functionality
        public static void ClassTest(string[] args) {
            // Test RunIpCalc function
            var ipCalcResult = RunIpCalc("192.168.1.1/24");
            Console.WriteLine("IpCalc Result:");
            foreach (var entry in ipCalcResult) {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            // Test GetHostname function
            Console.WriteLine($"Hostname: {GetHostname()}");

            // Test EnableIpForwarding and DisableIpForwarding
            EnableIpForwarding();
            Console.WriteLine("Enabled IP Forwarding.");
            DisableIpForwarding();
            Console.WriteLine("Disabled IP Forwarding.");

            // Test GetInterfaceIp function
            Console.WriteLine($"Interface IP (0): {GetInterfaceIp(0)}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteLinux.SysConfig {
    internal class FileSystem {
        // Global DEBUG_MODE equivalent
        private static int DEBUG_MODE = 1;

        // Method to write configuration data to a specified file
        public static int WriteConfig(string filename, string data) {
            int ret = 0;

            switch (DEBUG_MODE) {
                case 1:
                    // Append data to the file and add a new line
                    try {
                        File.AppendAllText(filename, data + "\n");
                        ret = 1;
                    } catch (Exception ex) {
                        Console.WriteLine($"Error writing to file: {ex.Message}");
                    }
                    goto case 2;

                case 2:
                    Console.WriteLine($"{data} written to {filename}.");
                    return ret;

                case 0:
                    try {
                        File.AppendAllText(filename, data + "\n");
                        ret = 1;
                    } catch (Exception ex) {
                        Console.WriteLine($"Error writing to file: {ex.Message}");
                    }
                    break;
            }

            return ret;
        }

        // Method to write a value to a specific /proc entry
        public static int WriteProcValue(string procentry, string value) {
            string procfile = $"/proc/{procentry}";

            try {
                using (FileStream fs = new FileStream(procfile, FileMode.OpenOrCreate, FileAccess.Write)) {
                    using (StreamWriter writer = new StreamWriter(fs)) {
                        writer.Write(value);
                    }
                }
                return 1;
            } catch (Exception ex) {
                Console.WriteLine($"Failed to write to {procfile}: {ex.Message}");
                return 0;
            }
        }

        // Method to copy a template file to a specified location
        public static bool CopyTemplate(string template, string location) {
            string templatePath = $"/etc/config/templates/{template}";

            try {
                File.Copy(templatePath, location, true);
                return true;
            } catch (Exception ex) {
                Console.WriteLine($"Error copying template from {templatePath} to {location}: {ex.Message}");
                return false;
            }
        }

        // Main method for testing
        public static void Main(string[] args) {
            // Set DEBUG_MODE
            DEBUG_MODE = 1;

            // Test WriteConfig
            string configFile = "test_config.txt";
            WriteConfig(configFile, "This is a test configuration line.");

            // Test WriteProcValue (Assumes the user has the required permissions)
            WriteProcValue("sys/net/ipv4/ip_forward", "1");

            // Test CopyTemplate
            bool copyResult = CopyTemplate("example_template.conf", "copied_template.conf");
            Console.WriteLine($"Template copy successful: {copyResult}");
        }
    }
}

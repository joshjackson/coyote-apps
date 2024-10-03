// Class: CoyoteLinux.SysConfig.Hardware
// Purpose: Hardware detection routines
//
// Author: Joshua Jackson <jjackson@vortech.net>
// Date: 10/03/2024

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoyoteLinux.SysConfig {
    internal class Hardware {
        // Global variable equivalent
        private static bool IN_DEVELOPMENT = false;

        // Data structure to hold NIC information
        public struct NicEntry {
            public string Device { get; set; }
            public string Module { get; set; }
        }

        // Reads the NIC module names from the appropriate file
        public static List<NicEntry> GetNicModuleNames() {
            List<NicEntry> modlist = new List<NicEntry>();
            string modFilePath = IN_DEVELOPMENT ? "niclist" : "/etc/sysconf/niclist";

            // Read NIC module names from the specified file
            try {
                using (StreamReader modFile = new StreamReader(modFilePath)) {
                    string? modLine;
                    while ((modLine = modFile.ReadLine()) != null) {
                        if (!string.IsNullOrWhiteSpace(modLine)) {
                            string[] splitLine = modLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (splitLine.Length >= 2) {
                                NicEntry modEntry = new NicEntry {
                                    Device = splitLine[0],
                                    Module = splitLine[1]
                                };
                                modlist.Add(modEntry);
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: Could not open PCI ID list. Details: {ex.Message}");
                return modlist;
            }

            // Reading from /proc/bus/pci/devices for the actual NICs detected
            List<NicEntry> niclist = new List<NicEntry>();
            string procFilePath = "/proc/bus/pci/devices";

            try {
                using (StreamReader procFile = new StreamReader(procFilePath)) {
                    string? procLine;
                    while ((procLine = procFile.ReadLine()) != null) {
                        string pattern = @"^([0-9A-Fa-f]+)\s+([0-9A-Fa-f]+)\s+([0-9A-Fa-f]+)";
                        Match match = Regex.Match(procLine, pattern);
                        if (match.Success) {
                            string busInfo = match.Groups[1].Value;
                            string vendor = match.Groups[2].Value;
                            string model = match.Groups[3].Value;

                            // Check if this NIC is in our module list
                            foreach (var modEntry in modlist) {
                                if (vendor.Contains(modEntry.Device)) {
                                    NicEntry nicEntry = new NicEntry {
                                        Device = $"{vendor}:{model}",
                                        Module = modEntry.Module
                                    };
                                    niclist.Add(nicEntry);
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: Unable to open PCI bus proc entry. Details: {ex.Message}");
                return niclist;
            }

            return niclist;
        }

        // Main method for testing the function
        public static void Main(string[] args) {
            // Set IN_DEVELOPMENT based on the environment or configuration
            IN_DEVELOPMENT = true;

            List<NicEntry> nicModules = GetNicModuleNames();

            Console.WriteLine("NIC Modules Detected:");
            foreach (var entry in nicModules) {
                Console.WriteLine($"Device: {entry.Device}, Module: {entry.Module}");
            }
        }
    }
}

// 
//  SysProcNetDev.cs
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

namespace CoyoteLinux.SysUtils {
    public struct NetworkInterfaceStats {
        public string InterfaceName { get; set; }

        // Receive Statistics
        public long ReceiveBytes { get; set; }
        public long ReceivePackets { get; set; }
        public long ReceiveErrors { get; set; }
        public long ReceiveDrop { get; set; }
        public long ReceiveFifoErrors { get; set; }
        public long ReceiveFrame { get; set; }
        public long ReceiveCompressed { get; set; }
        public long ReceiveMulticast { get; set; }

        // Transmit Statistics
        public long TransmitBytes { get; set; }
        public long TransmitPackets { get; set; }
        public long TransmitErrors { get; set; }
        public long TransmitDrop { get; set; }
        public long TransmitFifoErrors { get; set; }
        public long TransmitCollisions { get; set; }
        public long TransmitCarrier { get; set; }
        public long TransmitCompressed { get; set; }
    }

    public class SysProcNetDev {

        public static List<NetworkInterfaceStats> ReadProcNetDev() {
            List<NetworkInterfaceStats> interfaceStats = new List<NetworkInterfaceStats>();

            try {
                // Read all lines from the /proc/net/dev file
                string[] lines = File.ReadAllLines("/proc/net/dev");

                // Skip the first two lines (header information)
                for (int i = 2; i < lines.Length; i++) {
                    // Split the line by colon to separate interface name and its stats
                    string[] parts = lines[i].Split(':');
                    if (parts.Length != 2)
                        continue;

                    // Extract the interface name and trim any whitespace
                    string interfaceName = parts[0].Trim();

                    // Split the statistics by spaces and remove empty entries
                    string[] stats = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (stats.Length < 16)
                        continue; // Ensure we have enough data points

                    // Map the statistics to the NetworkInterfaceStats object
                    NetworkInterfaceStats statsEntry = new NetworkInterfaceStats {
                        InterfaceName = interfaceName,
                        ReceiveBytes = long.Parse(stats[0]),
                        ReceivePackets = long.Parse(stats[1]),
                        ReceiveErrors = long.Parse(stats[2]),
                        ReceiveDrop = long.Parse(stats[3]),
                        ReceiveFifoErrors = long.Parse(stats[4]),
                        ReceiveFrame = long.Parse(stats[5]),
                        ReceiveCompressed = long.Parse(stats[6]),
                        ReceiveMulticast = long.Parse(stats[7]),
                        TransmitBytes = long.Parse(stats[8]),
                        TransmitPackets = long.Parse(stats[9]),
                        TransmitErrors = long.Parse(stats[10]),
                        TransmitDrop = long.Parse(stats[11]),
                        TransmitFifoErrors = long.Parse(stats[12]),
                        TransmitCollisions = long.Parse(stats[13]),
                        TransmitCarrier = long.Parse(stats[14]),
                        TransmitCompressed = long.Parse(stats[15])
                    };

                    // Add to the list of interface stats
                    interfaceStats.Add(statsEntry);
                }
            } catch (Exception ex) {
                Console.WriteLine($"Failed to read /proc/net/dev: {ex.Message}");
            }

            return interfaceStats;
        }

    }
}

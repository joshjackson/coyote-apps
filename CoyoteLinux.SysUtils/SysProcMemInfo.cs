using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteLinux.SysUtils {
    public struct MemoryInfo {
        public long MemTotal { get; set; }
        public long MemFree { get; set; }
        public long Buffers { get; set; }
        public long Cached { get; set; }
        public long SwapCached { get; set; }
        public long Active { get; set; }
        public long Inactive { get; set; }
        public long SwapTotal { get; set; }
        public long SwapFree { get; set; }
        public long Dirty { get; set; }
        public long Writeback { get; set; }
        public long AnonPages { get; set; }
        public long Mapped { get; set; }
        public long Shmem { get; set; }
        public long Slab { get; set; }
        public long SReclaimable { get; set; }
        public long SUnreclaim { get; set; }

        // Additional fields can be added as needed
    }

    public class SysProcMemInfo {
        // Method to parse /proc/meminfo and return a MemoryInfo object
        public static MemoryInfo ParseProcMemInfo() {
            MemoryInfo memInfo = new MemoryInfo();
            Dictionary<string, long> memValues = new Dictionary<string, long>();

            try {
                // Read all lines from the /proc/meminfo file
                string[] lines = File.ReadAllLines("/proc/meminfo");

                foreach (string line in lines) {
                    // Each line is of the format: MemTotal: 16337452 kB
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2)
                        continue;

                    // Extract the key (e.g., MemTotal) and value (e.g., 16337452)
                    string key = parts[0].TrimEnd(':');
                    if (long.TryParse(parts[1], out long value)) {
                        memValues[key] = value;
                    }
                }

                // Map the extracted values to the MemoryInfo object
                memInfo.MemTotal = memValues.GetValueOrDefault("MemTotal");
                memInfo.MemFree = memValues.GetValueOrDefault("MemFree");
                memInfo.Buffers = memValues.GetValueOrDefault("Buffers");
                memInfo.Cached = memValues.GetValueOrDefault("Cached");
                memInfo.SwapCached = memValues.GetValueOrDefault("SwapCached");
                memInfo.Active = memValues.GetValueOrDefault("Active");
                memInfo.Inactive = memValues.GetValueOrDefault("Inactive");
                memInfo.SwapTotal = memValues.GetValueOrDefault("SwapTotal");
                memInfo.SwapFree = memValues.GetValueOrDefault("SwapFree");
                memInfo.Dirty = memValues.GetValueOrDefault("Dirty");
                memInfo.Writeback = memValues.GetValueOrDefault("Writeback");
                memInfo.AnonPages = memValues.GetValueOrDefault("AnonPages");
                memInfo.Mapped = memValues.GetValueOrDefault("Mapped");
                memInfo.Shmem = memValues.GetValueOrDefault("Shmem");
                memInfo.Slab = memValues.GetValueOrDefault("Slab");
                memInfo.SReclaimable = memValues.GetValueOrDefault("SReclaimable");
                memInfo.SUnreclaim = memValues.GetValueOrDefault("SUnreclaim");
            } catch (Exception ex) {
                Console.WriteLine($"Failed to parse /proc/meminfo: {ex.Message}");
            }

            return memInfo;
        }
    }

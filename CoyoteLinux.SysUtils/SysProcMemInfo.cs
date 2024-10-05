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
        public long KernelStack { get; set; }
        public long PageTables { get; set; }
        public long NFS_Unstable { get; set; }
        public long Bounce { get; set; }
        public long WritebackTmp { get; set; }
        public long CommitLimit { get; set; }
        public long Committed_AS { get; set; }
        public long VmallocTotal { get; set; }
        public long VmallocUsed { get; set; }
        public long VmallocChunk { get; set; }
        public long Percpu { get; set; }
        public long AnonHugePages { get; set; }
        public long ShmemHugePages { get; set; }
        public long ShmemPmdMapped { get; set; }
        public long FileHugePages { get; set; }
        public long FilePmdMapped { get; set; }
        public long HugePages_Total { get; set; }
        public long HugePages_Free { get; set; }
        public long HugePages_Rsvd { get; set; }
        public long HugePages_Surp { get; set; }
        public long Hugepagesize { get; set; }
        public long Hugetlb { get; set; }
        public long DirectMap4k { get; set; }
        public long DirectMap2M { get; set; }
        public long DirectMap1G { get; set; }
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
                memInfo.KernelStack = memValues.GetValueOrDefault("KernelStack");
                memInfo.PageTables = memValues.GetValueOrDefault("PageTables");
                memInfo.NFS_Unstable = memValues.GetValueOrDefault("NFS_Unstable");
                memInfo.Bounce = memValues.GetValueOrDefault("Bounce");
                memInfo.WritebackTmp = memValues.GetValueOrDefault("WritebackTmp");
                memInfo.CommitLimit = memValues.GetValueOrDefault("CommitLimit");
                memInfo.Committed_AS = memValues.GetValueOrDefault("Committed_AS");
                memInfo.VmallocTotal = memValues.GetValueOrDefault("VmallocTotal");
                memInfo.VmallocUsed = memValues.GetValueOrDefault("VmallocUsed");
                memInfo.VmallocChunk = memValues.GetValueOrDefault("VmallocChunk");
                memInfo.Percpu = memValues.GetValueOrDefault("Percpu");
                memInfo.AnonHugePages = memValues.GetValueOrDefault("AnonHugePages");
                memInfo.ShmemHugePages = memValues.GetValueOrDefault("ShmemHugePages");
                memInfo.ShmemPmdMapped = memValues.GetValueOrDefault("ShmemPmdMapped");
                memInfo.FileHugePages = memValues.GetValueOrDefault("FileHugePages");
                memInfo.FilePmdMapped = memValues.GetValueOrDefault("FilePmdMapped");
                memInfo.HugePages_Total = memValues.GetValueOrDefault("HugePages_Total");
                memInfo.HugePages_Free = memValues.GetValueOrDefault("HugePages_Free");
                memInfo.HugePages_Rsvd = memValues.GetValueOrDefault("HugePages_Rsvd");
                memInfo.HugePages_Surp = memValues.GetValueOrDefault("HugePages_Surp");
                memInfo.Hugepagesize = memValues.GetValueOrDefault("Hugepagesize");
                memInfo.Hugetlb = memValues.GetValueOrDefault("Hugetlb");
                memInfo.DirectMap4k = memValues.GetValueOrDefault("DirectMap4k");
                memInfo.DirectMap2M = memValues.GetValueOrDefault("DirectMap2M");
                memInfo.DirectMap1G = memValues.GetValueOrDefault("DirectMap1G");
            } catch (Exception ex) {
                Console.WriteLine($"Failed to parse /proc/meminfo: {ex.Message}");
            }

            return memInfo;
        }
    }
}

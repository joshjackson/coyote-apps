using CoyoteLinux.SysUtils;

namespace sysconfig {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");

            List<NetworkInterfaceStats> list = new List<NetworkInterfaceStats>();
            list = SysProcNetDev.ReadProcNetDev();
            foreach (NetworkInterfaceStats stat in list) {
                Console.WriteLine($"Interface {stat.InterfaceName}  rx(bytes): {stat.ReceiveBytes} rx(packets): {stat.ReceivePackets} tx(bytes): {stat.TransmitBytes} tx(packets): {stat.TransmitPackets}");
            }
        }
    }
}

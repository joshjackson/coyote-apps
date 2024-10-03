using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoyoteLinux.SysConfig {
    internal class Qos {
        private const int QOS_PRIO_LAN = 5;
        private const int QOS_PRIO_HIGH = 10;
        private const int QOS_PRIO_NORMAL = 20;
        private const int QOS_PRIO_LOW = 30;

        // Convert priority number to string representation
        public static string GetQosOutputPrio(int prio) {
            string ret = "low";
            switch (prio) {
                case QOS_PRIO_LAN:
                    ret = "lan";
                    break;
                case QOS_PRIO_LOW:
                    ret = "low";
                    break;
                case QOS_PRIO_NORMAL:
                    ret = "normal";
                    break;
                case QOS_PRIO_HIGH:
                    ret = "high";
                    break;
                default:
                    ret = "low";
                    break;
            }
            return ret;
        }

        public static int GetQosPrio(string priostr, bool quiet = false) {
            int ret = QOS_PRIO_LOW;
            switch (priostr.ToLower()) {
                case "lan":
                    ret = QOS_PRIO_LAN;
                    break;
                case "low":
                    ret = QOS_PRIO_LOW;
                    break;
                case "normal":
                    ret = QOS_PRIO_NORMAL;
                    break;
                case "high":
                    ret = QOS_PRIO_HIGH;
                    break;
                default:
                    if (!quiet) {
                        Console.WriteLine($"Invalid QoS priority: {priostr} - Defaulting to low priority class.");
                    }
                    break;
            }
            return ret;
        }

        public static void QosClearSettings(Config config) {
            Processes.DoExec("iptables -F -t mangle");
            Processes.DoExec("iptables -F wolv-tc-up -t mangle");
            Processes.DoExec("iptables -F wolv-tc-down -t mangle");
            Processes.DoExec("iptables -X wolv-tc-up -t mangle");
            Processes.DoExec("iptables -X wolv-tc-down -t mangle");
            Processes.DoExec($"tc qdisc del root dev {config.public_interface}");
            Processes.DoExec("tc qdisc del root dev eth1");
        }

        public static void ConfigureQoS(Config config) {
            // Clear any existing settings
            QosClearSettings(config);

            // Set up the QoS subsystem and attach any available filters
            if (config.qos.ContainsKey("enable") && config.qos["enable"] == "true") {
                StartQoS(config);
            }
        }

        public static void StartQoS(Config config) {
            // Determine if we should be using a PPP adapter for the public interface
            string pi = config.public_interface;

            // Initialize iptables rules for QoS
            Processes.DoExec("iptables -t mangle -N wolv-tc-up");
            Processes.DoExec("iptables -t mangle -N wolv-tc-down");

            // Do not subject LAN to LAN traffic to QoS
            Processes.DoExec($"iptables -A FORWARD -t mangle ! -i {pi} ! -o {pi} -j MARK --set-mark {QOS_PRIO_LAN}");

            // Apply rules for outbound and inbound traffic on the public interface
            Processes.DoExec($"iptables -A FORWARD -t mangle -i {pi} -j wolv-tc-down");
            Processes.DoExec($"iptables -A FORWARD -t mangle -o {pi} -j wolv-tc-up");

            // Calculate upstream and downstream bandwidth thresholds
            int upMax = (int)(int.Parse(config.qos["upstream"]) * 0.98);
            int upHigh = (int)(upMax * 0.90);
            int upNorm = (int)(upMax * 0.50);
            string upCeil = $"{upHigh}kbit";
            string upLow = "1kbit";

            int dlMax = (int)(int.Parse(config.qos["downstream"]) * 0.98);
            int dlHigh = (int)(dlMax * 0.90);
            int dlNorm = (int)(dlMax * 0.50);
            string dlCeil = $"{dlHigh}kbit";
            string dlLow = "1kbit";

            string li = "eth1";
            int dp = config.qos.ContainsKey("default-prio") ? int.Parse(config.qos["default-prio"]) : QOS_PRIO_LOW;

            // Set up the public interface
            Processes.DoExec($"tc qdisc add dev {pi} root handle 1: htb default {dp}");
            Processes.DoExec($"tc class add dev {pi} parent 1: classid 1:1 htb rate {upMax} burst 15k");
            Processes.DoExec($"tc class add dev {pi} parent 1:1 classid 1:10 htb rate $up_high ceil {upMax} burst 15k");
            Processes.DoExec($"tc class add dev {pi} parent 1:1 classid 1:20 htb rate $up_norm ceil {upCeil} burst 15k");
            Processes.DoExec($"tc class add dev {pi} parent 1:1 classid 1:30 htb rate $up_low ceil {upCeil} burst 5k");
            Processes.DoExec($"tc qdisc add dev {pi} parent 1:10 handle 10: sfq perturb 10");
            Processes.DoExec($"tc qdisc add dev {pi} parent 1:20 handle 20: sfq perturb 10");
            Processes.DoExec($"tc qdisc add dev {pi} parent 1:30 handle 30: sfq perturb 10");
            Processes.DoExec($"tc filter add dev {pi} parent 1:0 protocol ip prio 1 handle 10 fw flowid 1:10");
            Processes.DoExec($"tc filter add dev {pi} parent 1:0 protocol ip prio 1 handle 20 fw flowid 1:20");
            Processes.DoExec($"tc filter add dev {pi} parent 1:0 protocol ip prio 1 handle 30 fw flowid 1:30");

            // Set up the internal interface
            Processes.DoExec($"tc qdisc add dev {li} root handle 1: htb default {dp}");
            Processes.DoExec($"tc class add dev {li} parent 1: classid 1:1 htb rate {dlMax} burst 15k");
            Processes.DoExec($"tc class add dev {li} parent 1:1 classid 1:10 htb rate {dlHigh} ceil {dlMax} burst 15k");
            Processes.DoExec($"tc class add dev {li} parent 1:1 classid 1:20 htb rate {dlNorm} ceil {dlCeil} burst 15k");
            Processes.DoExec($"tc class add dev {li} parent 1:1 classid 1:30 htb rate {dlLow} ceil {dlCeil} burst 5k");
            Processes.DoExec($"tc qdisc add dev {li} parent 1:10 handle 10: sfq perturb 10");
            Processes.DoExec($"tc qdisc add dev {li} parent 1:20 handle 20: sfq perturb 10");
            Processes.DoExec($"tc qdisc add dev {li} parent 1:30 handle 30: sfq perturb 10");
            Processes.DoExec($"tc filter add dev {li} parent 1:0 protocol ip prio 1 handle 5 fw flowid 1:1");
            Processes.DoExec($"tc filter add dev {li} parent 1:0 protocol ip prio 1 handle 10 fw flowid 1:10");
            Processes.DoExec($"tc filter add dev {li} parent 1:0 protocol ip prio 1 handle 20 fw flowid 1:20");
            Processes.DoExec($"tc filter add dev {li} parent 1:0 protocol ip prio 1 handle 30 fw flowid 1:30");

            // Iterate over filters and apply rules based on configuration
            foreach (var qf in config.qos_filters) {
                
                string fc = (qf["interface"] == pi) ? "wolv-tc-up" : "wolv-tc-down";
                
                string cmd = $"iptables -t mangle -A {fc}";

                // Add protocol specification if not "all"
                if (qf["proto"] != "all") {
                    cmd += $" -p {qf["proto"]}";

                    // Add port specification if the protocol is TCP or UDP and ports are specified
                    if ((qf["proto"] == "tcp" || qf["proto"] == "udp") && qf.ContainsKey("ports") && !string.IsNullOrEmpty(qf["ports"])) {
                        cmd += $" --dport {qf["ports"]}";
                    }
                }

                // Add priority mark
                cmd += $" -j MARK --set-mark {qf["prio"]}";

                // Execute the constructed command
                Processes.DoExec(cmd);
            }
        }

    }
}

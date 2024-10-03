// 
//  NetworkSettings.cs
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

using System.Xml.Serialization;

namespace CoyoteLinux.Configuration {
    [Serializable]
    public class NetworkSettings {

        [XmlArray("NameServers")]
        [XmlArrayItem("Address")]
        public List<String> Nameservers { get; set; }

        [XmlArray("Interfaces")]
        [XmlArrayItem("Interface")]
        public List<NetworkInterface> Interfaces { get; set; }

        [XmlArray("Routes")]
        [XmlArrayItem("NetworkRoute")]
        public List<NetworkRoute> Routes { get; set; }

        public string BridgeIP { get; set; }
        public bool UseSpanningTree { get; set; }

        public bool FailoverEnabled { get; set; }
        public string VirtualIP { get; set; }
        public int FailoverClusterID { get; set; }
        public string FailoverKey { get; set; }

        public NetworkSettings() {
            Interfaces = new List<NetworkInterface>();
            Nameservers = new List<string>();
            Routes = new List<NetworkRoute>();
            FailoverEnabled = false;
            VirtualIP = "";
            FailoverClusterID = 0;
            FailoverKey = "";
            BridgeIP = "";
            UseSpanningTree = false;
        }

        public NetworkInterface? GetInterfaceByName(string ifName) {
            foreach (NetworkInterface iface in Interfaces) {
                if (iface.Name.ToLower() == ifName.ToLower()) {
                    return iface;
                }
            }
            return null;
        }

        public NetworkInterface? GetInterface(Guid aGuid) {
            foreach (NetworkInterface iface in Interfaces) {
                if (iface.id == aGuid) {
                    return iface;
                }
            }
            return null;
        }

        public List<String> GetAddresses() {
            List<String> ret = new List<string>();

            foreach (NetworkInterface iface in Interfaces) {
                if (iface.AddressType == NetworkInterfaceAddressType.Static) {
                    foreach (InterfaceAddress addr in iface.Addresses) {
                        ret.Add(addr.Address);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Determine if one or more interfaces are configured for bridging
        /// </summary>
        /// <returns>True if bridging is enabled on any interface</returns>
        public bool UseBridging() {
            foreach (NetworkInterface iface in Interfaces) {
                if (iface.Bridged) {
                    return true;
                }
            }
            return false;
        }
    }
}

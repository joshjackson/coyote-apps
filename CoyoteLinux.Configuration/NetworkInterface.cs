// 
//  NetworkInterface.cs
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
    public enum NetworkInterfaceAddressType {
        None,
        Static,
        DHCP,
        PPPoE,
        Bridged
    }

    [Serializable]
    public class InterfaceAddress {
        public string Address { get; set; }
        public string Netmask { get; set; }
        public long Prefix { get; set; }
        public bool IsSecondary { get; set; }
        public int VLanTag { get; set; }

        public InterfaceAddress() {
            Address = String.Empty;
            Netmask = String.Empty;
            Prefix = 0;
            IsSecondary = false;
            VLanTag = 1;
        }
    }

    [Serializable]
    public class NetworkInterface {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
        public string MAC { get; set; }
        public bool Bridged { get; set; }
        public int MTU { get; set; }
        public bool Enabled { get; set; }
        public NetworkInterfaceAddressType AddressType { get; set; }

        [XmlArray("Addresses")]
        [XmlArrayItem("Address")]
        public List<InterfaceAddress> Addresses { get; set; }

        public string DHCPHostname { get; set; }
        public bool IsVirtual { get; set; }

        [XmlIgnore]
        public string PrimaryIP {
            get {
                if (Addresses.Count > 0) {
                    return String.Format("{0}/{1}", Addresses[0].Address, Addresses[0].Prefix);
                } else {
                    return String.Empty;
                }
            }
        }

        public NetworkInterface() {
            AddressType = NetworkInterfaceAddressType.DHCP;
            id = Guid.NewGuid();
            Name = String.Empty;
            MAC = String.Empty;
            DHCPHostname = String.Empty;
            MTU = 0;
            Addresses = new List<InterfaceAddress>();
        }

    }
}

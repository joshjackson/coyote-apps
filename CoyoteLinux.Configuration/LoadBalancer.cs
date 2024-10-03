// 
//  LoadBalancer.cs
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
    public class LoadBalancerRealHost : CoyoteConfigSection {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public int Weight { get; set; }
        public bool Backup { get; set; }
        public int CheckInterval { get; set; }
        public int RiseCount { get; set; }
        public int FallCount { get; set; }
        public bool HealthCheck { get; set; }
        public int MaxConn { get; set; }

        public LoadBalancerRealHost() {
            Enabled = true;
            Name = String.Empty;
            Address = String.Empty;
            Backup = false;
            id = Guid.NewGuid();
            CheckInterval = 2000;
            RiseCount = 2;
            FallCount = 3;
            HealthCheck = true;
        }

        public override string GenerateConfigText() {
            return base.GenerateConfigText();
        }

    }

    [Serializable]
    public class LoadBalancerVirtualHost : CoyoteConfigSection {

        [XmlAttribute("id")]
        public Guid id { get; set; }

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        public string Name { get; set; }
        public Guid Owner { get; set; }

        public int ApplicationPort { get; set; }
        public string Protocol { get; set; }
        public string VirtualIP { get; set; }
        public string Scheduler { get; set; }
        public string Comment { get; set; }

        // ---------------------------
        // HAProxy Specifics
        public string ProxyMode { get; set; }
        public int MaxConns { get; set; }
        public int ConnTimeout { get; set; }
        public int ClientTimeout { get; set; }
        public int ServerTimeout { get; set; }

        public bool SSLCheck { get; set; }
        public bool ForwardFor { get; set; }
        public string BalancerMode { get; set; }

        // ---------------------------
        // LVS Specifics
        //        public int FirewallMark;
        //        public string Device;
        //        public int ServiceTimeout;
        //        public int ReentryTime;
        //        public string VirtualNetmask;
        //        public bool AddressAffinity;
        //        public string AffinityNetmask;
        //        public int AffinityTimeout;
        //        public bool Quiesce;


        [XmlArray("BackendServers")]
        [XmlArrayItem("Server")]
        public List<LoadBalancerRealHost> BackendServers;

        public LoadBalancerVirtualHost() {
            Name = String.Empty;
            Protocol = String.Empty;
            VirtualIP = String.Empty;
            Scheduler = String.Empty;
            Comment = String.Empty;
            ProxyMode = String.Empty;
            MaxConns = 0;
            ConnTimeout = 0;
            BalancerMode = String.Empty;
            BackendServers = new List<LoadBalancerRealHost>();
            id = Guid.Empty;
        }

        public LoadBalancerRealHost? GetBackendServer(Guid anID) {
            foreach (LoadBalancerRealHost rh in BackendServers) {
                if (rh.id == anID) {
                    return rh;
                }
            }
            return null;
        }

        public override string GenerateConfigText() {
            return "";
        }

    }

    [Serializable]
    [XmlRoot("LoadBalancer")]
    public class LoadBalancerSettings : CoyoteConfigSection {

        [XmlAttribute("enabled")]
        public bool Enabled;

        // ---------------------------
        // HAProxy Specifics
        public int MaxConnections;
        public int ClientTimeout;
        public int ServerTimeout;
        public int ConnectionTimeout;
        public int MaxConnPerVS;
        public bool Redispatch;
        public int RetryCount;

        // ---------------------------
        // LVS Specifics
        //        public string PublicIP;
        //        public string PrivateIP;
        //        public string NATIP;
        //        public string NATNetmask;
        //        public string NATDevice;


        [XmlArray("VirtualServers")]
        [XmlArrayItem("Server")]
        public List<LoadBalancerVirtualHost> VirtualServers;

        public LoadBalancerSettings() {
            Enabled = false;
            MaxConnections = 4096;
            ConnectionTimeout = 5000;
            ClientTimeout = 50000;
            ServerTimeout = 240000;
            MaxConnPerVS = 2000;

            Redispatch = true;
            RetryCount = 3;

            VirtualServers = new List<LoadBalancerVirtualHost>();
        }

        public LoadBalancerVirtualHost? GetVirtualHost(Guid anID) {
            foreach (LoadBalancerVirtualHost lvs in VirtualServers) {
                if (lvs.id == anID) {
                    return lvs;
                }
            }

            return null;
        }

        public LoadBalancerVirtualHost? GetVirtualHost(Guid anID, Guid anOwner) {
            foreach (LoadBalancerVirtualHost lvs in VirtualServers) {
                if ((lvs.id == anID) && (lvs.Owner == anOwner)) {
                    return lvs;
                }
            }

            return null;
        }

        public LoadBalancerVirtualHost? CreateVirtualHost() {
            LoadBalancerVirtualHost vs = new LoadBalancerVirtualHost();
            vs.Enabled = true;
            vs.ConnTimeout = ConnectionTimeout;
            vs.ClientTimeout = ClientTimeout;
            vs.ServerTimeout = ServerTimeout;
            vs.MaxConns = MaxConnPerVS;
            return vs;
        }

        public override string GenerateConfigText() {
            return String.Empty;
        }

    }
}

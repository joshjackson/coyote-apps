// 
//  CoyoteConfigFile.cs
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
    [XmlRoot("CoyoteConfig")]
    public class CoyoteConfigFile {
        [XmlAttribute("version")]
        public int ConfigVersion { get; set; }
        [XmlAttribute("licenseCode")]
        private string _licenseCode;

        [NonSerialized, XmlIgnore]
        private SystemActivation _sysAct;

        [NonSerialized, XmlIgnore]
        public SystemInfo _systemInfo;

        [XmlElement("SystemSettings")]
        public SystemSettings _systemSettings;

        [XmlElement("SyslogSettings")]
        public SyslogSettings _syslogSettings;

        [XmlElement("NetworkSettings")]
        public NetworkSettings _networkSettings;

        [XmlArray("Users")]
        [XmlArrayItem("User")]
        public List<SystemUser> _systemUsers;

        [XmlArray("UserGroups")]
        [XmlArrayItem("Group")]
        public List<SystemUserGroup> _systemUserGroups;

        [XmlArray("NetworkObjectGroups")]
        [XmlArrayItem("ObjectGroup")]
        public List<NetworkObjectGroup> _networkObjectGroups;

        [XmlArray("NetworkObjects")]
        [XmlArrayItem("Object")]
        public List<NetworkObject> _networkObjects;

        [XmlElement("LoadBalancer")]
        public LoadBalancerSettings _loadBalancerSettings;

        [XmlElement("PPTPSettings")]
        public PPTPVPNSettings _PPTPVPNSettings;

        [XmlElement("IPSECSettings")]
        public IPSECVPNSettings _IPSECSettings;

        [XmlElement("IPSSettings")]
        public IPSSettings _IPSSettings;

        [XmlArray("TrafficACLs")]
        [XmlArrayItem("ACL")]
        public List<TrafficACL> _ACLs;

        [XmlArray("BlackLists")]
        [XmlArrayItem("BlackList")]
        public List<BlackList> _blackLists;

        [XmlIgnore]
        public List<NetworkInterface> _networkInterfaces {
            get { return _networkSettings.Interfaces; }
        }

        public CoyoteLicenseLevel LicenseLevel {
            get { return _getLicenseLevel(); }
        }

        public CoyoteConfigFile() {

            _licenseCode = "";
            ConfigVersion = 0;
            _systemInfo = new SystemInfo();
            _sysAct = new SystemActivation();
            _systemSettings = new SystemSettings();
            _syslogSettings = new SyslogSettings();
            _networkSettings = new NetworkSettings();
            _loadBalancerSettings = new LoadBalancerSettings();
            _networkObjectGroups = new List<NetworkObjectGroup>();
            _networkObjects = new List<NetworkObject>();
            _systemUsers = new List<SystemUser>();
            _systemUserGroups = new List<SystemUserGroup>();
            _PPTPVPNSettings = new PPTPVPNSettings();
            _IPSECSettings = new IPSECVPNSettings();
            _IPSSettings = new IPSSettings();
            _ACLs = new List<TrafficACL>();
            _blackLists = new List<BlackList>();
        }

        private CoyoteLicenseLevel _getLicenseLevel() {
            if ((_licenseCode == "") || (_sysAct == null)) {
                return CoyoteLicenseLevel.vlTrial;
            }
            return _sysAct.ValidateLicense(_licenseCode);
        }

        private CoyoteLicenseLevel _setLicenseCode(string aCode) {
            CoyoteLicenseLevel newLevel = _sysAct.ValidateLicense(aCode);
            if (newLevel != CoyoteLicenseLevel.vlInvalid) {
                _licenseCode = aCode;

            }
            return newLevel;
        }

        public NetworkObject? FindObjectByName(string aName) {

            foreach (NetworkObject o in _networkObjects) {
                if (o.ObjectName == aName) {
                    return o;
                }
            }
            return null;
        }

        public bool SetLicenseCode(string aCode) {
            return (_setLicenseCode(aCode) != CoyoteLicenseLevel.vlInvalid);
        }

    }
}

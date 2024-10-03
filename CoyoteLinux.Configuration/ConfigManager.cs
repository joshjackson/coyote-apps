// 
//  ConfigManager.cs
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
    public class ConfigManager {

        private CoyoteConfigFile? _config;
        public CoyoteConfigFile? Config {
            get { return _config; }
            set { _config = value; }
        }

        private string _configDir;

        public ConfigManager(string ConfigPath) {
            _config = new CoyoteConfigFile();
            _configDir = ConfigPath;
        }

        /// <summary>
        /// Loads the specified file into the configuration object
        /// </summary>
        /// <param name="Filename">Filename of file to load</param>
        /// <returns>True if the file was loaded successfully</returns>
        private CoyoteResult _loadConfigFile(string Filename) {
            // Load the main configuration file
            try {
                XmlSerializer s = new XmlSerializer(typeof(CoyoteConfigFile));
                TextReader r = new StreamReader(_configDir + Filename);
                try {
                    _config = (CoyoteConfigFile?)s.Deserialize(r);
                    return new CoyoteResult();
                } finally {
                    r.Close();
                }
            } catch (Exception ex) {
                return new CoyoteResult(CoyoteErrorCode.CONFIG_LOAD_FAILED, ex);
            }
        }

        private CoyoteResult _saveConfigFile(string Filename) {
            try {
                XmlSerializer s = new XmlSerializer(typeof(CoyoteConfigFile));
                TextWriter w = new StreamWriter(_configDir + Filename);
                try {
                    s.Serialize(w, _config);
                    return new CoyoteResult();
                } finally {
                    w.Close();
                }
            } catch {
                return new CoyoteResult(CoyoteErrorCode.CONFIG_SAVE_FAILED);
            }
        }

        /// <summary>
        /// Loads the master configuration file into the config object
        /// </summary>
        /// <returns>True if the file was loaded successfully</returns>
        public CoyoteResult LoadConfig() {
            return _loadConfigFile("CoyoteConfig.xml");
        }

        /// <summary>
        /// Loads the current working configuration into the config object
        /// </summary>
        /// <returns>True if load was successful</returns>
        public CoyoteResult LoadWorkingConfig() {
            if (HaveWorkingConfig()) {
                return _loadConfigFile("working-config.xml");
            } else {
                return LoadConfig();
            }
        }

        /// <summary>
        /// Loads the current running configuration into the config object
        /// </summary>
        /// <returns>True if load was successful</returns>
        public CoyoteResult LoadRunningConfig() {
            return _loadConfigFile("running-config.xml");
        }

        /// <summary>
        /// Saves the currect configuration object to the working config file
        /// </summary>
        /// <returns>True if the file was saved successfully</returns>
        public CoyoteResult SaveWorkingConfig() {
            if (_config != null) {
                _config.ConfigVersion++;
            }
            return _saveConfigFile("working-config.xml");
        }

        /// <summary>
        /// Commits the active working configuration to the master config file
        /// </summary>
        /// <returns>True if the commit was successful</returns>
        public CoyoteResult CommitWorkingConfig() {

            CoyoteResult lRet = LoadWorkingConfig();
            if (lRet.code == CoyoteErrorCode.SUCCESS) {
                // Perform system configuration commit
                CoyoteResult rRet = _saveConfigFile("running-config.xml");
                if (rRet.code != CoyoteErrorCode.SUCCESS) {
                    return rRet;
                }
            } else {
                return lRet;
            }

            // Save the running configuration to the master configuration file
            CoyoteResult wRet = _saveConfigFile("CoyoteConfig.xml");

            if (wRet.code == CoyoteErrorCode.SUCCESS) {
                ResetConfig();
            }

            return wRet;

        }


        /// <summary>
        /// If there is an active working configuration, clear it
        /// </summary>
        public void ResetConfig() {
            if (File.Exists(_configDir + "working-config.xml")) {
                File.Delete(_configDir + "working-config.xml");
            }
        }

        /// <summary>
        /// Determine if a working config file is present
        /// </summary>
        /// <returns>True if a working config file is present</returns>
        public bool HaveWorkingConfig() {
            return (File.Exists(_configDir + "working-config.xml"));
        }

    }
}

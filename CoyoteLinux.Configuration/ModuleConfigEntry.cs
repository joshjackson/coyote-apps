// 
//  ModuleConfigEntry.cs
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
    public class ModuleCommand {
        [XmlAttribute("Name")]
        public string CommandName { get; set; }
        public string CommandControlFile { get; set; }
        public bool RequiresSysAdmin { get; set; }
        public bool RequiresHSPMode { get; set; }

        public ModuleCommand() {
            CommandName = string.Empty;
            CommandControlFile = string.Empty;
            RequiresSysAdmin = false;
            RequiresHSPMode = false;
        }
    }

    [Serializable]
    [XmlRoot("ModuleConfig")]
    public class ModuleConfigEntry {
        [XmlAttribute("Enabled")]
        public Boolean IsEnabled { get; set; }

        [XmlAttribute("Name")]
        public string ModuleName { get; set; }

        [XmlAttribute("GUID")]
        public Guid ModuleGuid { get; set; }

        [XmlArray("Dependencies")]
        [XmlArrayItem("DepEntry")]
        public List<Guid> ModuleDeps { get; set; }

        [XmlElement("ModuleCommand")]
        public string ModuleCommand { get; set; }

        [XmlElement("ModuleControlFile")]
        public string ModuleControlFile { get; set; }

        [XmlArray("ExecutableDependencies")]
        [XmlArrayItem("Executable")]
        public List<String> ExecDeps { get; set; }

        [XmlArray("ModuleSubCommands")]
        [XmlArrayItem("CommandEntry")]
        public List<ModuleCommand>? SubCommands { get; set; }

        [NonSerialized, XmlIgnore]
        public List<String> ErrorMessages;

        public ModuleConfigEntry() {
            ModuleDeps = new List<Guid>();
            ExecDeps = new List<string>();
            ErrorMessages = new List<string>();
            ModuleName = String.Empty;
            ModuleCommand = String.Empty;
            ModuleControlFile = String.Empty;
            SubCommands = new List<ModuleCommand>();
        }

        public static ModuleConfigEntry? LoadConfigFile(string Filename) {
            try {
                XmlSerializer s = new XmlSerializer(typeof(ModuleConfigEntry));
                using (TextReader r = new StreamReader(Filename)) {
                    ModuleConfigEntry? e = (ModuleConfigEntry?)s.Deserialize(r);
                    return e;
                }
            } catch {
#if true
                throw;
#else
                return null;
#endif
            }
        }
    }
}

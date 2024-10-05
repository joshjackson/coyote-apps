// 
//  CoyoteConsoleMenu.cs
//  
//  Author:
//       Joshua Jackson <jjackson@vortech.net>
// 
//  Product:
//       Coyote Linux
// 	
//  Copyright (c) 2011 Vortech Consulting, LLC, All rights reserved
// 
//  This file is part of the Coyote Linux distribution and is covered under the
//  Vortech Software Use and Distribution License. Use and/or distrbution of
//  this file without prior written consent from Vortech Consulting, LLC is in 
//  violation of United States and International copyright laws.
using Mono.Terminal;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace CoyoteConsole {

    public enum CoyoteMenuType {
        DEFAULT,
        ADMIN,
        MONITOR,
        USER,
        NETWORK,
        DEBUG
    }

    public enum CoyoteMenuItemType {
        UNKNOWN,
        ADMIN_NETWORKING,
        ADMIN_MONITOR,
        ADMIN_LOCKDOWN,
        ADMIN_FIRMWARE,
        ADMIN_REBOOT,
        ADMIN_SHUTDOWN,

        NETWORK_INTERFACES,

        MONITOR_NETWORK,
        MONITOR_FIREWALL,

        DEBUG_CONNTRACK,
        DEBUG_IPSEC
    }

    public delegate bool MenuAction();


    [Serializable]
    public class CoyoteMenuItem {

        public CoyoteMenuItem() {
            Title = "";
            Help = "";
            Action = null;
            Width = 1;
            ItemType = CoyoteMenuItemType.UNKNOWN;
        }

        public CoyoteMenuItem(CoyoteMenuItemType itemType, string title, string help, MenuAction action) {
            Title = title ?? "";
            Help = help ?? "";
            Action = action;
            ItemType = itemType;
            Width = Title.Length + Help.Length + 1;
        }

        [XmlAttribute("name")]
        public CoyoteMenuItemType ItemType;
        public string Title { get; set; }
        public string Help { get; set; }
        [XmlIgnore]
        public MenuAction Action { get; set; }
        [XmlIgnore]
        public int Width { get; set; }
    }


    [Serializable]
    public class CoyoteMenu {
        [XmlAttribute]
        public CoyoteMenuType MenuType;
        [XmlElement("MenuItem", typeof(CoyoteMenuItem))]
        public ArrayList MenuItems;

        public CoyoteMenu() {
        }

        public CoyoteMenuItem GetItem(CoyoteMenuItemType it) {
            foreach (CoyoteMenuItem i in MenuItems) {
                if (i.ItemType == it) {
                    return i;
                }
            }

            return null;
        }

    }

    [Serializable]
    [XmlRoot("Menus")]
    public class CoyoteMenuList {
        [XmlElement("CoyoteMenu", typeof(CoyoteMenu))]
        public ArrayList MenuList;

        public CoyoteMenuList() {
            MenuList = new ArrayList();
        }
    }

    public static class CoyoteMenus {

        public static CoyoteMenuList Menus;

        public static void Init() {
            try {
                Menus = (CoyoteMenuList)new XmlSerializer(typeof(CoyoteMenuList),
                             new Type[] { typeof(CoyoteMenuList) }).Deserialize(new XmlTextReader("ConsoleText.xml"));
            } catch (Exception ex) {
                Application.Error("Error", "Failed to load console menu configuration.");
            }
        }

        public static CoyoteMenu GetMenu(CoyoteMenuType MenuType) {
            if (Menus == null) {
                Init();
            }

            foreach (CoyoteMenu c in Menus.MenuList) {
                if (c.MenuType == MenuType) {
                    return c;
                }
            }

            return null;
        }
    }

    public class CoyoteConsoleMenu : IListProvider {
        public CoyoteMenu menuConfig;

        public List<CoyoteMenuItem> items = new List<CoyoteMenuItem>();
        public ListView view;
        public CoyoteMenuType menuType = CoyoteMenuType.DEFAULT;
        public Container helpArea;

        private void _initMenu() {

            helpArea = new Container(33, 2, 44, 15);
            helpArea.Redraw();

            menuConfig = CoyoteMenus.GetMenu(menuType);
        }

        //public CoyoteConsoleMenu() {
        //	_initMenu();
        //}

        public CoyoteConsoleMenu(CoyoteMenuType mType) {
            menuType = mType;
            _initMenu();
        }

        public void AddHandler(CoyoteMenuItemType itemType, MenuAction itemHandler) {

            CoyoteMenuItem i = null;
            if (menuConfig != null) {
                i = menuConfig.GetItem(itemType);
            }

            if (i != null) {
                i.Action = itemHandler;
            } else {
                i = new CoyoteMenuItem(itemType, itemType.ToString(), "No help is available for this item", itemHandler);
            }

            items.Add(i);
        }

        public void DefaultMenuAction() {
            Application.Info("Error", "The selected menu item has not been implemented yet.");
        }

        void IListProvider.SetListView(ListView v) {
            view = v;
        }

        int IListProvider.Items {
            get {
                return items.Count;
            }
        }

        bool IListProvider.AllowMark {
            get {
                return false;
            }
        }

        bool IListProvider.IsMarked(int n) {
            return false;
        }

        private List<String> GetHelpLines(String Help) {

            List<String> lines = new List<String>();

            int last = 0;
            int max_w = 0;
            string x;
            for (int i = 0; i < Help.Length; i++) {
                if (Help[i] == '\n') {
                    x = Help.Substring(last, i - last);
                    lines.Add(x);
                    last = i + 1;
                    if (x.Length > max_w)
                        max_w = x.Length;
                }
            }
            x = Help.Substring(last);
            if (x.Length > max_w)
                max_w = x.Length;
            lines.Add(x);

            return lines;
        }

        void IListProvider.Render(int line, int col, int width, int item) {

            string str = (items[item].Title.Length > width) ?
                items[item].Title.Substring(0, width) :
                items[item].Title.PadRight(width);

            Curses.addstr(str);

            if (item == view.Selected) {
                UpdateHelp();
            }

        }

        bool IListProvider.ProcessKey(int ch) {
            return false;
        }

        void UpdateHelp() {

            if (items.Count == 0) {
                return;
            }

            Curses.attrset(Application.ColorNormal);
            helpArea.Clear();
            int item = view.Selected;
            // Render help information

            Curses.move(2, 33);
            Curses.addstr(items[item].Title);
            if (!String.IsNullOrEmpty(items[item].Help)) {
                List<String> s = GetHelpLines(items[item].Help);
                for (int y = 0; y < s.Count; y++) {
                    Curses.move(4 + y, 33);
                    Curses.addstr(s[y]);
                }
            } else {
                Curses.move(4, 41);
                Curses.addstr("No help available for this item");
            }
        }

        void IListProvider.SelectedChanged() {
            UpdateHelp();
        }

    }
}


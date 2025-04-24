using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Action { get; set; }
        public int Level { get; set; }
        public int Status { get; set; } // 0-активно, 1-только просмотр, 2-скрыто
        public List<MenuItem> Children { get; set; } = new List<MenuItem>();

        public MenuItem(int level, string title, string action)
        {
            Level = level;
            Title = title;
            Action = action;
        }
    }

    public class MenuBuilder
    {
        public List<MenuItem> MenuItems { get; } = new List<MenuItem>();

        public MenuBuilder(string menuFile = "menu.txt")
        {
            if (!File.Exists(menuFile))
                throw new FileNotFoundException($"Файл меню {menuFile} не найден");

            var lines = File.ReadAllLines(menuFile);
            var stack = new Stack<MenuItem>();

            foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var parts = line.Split(new[] { ' ' }, 3);
                if (parts.Length < 2) continue;

                int level = int.Parse(parts[0]);
                string title = parts[1];
                string action = parts.Length > 2 ? parts[2].Trim('"') : null;

                var item = new MenuItem(level, title, action);

                if (level == 0)
                {
                    MenuItems.Add(item);
                    stack.Clear();
                    stack.Push(item);
                }
                else
                {
                    while (stack.Peek().Level >= level) stack.Pop();
                    stack.Peek().Children.Add(item);
                    stack.Push(item);
                }
            }
        }

        public void ApplyUserPermissions(string userFile, string username)
        {
            if (!File.Exists(userFile))
                throw new FileNotFoundException($"Файл пользователей {userFile} не найден");

            var permissions = new Dictionary<string, int>();
            bool userFound = false;

            foreach (var line in File.ReadAllLines(userFile))
            {
                if (line.StartsWith("#"))
                {
                    if (userFound) break;
                    if (line.Substring(1).Split(' ')[0] == username)
                        userFound = true;
                }
                else if (userFound && !string.IsNullOrWhiteSpace(line))
                {
                    var parts = line.Split(' ');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int status))
                        permissions[parts[0]] = status;
                }
            }

            if (userFound)
                ApplyPermissions(MenuItems, permissions);
        }

        private void ApplyPermissions(List<MenuItem> items, Dictionary<string, int> permissions)
        {
            foreach (var item in items)
            {
                if (permissions.TryGetValue(item.Title, out int status))
                    item.Status = status;
                ApplyPermissions(item.Children, permissions);
            }
        }

        public void BuildMenu(MenuStrip menuStrip, EventHandler clickHandler)
        {
            menuStrip.Items.Clear();
            BuildMenuItems(menuStrip.Items, MenuItems, clickHandler);
        }

        private void BuildMenuItems(ToolStripItemCollection parent, List<MenuItem> items, EventHandler clickHandler)
        {
            foreach (var item in items.Where(i => i.Status != 2)) // Пропускаем скрытые
            {
                var menuItem = new ToolStripMenuItem(item.Title)
                {
                    Tag = item.Action,
                    Enabled = item.Status == 0
                };

                if (item.Children.Count > 0)
                {
                    BuildMenuItems(menuItem.DropDownItems, item.Children, clickHandler);
                }
                else
                {
                    menuItem.Click += clickHandler;
                }

                parent.Add(menuItem);
            }
        }
    }
}

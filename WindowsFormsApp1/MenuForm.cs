using System;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class MenuForm : Form
    {
        private readonly MenuBuilder _menuBuilder;
        private readonly string _username;

        public MenuForm(UserRole role, string username)
        {
            

            InitializeComponent();
            _username = username;
            this.Text = $"база - Пользователь: {username}";

            try
            {
                _menuBuilder = new MenuBuilder();
                _menuBuilder.ApplyUserPermissions("user.txt", username);
                _menuBuilder.BuildMenu(menuStrip1, MenuItem_Click);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки меню: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem?.Tag != null)
            {
                string action = menuItem.Tag.ToString();
                MessageBox.Show($"Выполнено: {action}\nПользователь: {_username}",
                    "Действие меню", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonLoadDll_Click(object sender, EventArgs e)
        {
            LoadExternalDll("MyPlugin.dll", "MyPlugin.Main", "Run");
        }

        private void LoadExternalDll(string dllPath, string className, string methodName)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllPath);
                Type type = assembly.GetType(className);
                object instance = Activator.CreateInstance(type);
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(instance, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки DLL: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            authrizationForm newForm = new authrizationForm();
            newForm.Show();
            this.Close();
        }
    }
}
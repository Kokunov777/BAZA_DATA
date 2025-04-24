using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class authrizationForm : Form
    {
        private readonly AuthManager _authManager = new AuthManager();
        public string Username { get; private set; }
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;

        public authrizationForm()
        {
            InitializeComponent();
            this.Text = "Авторизация - База данных";
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_authManager.Authenticate(username, password))
                {
                    Username = username;
                    UserRole role = _authManager.GetUserRole(username);
                    
                    MenuForm mainForm = new MenuForm(role, username);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    passwordTextBox.Clear();
                    passwordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
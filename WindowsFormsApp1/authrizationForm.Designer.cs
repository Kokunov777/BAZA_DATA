using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class authrizationForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // Настройка формы
            this.Text = "Вход";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(300, 250);

            // Панель заголовка
            Panel headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(255, 218, 34);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            this.Controls.Add(headerPanel);

            // Иконка ключа
            PictureBox keyIcon = new PictureBox();
            keyIcon.Image = Properties.Resources.key_icon; // Замените на свою иконку
            keyIcon.SizeMode = PictureBoxSizeMode.Zoom;
            keyIcon.Width = 40;
            keyIcon.Height = 40;
            keyIcon.Location = new Point(10, 10);
            headerPanel.Controls.Add(keyIcon);

            // Лейблы в заголовке
            Label titleLabel = new Label();
            titleLabel.Text = "АИС Отдел кадров";
            titleLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            titleLabel.Location = new Point(60, 10);
            titleLabel.AutoSize = true;
            headerPanel.Controls.Add(titleLabel);

            Label versionLabel = new Label();
            versionLabel.Text = "Версия 1.0.0.0";
            versionLabel.Font = new Font("Arial", 8, FontStyle.Regular);
            versionLabel.Location = new Point(60, 30);
            versionLabel.AutoSize = true;
            headerPanel.Controls.Add(versionLabel);

            Label descriptionLabel = new Label();
            descriptionLabel.Text = "Введите имя пользователя и пароль";
            descriptionLabel.Font = new Font("Arial", 8, FontStyle.Regular);
            descriptionLabel.Location = new Point(10, 70);
            descriptionLabel.AutoSize = true;
            this.Controls.Add(descriptionLabel);

            // Лейблы и текстовые поля
            Label usernameLabel = new Label();
            usernameLabel.Text = "Имя пользователя:";
            usernameLabel.Location = new Point(10, 100);
            usernameLabel.AutoSize = true;
            this.Controls.Add(usernameLabel);

            usernameTextBox = new TextBox();
            usernameTextBox.Location = new Point(120, 100);
            usernameTextBox.Width = 150;
            usernameTextBox.Text = "";
            this.Controls.Add(usernameTextBox);

            Label passwordLabel = new Label();
            passwordLabel.Text = "Пароль:";
            passwordLabel.Location = new Point(10, 130);
            passwordLabel.AutoSize = true;
            this.Controls.Add(passwordLabel);

            passwordTextBox = new TextBox();
            passwordTextBox.Location = new Point(120, 130);
            passwordTextBox.Width = 150;
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Text = "";
            this.Controls.Add(passwordTextBox);

            // Кнопки
            Button loginButton = new Button();
            loginButton.Text = "Вход";
            loginButton.Location = new Point(120, 170);
            loginButton.Size = new Size(70, 25);
            loginButton.Click += LoginButton_Click;
            this.Controls.Add(loginButton);

            Button cancelButton = new Button();
            cancelButton.Text = "Отмена";
            cancelButton.Location = new Point(200, 170);
            cancelButton.Size = new Size(70, 25);
            cancelButton.Click += CancelButton_Click;
            this.Controls.Add(cancelButton);

            // Нижний колонтитул
            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 30;
            footerPanel.BackColor = SystemColors.Control;
            this.Controls.Add(footerPanel);

            Label footerLabel = new Label();
            footerLabel.Text = "Язык ввода Английский";
            footerLabel.Location = new Point(10, 5);
            footerLabel.AutoSize = true;
            footerPanel.Controls.Add(footerLabel);
        }


        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
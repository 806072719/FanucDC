using AntdUI;
using System.Drawing;
using System.Windows.Forms;

namespace FanucDC
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            usernameLabel = new AntdUI.Label();
            passwordLabel = new AntdUI.Label();
            usernameText = new Input();
            passwordText = new Input();
            button2 = new AntdUI.Button();
            cancelBtn = new AntdUI.Button();
            // 新增登录标题标签（提升界面辨识度）
            lblLoginTitle = new AntdUI.Label();
            // 新增登录表单面板（分层布局，更有层次感）
            pnlLoginForm = new AntdUI.Panel();
            pnlLoginForm.SuspendLayout();
            SuspendLayout();
            // 
            // lblLoginTitle - 登录标题
            // 
            lblLoginTitle.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            lblLoginTitle.ForeColor = Color.FromArgb(49, 112, 250);
            lblLoginTitle.Location = new Point(0, 20);
            lblLoginTitle.Name = "lblLoginTitle";
            lblLoginTitle.Size = new Size(350, 40);
            lblLoginTitle.TabIndex = 0;
            lblLoginTitle.Text = "系统登录";
            lblLoginTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlLoginForm - 登录表单面板（分层阴影，提升质感）
            // 
            pnlLoginForm.BackColor = Color.White;
            pnlLoginForm.BorderColor = Color.FromArgb(230, 230, 230);
            pnlLoginForm.Radius = 8;
            //pnlLoginForm.BorderStyle = BorderStyle.FixedSingle;
            pnlLoginForm.Controls.Add(usernameLabel);
            pnlLoginForm.Controls.Add(passwordLabel);
            pnlLoginForm.Controls.Add(usernameText);
            pnlLoginForm.Controls.Add(passwordText);
            pnlLoginForm.Controls.Add(button2);
            pnlLoginForm.Controls.Add(cancelBtn);
            pnlLoginForm.Location = new Point(20, 70);
            pnlLoginForm.Name = "pnlLoginForm";
            pnlLoginForm.Size = new Size(310, 260);
            pnlLoginForm.TabIndex = 1;
            // 
            // usernameLabel - 用户名标签
            // 
            usernameLabel.Font = new Font("Microsoft YaHei UI", 9.5F);
            usernameLabel.ForeColor = Color.FromArgb(80, 80, 80);
            usernameLabel.Location = new Point(30, 30);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(70, 40);
            usernameLabel.TabIndex = 6;
            usernameLabel.Text = "用户名";
            usernameLabel.TextAlign = ContentAlignment.MiddleRight;
            usernameLabel.Click += usernameLabel_Click;
            // 
            // passwordLabel - 密码标签
            // 
            passwordLabel.Font = new Font("Microsoft YaHei UI", 9.5F);
            passwordLabel.ForeColor = Color.FromArgb(80, 80, 80);
            passwordLabel.Location = new Point(30, 90);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(70, 40);
            passwordLabel.TabIndex = 7;
            passwordLabel.Text = "密码";
            passwordLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // usernameText - 用户名输入框
            // 
            usernameText.Font = new Font("Microsoft YaHei UI", 9.5F);
            usernameText.Location = new Point(106, 30);
            usernameText.Name = "usernameText";
            usernameText.Size = new Size(170, 40);
            usernameText.TabIndex = 8;
            usernameText.PlaceholderText = "请输入登录用户名";
            // 
            // passwordText - 密码输入框
            // 
            passwordText.Font = new Font("Microsoft YaHei UI", 9.5F);
            passwordText.Location = new Point(106, 90);
            passwordText.Name = "passwordText";
            passwordText.PasswordChar = '*';
            passwordText.Size = new Size(170, 40);
            passwordText.TabIndex = 9;
            passwordText.PlaceholderText = "请输入登录密码";
            // 
            // button2 - 登录按钮（主按钮）
            // 
            button2.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            button2.Location = new Point(176, 160);
            button2.Name = "button2";
            button2.Size = new Size(100, 45);
            button2.TabIndex = 11;
            button2.Text = "登录";
            button2.Type = TTypeMini.Primary;
            button2.Click += button2_Click;
            // 
            // cancelBtn - 取消按钮（次按钮）
            // 
            cancelBtn.Font = new Font("Microsoft YaHei UI", 10F);
            cancelBtn.Location = new Point(66, 160);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(100, 45);
            cancelBtn.TabIndex = 12;
            cancelBtn.Text = "取消";
            cancelBtn.Type = TTypeMini.Default;
            // 
            // LoginForm - 登录主窗体
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(350, 360);
            Controls.Add(pnlLoginForm);
            Controls.Add(lblLoginTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fanuc CNC";
            pnlLoginForm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Label usernameLabel;
        private AntdUI.Label passwordLabel;
        private AntdUI.Input usernameText;
        private AntdUI.Input passwordText;
        private AntdUI.Button button2;
        private AntdUI.Button cancelBtn;
        // 新增控件（仅布局用，无业务逻辑）
        private AntdUI.Label lblLoginTitle;
        private AntdUI.Panel pnlLoginForm;
    }
}
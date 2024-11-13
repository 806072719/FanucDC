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
            loginBtn = new Button();
            cancelBtn = new Button();
            usernameLabel = new Label();
            passwordLabel = new Label();
            usernameText = new TextBox();
            passwordText = new TextBox();
            SuspendLayout();
            // 
            // loginBtn
            // 
            loginBtn.Location = new Point(163, 188);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new Size(75, 23);
            loginBtn.TabIndex = 0;
            loginBtn.Text = "登录";
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(55, 188);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 23);
            cancelBtn.TabIndex = 1;
            cancelBtn.Text = "取消";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(55, 56);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(44, 17);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "用户名";
            usernameLabel.Click += label1_Click;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(55, 105);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(32, 17);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "密码";
            // 
            // usernameText
            // 
            usernameText.Location = new Point(114, 53);
            usernameText.Name = "usernameText";
            usernameText.Size = new Size(100, 23);
            usernameText.TabIndex = 4;
            // 
            // passwordText
            // 
            passwordText.Location = new Point(114, 105);
            passwordText.MaxLength = 14;
            passwordText.Name = "passwordText";
            passwordText.PasswordChar = '*';
            passwordText.Size = new Size(100, 23);
            passwordText.TabIndex = 5;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 223);
            Controls.Add(passwordText);
            Controls.Add(usernameText);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(cancelBtn);
            Controls.Add(loginBtn);
            MaximizeBox = false;
            Name = "LoginForm";
            Text = "LoginForm";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loginBtn;
        private Button cancelBtn;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameText;
        private TextBox passwordText;
    }
}
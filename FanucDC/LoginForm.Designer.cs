using AntdUI;

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
            usernameLabel = new AntdUI.Label();
            passwordLabel = new AntdUI.Label();
            usernameText = new Input();
            passwordText = new Input();
            button2 = new AntdUI.Button();
            cancelBtn = new AntdUI.Button();
            SuspendLayout();
            // 
            // usernameLabel
            // 
            usernameLabel.Location = new Point(33, 53);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(75, 38);
            usernameLabel.TabIndex = 6;
            usernameLabel.Text = "用户名";
            usernameLabel.TextAlign = ContentAlignment.MiddleRight;
            usernameLabel.Click += usernameLabel_Click;
            // 
            // passwordLabel
            // 
            passwordLabel.Location = new Point(33, 105);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(75, 35);
            passwordLabel.TabIndex = 7;
            passwordLabel.Text = "密码";
            passwordLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // usernameText
            // 
            usernameText.Location = new Point(114, 53);
            usernameText.Name = "usernameText";
            usernameText.Size = new Size(124, 38);
            usernameText.TabIndex = 8;
            // 
            // passwordText
            // 
            passwordText.Location = new Point(114, 105);
            passwordText.Name = "passwordText";
            passwordText.PasswordChar = '*';
            passwordText.Size = new Size(124, 35);
            passwordText.TabIndex = 9;
            // 
            // button2
            // 
            button2.AutoSizeMode = TAutoSize.Auto;
            button2.Location = new Point(181, 170);
            button2.Name = "button2";
            button2.Shape = TShape.Round;
            button2.Size = new Size(57, 41);
            button2.TabIndex = 11;
            button2.Text = "登录";
            button2.Type = TTypeMini.Primary;
            button2.Click += button2_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.AutoSizeMode = TAutoSize.Auto;
            cancelBtn.Location = new Point(114, 170);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Shape = TShape.Round;
            cancelBtn.Size = new Size(57, 41);
            cancelBtn.TabIndex = 12;
            cancelBtn.Text = "取消";
            cancelBtn.Type = TTypeMini.Warn;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 223);
            Controls.Add(cancelBtn);
            Controls.Add(button2);
            Controls.Add(passwordText);
            Controls.Add(usernameText);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            MaximizeBox = false;
            Name = "LoginForm";
            Text = "LoginForm";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private AntdUI.Label usernameLabel;
        private AntdUI.Label passwordLabel;
        private AntdUI.Input usernameText;
        private AntdUI.Input passwordText;
        private AntdUI.Button button2;
        private AntdUI.Button cancelBtn;
    }
}
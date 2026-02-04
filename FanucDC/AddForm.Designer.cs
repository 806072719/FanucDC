using AntdUI;

namespace FanucDC
{
    partial class AddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            codeLabel = new AntdUI.Label();
            codeText = new AntdUI.Input();
            nameText = new AntdUI.Input();
            nameLabel = new AntdUI.Label();
            ipText = new AntdUI.Input();
            ipLabel = new AntdUI.Label();
            portText = new AntdUI.Input();
            port = new AntdUI.Label();
            saveBtn = new AntdUI.Button();
            cancelBtn = new AntdUI.Button();
            SuspendLayout();
            // 
            // codeLabel
            // 
            codeLabel.Font = new Font("Microsoft YaHei UI", 9F);
            codeLabel.ForeColor = Color.FromArgb(80, 80, 80);
            codeLabel.Location = new Point(30, 30);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new Size(80, 36);
            codeLabel.TabIndex = 0;
            codeLabel.Text = "设备代码";
            codeLabel.TextAlign = ContentAlignment.MiddleRight;
            codeLabel.Click += label1_Click;
            // 
            // codeText
            // 
            codeText.Location = new Point(116, 30);
            codeText.Name = "codeText";
            codeText.Size = new System.Drawing.Size(180, 36);
            codeText.TabIndex = 1;
            // 
            // nameText
            // 
            nameText.Location = new Point(116, 76);
            nameText.Name = "nameText";
            nameText.Size = new System.Drawing.Size(180, 36);
            nameText.TabIndex = 3;
            // 
            // nameLabel
            // 
            nameLabel.Font = new Font("Microsoft YaHei UI", 9F);
            nameLabel.ForeColor = Color.FromArgb(80, 80, 80);
            nameLabel.Location = new Point(30, 76);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(80, 36);
            nameLabel.TabIndex = 2;
            nameLabel.Text = "设备名称";
            nameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ipText
            // 
            ipText.Location = new Point(116, 122);
            ipText.Name = "ipText";
            ipText.Size = new System.Drawing.Size(180, 36);
            ipText.TabIndex = 5;
            // 
            // ipLabel
            // 
            ipLabel.Font = new Font("Microsoft YaHei UI", 9F);
            ipLabel.ForeColor = Color.FromArgb(80, 80, 80);
            ipLabel.Location = new Point(30, 122);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(80, 36);
            ipLabel.TabIndex = 4;
            ipLabel.Text = "设备IP";
            ipLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // portText
            // 
            portText.Location = new Point(116, 168);
            portText.Name = "portText";
            portText.Size = new System.Drawing.Size(180, 36);
            portText.TabIndex = 7;
            // 
            // port
            // 
            port.Font = new Font("Microsoft YaHei UI", 9F);
            port.ForeColor = Color.FromArgb(80, 80, 80);
            port.Location = new Point(30, 168);
            port.Name = "port";
            port.Size = new Size(80, 36);
            port.Text = "设备端口";
            port.TextAlign = ContentAlignment.MiddleRight;
            // 
            // saveBtn
            // 
            saveBtn.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            saveBtn.Location = new Point(181, 220);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(90, 40);
            saveBtn.TabIndex = 8;
            saveBtn.Text = "保存";
            saveBtn.Type = TTypeMini.Primary;
            saveBtn.Click += saveBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Font = new Font("Microsoft YaHei UI", 9F);
            cancelBtn.Location = new Point(70, 220);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(90, 40);
            cancelBtn.TabIndex = 9;
            cancelBtn.Text = "取消";
            cancelBtn.Type = TTypeMini.Default;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // AddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(326, 280);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(portText);
            Controls.Add(port);
            Controls.Add(ipText);
            Controls.Add(ipLabel);
            Controls.Add(nameText);
            Controls.Add(nameLabel);
            Controls.Add(codeText);
            Controls.Add(codeLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "新增";
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Label codeLabel;
        private AntdUI.Input codeText;
        private AntdUI.Input nameText;
        private AntdUI.Label nameLabel;
        private AntdUI.Input ipText;
        private AntdUI.Label ipLabel;
        private AntdUI.Input portText;
        private AntdUI.Label port;
        private AntdUI.Button saveBtn;
        private AntdUI.Button cancelBtn;
    }
}
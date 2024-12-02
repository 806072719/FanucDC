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
            codeLabel.Location = new Point(12, 12);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new Size(75, 35);
            codeLabel.TabIndex = 0;
            codeLabel.Text = "设备代码";
            codeLabel.Click += label1_Click;
            // 
            // codeText
            // 
            codeText.Location = new Point(90, 12);
            codeText.Name = "codeText";
            codeText.Size = new Size(186, 35);
            codeText.TabIndex = 1;
            // 
            // nameText
            // 
            nameText.Location = new Point(90, 53);
            nameText.Name = "nameText";
            nameText.Size = new Size(186, 35);
            nameText.TabIndex = 3;
            // 
            // nameLabel
            // 
            nameLabel.Location = new Point(12, 53);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(75, 35);
            nameLabel.TabIndex = 2;
            nameLabel.Text = "设备名称";
            // 
            // ipText
            // 
            ipText.Location = new Point(90, 94);
            ipText.Name = "ipText";
            ipText.Size = new Size(186, 35);
            ipText.TabIndex = 5;
            // 
            // ipLabel
            // 
            ipLabel.Location = new Point(12, 94);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(75, 35);
            ipLabel.TabIndex = 4;
            ipLabel.Text = "设备IP";
            // 
            // portText
            // 
            portText.Location = new Point(90, 135);
            portText.Name = "portText";
            portText.Size = new Size(186, 35);
            portText.TabIndex = 7;
            // 
            // port
            // 
            port.Location = new Point(12, 135);
            port.Name = "port";
            port.Size = new Size(75, 35);
            port.TabIndex = 6;
            port.Text = "设备端口";
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(164, 176);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(75, 40);
            saveBtn.TabIndex = 8;
            saveBtn.Text = "保存";
            saveBtn.Click += saveBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(47, 176);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 40);
            cancelBtn.TabIndex = 9;
            cancelBtn.Text = "取消";
            cancelBtn.Click += cancelBtn_Click;
            // 
            // AddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 219);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddForm";
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
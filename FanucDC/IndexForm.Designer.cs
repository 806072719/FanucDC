using AntdUI;
using System.Drawing;
using System.Windows.Forms;

namespace FanucDC
{
    partial class IndexForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexForm));
            logPanel = new AntdUI.Panel();
            info = new ListBox();
            listPanel = new AntdUI.Panel();
            labelTime1 = new LabelTime();
            delBtn = new AntdUI.Button();
            addBtn = new AntdUI.Button();
            table1 = new Table();
            infoPanel = new AntdUI.Panel();
            input11 = new Input();
            label11 = new AntdUI.Label();
            alarmText = new Input();
            alarmLabel = new AntdUI.Label();
            statusText = new Input();
            statusLabel = new AntdUI.Label();
            circleText = new Input();
            circleLabel = new AntdUI.Label();
            runText = new Input();
            runLabel = new AntdUI.Label();
            openText = new Input();
            openLabel = new AntdUI.Label();
            input5 = new Input();
            label5 = new AntdUI.Label();
            totalText = new Input();
            totalLabel = new AntdUI.Label();
            currentText = new Input();
            currentLabel = new AntdUI.Label();
            input2 = new Input();
            label2 = new AntdUI.Label();
            programText = new Input();
            programLabel = new AntdUI.Label();
            ipText = new Input();
            ipLabel = new AntdUI.Label();
            menuStrip1 = new MenuStrip();
            helpToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            打开ToolStripMenuItem = new ToolStripMenuItem();
            logPanel.SuspendLayout();
            listPanel.SuspendLayout();
            infoPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // logPanel
            // 
            logPanel.BackColor = Color.FromArgb(245, 247, 250);
            logPanel.Controls.Add(info);
            logPanel.Dock = DockStyle.Bottom;
            logPanel.Location = new Point(0, 544);
            logPanel.Name = "logPanel";
            logPanel.Padding = new Padding(8);
            logPanel.Size = new Size(1080, 150);
            logPanel.TabIndex = 0;
            logPanel.Text = "info";
            // 
            // info
            // 
            info.BackColor = Color.White;
            info.BorderStyle = BorderStyle.None;
            info.Dock = DockStyle.Fill;
            info.Font = new Font("Microsoft YaHei UI", 9F);
            info.ForeColor = Color.FromArgb(51, 51, 51);
            info.FormattingEnabled = true;
            info.ItemHeight = 17;
            info.Location = new Point(8, 8);
            info.Margin = new Padding(0);
            info.Name = "info";
            info.Size = new Size(1064, 134);
            info.TabIndex = 0;
            // 
            // listPanel
            // 
            listPanel.BackColor = Color.FromArgb(245, 247, 250);
            listPanel.Controls.Add(labelTime1);
            listPanel.Controls.Add(delBtn);
            listPanel.Controls.Add(addBtn);
            listPanel.Controls.Add(table1);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Location = new Point(0, 245);
            listPanel.Name = "listPanel";
            listPanel.Padding = new Padding(12);
            listPanel.Size = new Size(1080, 299);
            listPanel.TabIndex = 2;
            // 
            // labelTime1
            // 
            labelTime1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            labelTime1.ForeColor = Color.FromArgb(30, 136, 229);
            labelTime1.Location = new Point(20, 20);
            labelTime1.Name = "labelTime1";
            labelTime1.Size = new Size(140, 30);
            labelTime1.TabIndex = 3;
            labelTime1.Text = "10:19:02";
            // 
            // delBtn
            // 
            delBtn.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            delBtn.Location = new Point(20, 120);
            delBtn.Name = "delBtn";
            delBtn.Size = new Size(140, 40);
            delBtn.TabIndex = 2;
            delBtn.Text = "删除设备";
            delBtn.Type = TTypeMini.Error;
            delBtn.Click += delBtn_Click;
            // 
            // addBtn
            // 
            addBtn.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            addBtn.Location = new Point(20, 60);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(140, 40);
            addBtn.TabIndex = 1;
            addBtn.Text = "添加设备";
            addBtn.Type = TTypeMini.Primary;
            addBtn.Click += addBtn_Click;
            // 
            // table1
            // 
            table1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            table1.BackColor = Color.White;
            table1.Font = new Font("Microsoft YaHei UI", 9F);
            table1.Location = new Point(180, 12);
            table1.Name = "table1";
            table1.Size = new Size(888, 275);
            table1.TabIndex = 0;
            table1.Text = "设备列表";
            // 
            // infoPanel
            // 
            infoPanel.BackColor = Color.White;
            infoPanel.Controls.Add(input11);
            infoPanel.Controls.Add(label11);
            infoPanel.Controls.Add(alarmText);
            infoPanel.Controls.Add(alarmLabel);
            infoPanel.Controls.Add(statusText);
            infoPanel.Controls.Add(statusLabel);
            infoPanel.Controls.Add(circleText);
            infoPanel.Controls.Add(circleLabel);
            infoPanel.Controls.Add(runText);
            infoPanel.Controls.Add(runLabel);
            infoPanel.Controls.Add(openText);
            infoPanel.Controls.Add(openLabel);
            infoPanel.Controls.Add(input5);
            infoPanel.Controls.Add(label5);
            infoPanel.Controls.Add(totalText);
            infoPanel.Controls.Add(totalLabel);
            infoPanel.Controls.Add(currentText);
            infoPanel.Controls.Add(currentLabel);
            infoPanel.Controls.Add(input2);
            infoPanel.Controls.Add(label2);
            infoPanel.Controls.Add(programText);
            infoPanel.Controls.Add(programLabel);
            infoPanel.Controls.Add(ipText);
            infoPanel.Controls.Add(ipLabel);
            infoPanel.Dock = DockStyle.Top;
            infoPanel.Location = new Point(0, 25);
            infoPanel.Name = "infoPanel";
            infoPanel.Padding = new Padding(40, 20, 40, 20);
            infoPanel.Size = new Size(1080, 220);
            infoPanel.TabIndex = 1;
            // 
            // input11
            // 
            input11.Enabled = false;
            input11.Location = new Point(730, 170);
            input11.Name = "input11";
            input11.Size = new Size(180, 38);
            input11.TabIndex = 23;
            // 
            // label11
            // 
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Microsoft YaHei UI", 9F);
            label11.ForeColor = Color.FromArgb(80, 80, 80);
            label11.Location = new Point(640, 170);
            label11.Name = "label11";
            label11.Size = new Size(80, 38);
            label11.TabIndex = 22;
            label11.Text = "选择IP";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // alarmText
            // 
            alarmText.Enabled = false;
            alarmText.Location = new Point(430, 170);
            alarmText.Name = "alarmText";
            alarmText.Size = new Size(180, 38);
            alarmText.TabIndex = 21;
            // 
            // alarmLabel
            // 
            alarmLabel.BackColor = Color.Transparent;
            alarmLabel.Font = new Font("Microsoft YaHei UI", 9F);
            alarmLabel.ForeColor = Color.FromArgb(80, 80, 80);
            alarmLabel.Location = new Point(340, 170);
            alarmLabel.Name = "alarmLabel";
            alarmLabel.Size = new Size(80, 38);
            alarmLabel.TabIndex = 20;
            alarmLabel.Text = "告警状态";
            alarmLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // statusText
            // 
            statusText.Enabled = false;
            statusText.Location = new Point(130, 170);
            statusText.Name = "statusText";
            statusText.Size = new Size(180, 38);
            statusText.TabIndex = 19;
            // 
            // statusLabel
            // 
            statusLabel.BackColor = Color.Transparent;
            statusLabel.Font = new Font("Microsoft YaHei UI", 9F);
            statusLabel.ForeColor = Color.FromArgb(80, 80, 80);
            statusLabel.Location = new Point(40, 170);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(80, 38);
            statusLabel.TabIndex = 18;
            statusLabel.Text = "当前状态";
            statusLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // circleText
            // 
            circleText.Enabled = false;
            circleText.Location = new Point(730, 120);
            circleText.Name = "circleText";
            circleText.Size = new Size(180, 38);
            circleText.TabIndex = 17;
            // 
            // circleLabel
            // 
            circleLabel.BackColor = Color.Transparent;
            circleLabel.Font = new Font("Microsoft YaHei UI", 9F);
            circleLabel.ForeColor = Color.FromArgb(80, 80, 80);
            circleLabel.Location = new Point(640, 120);
            circleLabel.Name = "circleLabel";
            circleLabel.Size = new Size(80, 38);
            circleLabel.TabIndex = 16;
            circleLabel.Text = "循环时间";
            circleLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // runText
            // 
            runText.Enabled = false;
            runText.Location = new Point(430, 120);
            runText.Name = "runText";
            runText.Size = new Size(180, 38);
            runText.TabIndex = 15;
            // 
            // runLabel
            // 
            runLabel.BackColor = Color.Transparent;
            runLabel.Font = new Font("Microsoft YaHei UI", 9F);
            runLabel.ForeColor = Color.FromArgb(80, 80, 80);
            runLabel.Location = new Point(340, 120);
            runLabel.Name = "runLabel";
            runLabel.Size = new Size(80, 38);
            runLabel.TabIndex = 14;
            runLabel.Text = "运行时间";
            runLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // openText
            // 
            openText.Enabled = false;
            openText.Location = new Point(130, 120);
            openText.Name = "openText";
            openText.Size = new Size(180, 38);
            openText.TabIndex = 13;
            // 
            // openLabel
            // 
            openLabel.BackColor = Color.Transparent;
            openLabel.Font = new Font("Microsoft YaHei UI", 9F);
            openLabel.ForeColor = Color.FromArgb(80, 80, 80);
            openLabel.Location = new Point(40, 120);
            openLabel.Name = "openLabel";
            openLabel.Size = new Size(80, 38);
            openLabel.TabIndex = 12;
            openLabel.Text = "开机时间";
            openLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // input5
            // 
            input5.Enabled = false;
            input5.Location = new Point(730, 70);
            input5.Name = "input5";
            input5.Size = new Size(180, 38);
            input5.TabIndex = 11;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Microsoft YaHei UI", 9F);
            label5.ForeColor = Color.FromArgb(80, 80, 80);
            label5.Location = new Point(640, 70);
            label5.Name = "label5";
            label5.Size = new Size(80, 38);
            label5.TabIndex = 10;
            label5.Text = "选择IP";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // totalText
            // 
            totalText.Enabled = false;
            totalText.Location = new Point(430, 70);
            totalText.Name = "totalText";
            totalText.Size = new Size(180, 38);
            totalText.TabIndex = 9;
            // 
            // totalLabel
            // 
            totalLabel.BackColor = Color.Transparent;
            totalLabel.Font = new Font("Microsoft YaHei UI", 9F);
            totalLabel.ForeColor = Color.FromArgb(80, 80, 80);
            totalLabel.Location = new Point(340, 70);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(80, 38);
            totalLabel.TabIndex = 8;
            totalLabel.Text = "总共数量";
            totalLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // currentText
            // 
            currentText.Enabled = false;
            currentText.Location = new Point(130, 70);
            currentText.Name = "currentText";
            currentText.Size = new Size(180, 38);
            currentText.TabIndex = 7;
            // 
            // currentLabel
            // 
            currentLabel.BackColor = Color.Transparent;
            currentLabel.Font = new Font("Microsoft YaHei UI", 9F);
            currentLabel.ForeColor = Color.FromArgb(80, 80, 80);
            currentLabel.Location = new Point(40, 70);
            currentLabel.Name = "currentLabel";
            currentLabel.Size = new Size(80, 38);
            currentLabel.TabIndex = 6;
            currentLabel.Text = "一次数量";
            currentLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // input2
            // 
            input2.Enabled = false;
            input2.Location = new Point(730, 20);
            input2.Name = "input2";
            input2.Size = new Size(180, 38);
            input2.TabIndex = 5;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft YaHei UI", 9F);
            label2.ForeColor = Color.FromArgb(80, 80, 80);
            label2.Location = new Point(640, 20);
            label2.Name = "label2";
            label2.Size = new Size(80, 38);
            label2.TabIndex = 4;
            label2.Text = "选择IP";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // programText
            // 
            programText.Enabled = false;
            programText.Location = new Point(430, 20);
            programText.Name = "programText";
            programText.Size = new Size(180, 38);
            programText.TabIndex = 3;
            // 
            // programLabel
            // 
            programLabel.BackColor = Color.Transparent;
            programLabel.Font = new Font("Microsoft YaHei UI", 9F);
            programLabel.ForeColor = Color.FromArgb(80, 80, 80);
            programLabel.Location = new Point(340, 20);
            programLabel.Name = "programLabel";
            programLabel.Size = new Size(80, 38);
            programLabel.TabIndex = 2;
            programLabel.Text = "程序名称";
            programLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ipText
            // 
            ipText.Enabled = false;
            ipText.Location = new Point(130, 20);
            ipText.Name = "ipText";
            ipText.Size = new Size(180, 38);
            ipText.TabIndex = 1;
            // 
            // ipLabel
            // 
            ipLabel.BackColor = Color.Transparent;
            ipLabel.Font = new Font("Microsoft YaHei UI", 9F);
            ipLabel.ForeColor = Color.FromArgb(80, 80, 80);
            ipLabel.Location = new Point(40, 20);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(80, 38);
            ipLabel.TabIndex = 0;
            ipLabel.Text = "选择IP";
            ipLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(245, 247, 250);
            menuStrip1.Font = new Font("Microsoft YaHei UI", 9F);
            menuStrip1.Items.AddRange(new ToolStripItem[] { helpToolStripMenuItem, 打开ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1080, 25);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { quitToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 21);
            helpToolStripMenuItem.Text = "打开";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(180, 22);
            quitToolStripMenuItem.Text = "退出";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // 打开ToolStripMenuItem
            // 
            打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            打开ToolStripMenuItem.Size = new Size(44, 21);
            打开ToolStripMenuItem.Text = "帮助";
            // 
            // IndexForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1080, 694);
            Controls.Add(listPanel);
            Controls.Add(infoPanel);
            Controls.Add(logPanel);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "IndexForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fanuc DC";
            logPanel.ResumeLayout(false);
            listPanel.ResumeLayout(false);
            infoPanel.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private AntdUI.Panel logPanel;
        private AntdUI.Panel listPanel;
        private AntdUI.Panel infoPanel;
        private AntdUI.Table table1;
        private AntdUI.LabelTime labelTime1;
        private AntdUI.Button delBtn;
        private AntdUI.Button addBtn;
        private AntdUI.Label ipLabel;
        private AntdUI.Input ipText;
        private AntdUI.Input input11;
        private AntdUI.Label label11;
        private AntdUI.Input alarmText;
        private AntdUI.Label alarmLabel;
        private AntdUI.Input statusText;
        private AntdUI.Label statusLabel;
        private AntdUI.Input circleText;
        private AntdUI.Label circleLabel;
        private AntdUI.Input runText;
        private AntdUI.Label runLabel;
        private AntdUI.Input openText;
        private AntdUI.Label openLabel;
        private AntdUI.Input input5;
        private AntdUI.Label label5;
        private AntdUI.Input totalText;
        private AntdUI.Label totalLabel;
        private AntdUI.Input currentText;
        private AntdUI.Label currentLabel;
        private AntdUI.Input input2;
        private AntdUI.Label label2;
        private AntdUI.Input programText;
        private AntdUI.Label programLabel;
        private System.Windows.Forms.ListBox info;
        // 新增菜单控件声明
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem 打开ToolStripMenuItem;
    }
}
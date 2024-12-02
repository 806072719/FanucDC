using AntdUI;

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
            logPanel.SuspendLayout();
            listPanel.SuspendLayout();
            infoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // logPanel
            // 
            logPanel.Controls.Add(info);
            logPanel.Dock = DockStyle.Bottom;
            logPanel.Location = new Point(0, 480);
            logPanel.Name = "logPanel";
            logPanel.Size = new Size(1041, 190);
            logPanel.TabIndex = 0;
            logPanel.Text = "panel1";
            // 
            // info
            // 
            info.Dock = DockStyle.Fill;
            info.FormattingEnabled = true;
            info.ItemHeight = 17;
            info.Location = new Point(0, 0);
            info.Name = "info";
            info.Size = new Size(1041, 190);
            info.TabIndex = 0;
            // 
            // listPanel
            // 
            listPanel.Controls.Add(labelTime1);
            listPanel.Controls.Add(delBtn);
            listPanel.Controls.Add(addBtn);
            listPanel.Controls.Add(table1);
            listPanel.Dock = DockStyle.Top;
            listPanel.Location = new Point(0, 0);
            listPanel.Name = "listPanel";
            listPanel.Size = new Size(1041, 191);
            listPanel.TabIndex = 1;
            listPanel.Text = "panel1";
            // 
            // labelTime1
            // 
            labelTime1.Location = new Point(12, 12);
            labelTime1.Name = "labelTime1";
            labelTime1.Size = new Size(75, 23);
            labelTime1.TabIndex = 3;
            labelTime1.Text = "labelTime1";
            // 
            // delBtn
            // 
            delBtn.Location = new Point(12, 121);
            delBtn.Name = "delBtn";
            delBtn.Size = new Size(75, 40);
            delBtn.TabIndex = 2;
            delBtn.Text = "删除设备";
            delBtn.Type = TTypeMini.Error;
            // 
            // addBtn
            // 
            addBtn.Location = new Point(12, 52);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(75, 39);
            addBtn.TabIndex = 1;
            addBtn.Text = "添加设备";
            addBtn.Type = TTypeMini.Primary;
            addBtn.Click += addBtn_Click;
            // 
            // table1
            // 
            table1.Dock = DockStyle.Right;
            table1.Location = new Point(110, 0);
            table1.Name = "table1";
            table1.Size = new Size(931, 191);
            table1.TabIndex = 0;
            table1.Text = "设备列表";
            // 
            // infoPanel
            // 
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
            infoPanel.Dock = DockStyle.Fill;
            infoPanel.Location = new Point(0, 191);
            infoPanel.Name = "infoPanel";
            infoPanel.Size = new Size(1041, 289);
            infoPanel.TabIndex = 2;
            infoPanel.Text = "panel2";
            // 
            // input11
            // 
            input11.Enabled = false;
            input11.Location = new Point(749, 213);
            input11.Name = "input11";
            input11.Size = new Size(162, 47);
            input11.TabIndex = 23;
            // 
            // label11
            // 
            label11.BackColor = Color.White;
            label11.Location = new Point(685, 213);
            label11.Name = "label11";
            label11.Size = new Size(58, 47);
            label11.TabIndex = 22;
            label11.Text = "选择IP";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // alarmText
            // 
            alarmText.Enabled = false;
            alarmText.Location = new Point(458, 213);
            alarmText.Name = "alarmText";
            alarmText.Size = new Size(162, 47);
            alarmText.TabIndex = 21;
            // 
            // alarmLabel
            // 
            alarmLabel.BackColor = Color.White;
            alarmLabel.Location = new Point(394, 213);
            alarmLabel.Name = "alarmLabel";
            alarmLabel.Size = new Size(58, 47);
            alarmLabel.TabIndex = 20;
            alarmLabel.Text = "告警状态";
            alarmLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // statusText
            // 
            statusText.Enabled = false;
            statusText.Location = new Point(174, 213);
            statusText.Name = "statusText";
            statusText.Size = new Size(162, 47);
            statusText.TabIndex = 19;
            // 
            // statusLabel
            // 
            statusLabel.BackColor = Color.White;
            statusLabel.Location = new Point(110, 213);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(58, 47);
            statusLabel.TabIndex = 18;
            statusLabel.Text = "当前状态";
            statusLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // circleText
            // 
            circleText.Enabled = false;
            circleText.Location = new Point(749, 140);
            circleText.Name = "circleText";
            circleText.Size = new Size(162, 47);
            circleText.TabIndex = 17;
            // 
            // circleLabel
            // 
            circleLabel.BackColor = Color.White;
            circleLabel.Location = new Point(685, 140);
            circleLabel.Name = "circleLabel";
            circleLabel.Size = new Size(58, 47);
            circleLabel.TabIndex = 16;
            circleLabel.Text = "循环时间";
            circleLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // runText
            // 
            runText.Enabled = false;
            runText.Location = new Point(458, 140);
            runText.Name = "runText";
            runText.Size = new Size(162, 47);
            runText.TabIndex = 15;
            // 
            // runLabel
            // 
            runLabel.BackColor = Color.White;
            runLabel.Location = new Point(394, 140);
            runLabel.Name = "runLabel";
            runLabel.Size = new Size(58, 47);
            runLabel.TabIndex = 14;
            runLabel.Text = "运行时间";
            runLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // openText
            // 
            openText.Enabled = false;
            openText.Location = new Point(174, 140);
            openText.Name = "openText";
            openText.Size = new Size(162, 47);
            openText.TabIndex = 13;
            // 
            // openLabel
            // 
            openLabel.BackColor = Color.White;
            openLabel.Location = new Point(110, 140);
            openLabel.Name = "openLabel";
            openLabel.Size = new Size(58, 47);
            openLabel.TabIndex = 12;
            openLabel.Text = "开机时间";
            openLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // input5
            // 
            input5.Enabled = false;
            input5.Location = new Point(749, 74);
            input5.Name = "input5";
            input5.Size = new Size(162, 47);
            input5.TabIndex = 11;
            // 
            // label5
            // 
            label5.BackColor = Color.White;
            label5.Location = new Point(685, 74);
            label5.Name = "label5";
            label5.Size = new Size(58, 47);
            label5.TabIndex = 10;
            label5.Text = "选择IP";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // totalText
            // 
            totalText.Enabled = false;
            totalText.Location = new Point(458, 74);
            totalText.Name = "totalText";
            totalText.Size = new Size(162, 47);
            totalText.TabIndex = 9;
            // 
            // totalLabel
            // 
            totalLabel.BackColor = Color.White;
            totalLabel.Location = new Point(394, 74);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(58, 47);
            totalLabel.TabIndex = 8;
            totalLabel.Text = "总共数量";
            totalLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // currentText
            // 
            currentText.Enabled = false;
            currentText.Location = new Point(174, 74);
            currentText.Name = "currentText";
            currentText.Size = new Size(162, 47);
            currentText.TabIndex = 7;
            // 
            // currentLabel
            // 
            currentLabel.BackColor = Color.White;
            currentLabel.Location = new Point(110, 74);
            currentLabel.Name = "currentLabel";
            currentLabel.Size = new Size(58, 47);
            currentLabel.TabIndex = 6;
            currentLabel.Text = "一次数量";
            currentLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // input2
            // 
            input2.Enabled = false;
            input2.Location = new Point(749, 3);
            input2.Name = "input2";
            input2.Size = new Size(162, 47);
            input2.TabIndex = 5;
            // 
            // label2
            // 
            label2.BackColor = Color.White;
            label2.Location = new Point(685, 3);
            label2.Name = "label2";
            label2.Size = new Size(58, 47);
            label2.TabIndex = 4;
            label2.Text = "选择IP";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // programText
            // 
            programText.Enabled = false;
            programText.Location = new Point(458, 6);
            programText.Name = "programText";
            programText.Size = new Size(162, 47);
            programText.TabIndex = 3;
            // 
            // programLabel
            // 
            programLabel.BackColor = Color.White;
            programLabel.Location = new Point(394, 6);
            programLabel.Name = "programLabel";
            programLabel.Size = new Size(58, 47);
            programLabel.TabIndex = 2;
            programLabel.Text = "程序名称";
            programLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ipText
            // 
            ipText.Enabled = false;
            ipText.Location = new Point(174, 6);
            ipText.Name = "ipText";
            ipText.Size = new Size(162, 47);
            ipText.TabIndex = 1;
            // 
            // ipLabel
            // 
            ipLabel.BackColor = Color.White;
            ipLabel.Location = new Point(110, 6);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(58, 47);
            ipLabel.TabIndex = 0;
            ipLabel.Text = "选择IP";
            ipLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // IndexForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1041, 670);
            Controls.Add(infoPanel);
            Controls.Add(listPanel);
            Controls.Add(logPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "IndexForm";
            Text = "Fanuc DC";
            logPanel.ResumeLayout(false);
            listPanel.ResumeLayout(false);
            infoPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Panel logPanel;
        private AntdUI.Panel listPanel;
        private AntdUI.Panel infoPanel;
        private Table table1;
        private LabelTime labelTime1;
        private AntdUI.Button delBtn;
        private AntdUI.Button addBtn;
        private AntdUI.Label ipLabel;
        private Input ipText;
        private Input input11;
        private AntdUI.Label label11;
        private Input alarmText;
        private AntdUI.Label alarmLabel;
        private Input statusText;
        private AntdUI.Label statusLabel;
        private Input circleText;
        private AntdUI.Label circleLabel;
        private Input runText;
        private AntdUI.Label runLabel;
        private Input openText;
        private AntdUI.Label openLabel;
        private Input input5;
        private AntdUI.Label label5;
        private Input totalText;
        private AntdUI.Label totalLabel;
        private Input currentText;
        private AntdUI.Label currentLabel;
        private Input input2;
        private AntdUI.Label label2;
        private Input programText;
        private AntdUI.Label programLabel;
        private ListBox info;
    }
}
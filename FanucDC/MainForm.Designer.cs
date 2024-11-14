using FanucDC.pojo;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace FanucDC
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                foreach (var item in EQUIPMENT_TIMER_DICT)
                {
                    item.Value.Dispose();
                }
                EQUIPMENT_TIMER_DICT.Clear();


                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView1 = new DataGridView();
            equipmentName = new DataGridViewTextBoxColumn();
            equipmentIp = new DataGridViewTextBoxColumn();
            equipmentPort = new DataGridViewTextBoxColumn();
            equipmentStatus = new DataGridViewTextBoxColumn();
            lastTimeColumn = new DataGridViewTextBoxColumn();
            oneCountLabel = new LinkLabel();
            oneCountText = new TextBox();
            timeText = new TextBox();
            threadNoText = new TextBox();
            info = new ListBox();
            clearLogBtn = new System.Windows.Forms.Button();
            totalCount = new LinkLabel();
            totalCountText = new TextBox();
            programNameLabel = new LinkLabel();
            programNameText = new TextBox();
            ipText = new TextBox();
            ipLabel = new LinkLabel();
            openTimeText = new TextBox();
            openTimeLabel = new LinkLabel();
            runTimeText = new TextBox();
            runTextLabel = new LinkLabel();
            circleTimeText = new TextBox();
            circleTimeLabel = new LinkLabel();
            textBox5 = new TextBox();
            linkLabel5 = new LinkLabel();
            statusText = new TextBox();
            statusLabel = new LinkLabel();
            alarmText = new TextBox();
            alarmLabel = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { equipmentName, equipmentIp, equipmentPort, equipmentStatus, lastTimeColumn });
            dataGridView1.Location = new Point(0, -1);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(993, 173);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // equipmentName
            // 
            equipmentName.FillWeight = 300F;
            equipmentName.HeaderText = "设备名称";
            equipmentName.Name = "equipmentName";
            equipmentName.Width = 300;
            // 
            // equipmentIp
            // 
            equipmentIp.FillWeight = 200F;
            equipmentIp.HeaderText = "设备IP";
            equipmentIp.Name = "equipmentIp";
            equipmentIp.Width = 200;
            // 
            // equipmentPort
            // 
            equipmentPort.FillWeight = 150F;
            equipmentPort.HeaderText = "端口";
            equipmentPort.Name = "equipmentPort";
            equipmentPort.Width = 150;
            // 
            // equipmentStatus
            // 
            equipmentStatus.FillWeight = 150F;
            equipmentStatus.HeaderText = "连接状态";
            equipmentStatus.Name = "equipmentStatus";
            equipmentStatus.Width = 150;
            // 
            // lastTimeColumn
            // 
            lastTimeColumn.FillWeight = 150F;
            lastTimeColumn.HeaderText = "上次连接时间";
            lastTimeColumn.Name = "lastTimeColumn";
            lastTimeColumn.Width = 150;
            // 
            // oneCountLabel
            // 
            oneCountLabel.AutoSize = true;
            oneCountLabel.Location = new Point(7, 238);
            oneCountLabel.Name = "oneCountLabel";
            oneCountLabel.Size = new Size(80, 17);
            oneCountLabel.TabIndex = 1;
            oneCountLabel.TabStop = true;
            oneCountLabel.Text = "一次生产数量";
            oneCountLabel.LinkClicked += oneCountLabel_LinkClicked;
            // 
            // oneCountText
            // 
            oneCountText.Location = new Point(93, 235);
            oneCountText.Name = "oneCountText";
            oneCountText.ReadOnly = true;
            oneCountText.Size = new Size(150, 23);
            oneCountText.TabIndex = 2;
            // 
            // timeText
            // 
            timeText.Location = new Point(12, 720);
            timeText.Name = "timeText";
            timeText.ReadOnly = true;
            timeText.Size = new Size(134, 23);
            timeText.TabIndex = 3;
            // 
            // threadNoText
            // 
            threadNoText.Location = new Point(916, 720);
            threadNoText.Name = "threadNoText";
            threadNoText.ReadOnly = true;
            threadNoText.Size = new Size(77, 23);
            threadNoText.TabIndex = 4;
            // 
            // info
            // 
            info.FormattingEnabled = true;
            info.ItemHeight = 17;
            info.Location = new Point(12, 537);
            info.Name = "info";
            info.Size = new Size(981, 174);
            info.TabIndex = 5;
            info.SelectedIndexChanged += info_SelectedIndexChanged;
            // 
            // clearLogBtn
            // 
            clearLogBtn.Location = new Point(12, 508);
            clearLogBtn.Name = "clearLogBtn";
            clearLogBtn.Size = new Size(75, 23);
            clearLogBtn.TabIndex = 6;
            clearLogBtn.Text = "清理日志";
            clearLogBtn.UseVisualStyleBackColor = true;
            clearLogBtn.Click += button1_Click;
            // 
            // totalCount
            // 
            totalCount.AutoSize = true;
            totalCount.Location = new Point(264, 238);
            totalCount.Name = "totalCount";
            totalCount.Size = new Size(68, 17);
            totalCount.TabIndex = 7;
            totalCount.TabStop = true;
            totalCount.Text = "总生产数量";
            // 
            // totalCountText
            // 
            totalCountText.Location = new Point(338, 232);
            totalCountText.Name = "totalCountText";
            totalCountText.ReadOnly = true;
            totalCountText.Size = new Size(150, 23);
            totalCountText.TabIndex = 8;
            // 
            // programNameLabel
            // 
            programNameLabel.AutoSize = true;
            programNameLabel.Location = new Point(276, 184);
            programNameLabel.Name = "programNameLabel";
            programNameLabel.Size = new Size(56, 17);
            programNameLabel.TabIndex = 9;
            programNameLabel.TabStop = true;
            programNameLabel.Text = "程序名称";
            // 
            // programNameText
            // 
            programNameText.Location = new Point(338, 181);
            programNameText.Name = "programNameText";
            programNameText.ReadOnly = true;
            programNameText.Size = new Size(150, 23);
            programNameText.TabIndex = 10;
            // 
            // ipText
            // 
            ipText.Location = new Point(93, 181);
            ipText.Name = "ipText";
            ipText.ReadOnly = true;
            ipText.Size = new Size(150, 23);
            ipText.TabIndex = 12;
            ipText.TextChanged += textBox1_TextChanged;
            // 
            // ipLabel
            // 
            ipLabel.AutoSize = true;
            ipLabel.Location = new Point(31, 184);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(43, 17);
            ipLabel.TabIndex = 11;
            ipLabel.TabStop = true;
            ipLabel.Text = "当前IP";
            // 
            // openTimeText
            // 
            openTimeText.Location = new Point(93, 284);
            openTimeText.Name = "openTimeText";
            openTimeText.ReadOnly = true;
            openTimeText.Size = new Size(150, 23);
            openTimeText.TabIndex = 14;
            // 
            // openTimeLabel
            // 
            openTimeLabel.AutoSize = true;
            openTimeLabel.Location = new Point(31, 288);
            openTimeLabel.Name = "openTimeLabel";
            openTimeLabel.Size = new Size(56, 17);
            openTimeLabel.TabIndex = 13;
            openTimeLabel.TabStop = true;
            openTimeLabel.Text = "开机时间";
            // 
            // runTimeText
            // 
            runTimeText.Location = new Point(338, 281);
            runTimeText.Name = "runTimeText";
            runTimeText.ReadOnly = true;
            runTimeText.Size = new Size(150, 23);
            runTimeText.TabIndex = 16;
            // 
            // runTextLabel
            // 
            runTextLabel.AutoSize = true;
            runTextLabel.Location = new Point(275, 284);
            runTextLabel.Name = "runTextLabel";
            runTextLabel.Size = new Size(56, 17);
            runTextLabel.TabIndex = 15;
            runTextLabel.TabStop = true;
            runTextLabel.Text = "运行时间";
            // 
            // circleTimeText
            // 
            circleTimeText.Location = new Point(93, 333);
            circleTimeText.Name = "circleTimeText";
            circleTimeText.ReadOnly = true;
            circleTimeText.Size = new Size(150, 23);
            circleTimeText.TabIndex = 18;
            // 
            // circleTimeLabel
            // 
            circleTimeLabel.AutoSize = true;
            circleTimeLabel.Location = new Point(31, 336);
            circleTimeLabel.Name = "circleTimeLabel";
            circleTimeLabel.Size = new Size(56, 17);
            circleTimeLabel.TabIndex = 17;
            circleTimeLabel.TabStop = true;
            circleTimeLabel.Text = "循环时间";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(338, 330);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(150, 23);
            textBox5.TabIndex = 20;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.Location = new Point(252, 333);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(80, 17);
            linkLabel5.TabIndex = 19;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "一次生产数量";
            // 
            // statusText
            // 
            statusText.Location = new Point(93, 376);
            statusText.Name = "statusText";
            statusText.ReadOnly = true;
            statusText.Size = new Size(150, 23);
            statusText.TabIndex = 22;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(31, 379);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(56, 17);
            statusLabel.TabIndex = 21;
            statusLabel.TabStop = true;
            statusLabel.Text = "当前状态";
            // 
            // alarmText
            // 
            alarmText.Location = new Point(338, 376);
            alarmText.Name = "alarmText";
            alarmText.ReadOnly = true;
            alarmText.Size = new Size(150, 23);
            alarmText.TabIndex = 24;
            // 
            // alarmLabel
            // 
            alarmLabel.AutoSize = true;
            alarmLabel.Location = new Point(276, 379);
            alarmLabel.Name = "alarmLabel";
            alarmLabel.Size = new Size(56, 17);
            alarmLabel.TabIndex = 23;
            alarmLabel.TabStop = true;
            alarmLabel.Text = "警告状态";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1002, 750);
            Controls.Add(alarmText);
            Controls.Add(alarmLabel);
            Controls.Add(statusText);
            Controls.Add(statusLabel);
            Controls.Add(textBox5);
            Controls.Add(linkLabel5);
            Controls.Add(circleTimeText);
            Controls.Add(circleTimeLabel);
            Controls.Add(runTimeText);
            Controls.Add(runTextLabel);
            Controls.Add(openTimeText);
            Controls.Add(openTimeLabel);
            Controls.Add(ipText);
            Controls.Add(ipLabel);
            Controls.Add(programNameText);
            Controls.Add(programNameLabel);
            Controls.Add(totalCountText);
            Controls.Add(totalCount);
            Controls.Add(clearLogBtn);
            Controls.Add(info);
            Controls.Add(threadNoText);
            Controls.Add(timeText);
            Controls.Add(oneCountText);
            Controls.Add(oneCountLabel);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Fanuc DC";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private LinkLabel oneCountLabel;
        private TextBox oneCountText;
        private TextBox timeText;
        private TextBox threadNoText;
        private ListBox info;


        public void LogInfo(string text)
        {
            Log(text, Color.Green);
        }

        public void LogError(string text)
        {
            Log(text, Color.Red);
        }

        public void LogWarn(string text)
        {
            Log(text, Color.Yellow);
        }

        private void Log(string text, Color color)
        {
            if (this.IsDisposed)
            {
                return;
            }
            if (this.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new Action(() => {
                    Log(text, color);
                }));
                return;
            }
            this.info.Items.Add(DateTime.Now.ToString() + " " + text);
        }

        private System.Windows.Forms.Button clearLogBtn;



        private void refreshTime(object? sender, ElapsedEventArgs e)
        {
            if (this.IsDisposed)
                return;

            if (this.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new Action(() => {
                    refreshTime(sender, e);
                }));
                return;
            }
            this.timeText.Text = DateTime.Now.ToString();
        }

        private void refreshTraceData(pojo.Trace trace) 
        {
            if (this.IsDisposed)
                return;

            if (this.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    refreshTraceData(trace);
                }));
                return;
            }
        }

        private void refreshOnline(string ip, int status)
        {
            if (this.IsDisposed)
                return;
            if (this.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    refreshOnline(ip,status);
                }));
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(ip))
                {
                    row.Cells[3].Value = status == 0 ? "正常" : "异常代码" + status;
                    if (status == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Green;
                    } else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                    row.Cells[4].Value = DateTime.Now.ToString();
                    break;
                }
            }
        }



        private DataGridViewTextBoxColumn equipmentName;
        private DataGridViewTextBoxColumn equipmentIp;
        private DataGridViewTextBoxColumn equipmentPort;
        private DataGridViewTextBoxColumn equipmentStatus;
        private DataGridViewTextBoxColumn lastTimeColumn;


        private void refreshData(pojo.Trace trace)
        {
            if (this.IsDisposed)
                return;
            if (this.InvokeRequired && !this.IsDisposed)
            {
                this.Invoke(new Action(() =>
                {
                    refreshData(trace);
                }));
                return;
            }

            oneCountText.Text = trace.CurrentCount.ToString();
            totalCountText.Text = trace.TotalCount.ToString();
            programNameText.Text = trace.ProgramName.ToString();
            statusText.Text = trace.Status.ToString();
            alarmText.Text = trace.Alarm.ToString();
            openTimeText.Text = trace.OpenTime.ToString();
            circleTimeText.Text = trace.CircleTime.ToString();
            runTimeText.Text = trace.RunTime.ToString();
            ipText.Text = trace.Ip.ToString();
            
        }

        private LinkLabel totalCount;
        private TextBox totalCountText;
        private LinkLabel programNameLabel;
        private TextBox programNameText;
        private TextBox ipText;
        private LinkLabel ipLabel;
        private TextBox openTimeText;
        private LinkLabel openTimeLabel;
        private TextBox runTimeText;
        private LinkLabel runTextLabel;
        private TextBox circleTimeText;
        private LinkLabel circleTimeLabel;
        private TextBox textBox5;
        private LinkLabel linkLabel5;
        private TextBox statusText;
        private LinkLabel statusLabel;
        private TextBox alarmText;
        private LinkLabel alarmLabel;
    }

}
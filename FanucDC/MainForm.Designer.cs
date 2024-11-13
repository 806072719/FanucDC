using FanucDC.pojo;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;

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
            clearLogBtn = new Button();
            addBtn = new Button();
            delBtn = new Button();
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
            oneCountLabel.Location = new Point(12, 181);
            oneCountLabel.Name = "oneCountLabel";
            oneCountLabel.Size = new Size(80, 17);
            oneCountLabel.TabIndex = 1;
            oneCountLabel.TabStop = true;
            oneCountLabel.Text = "一次生产数量";
            oneCountLabel.LinkClicked += oneCountLabel_LinkClicked;
            // 
            // oneCountText
            // 
            oneCountText.Location = new Point(101, 178);
            oneCountText.Name = "oneCountText";
            oneCountText.ReadOnly = true;
            oneCountText.Size = new Size(100, 23);
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
            threadNoText.Location = new Point(1038, 720);
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
            info.Size = new Size(1103, 174);
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
            // addBtn
            // 
            addBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addBtn.Location = new Point(1034, 12);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(75, 23);
            addBtn.TabIndex = 7;
            addBtn.Text = "添加设备";
            addBtn.UseVisualStyleBackColor = true;
            // 
            // delBtn
            // 
            delBtn.Location = new Point(1034, 55);
            delBtn.Name = "delBtn";
            delBtn.Size = new Size(75, 23);
            delBtn.TabIndex = 8;
            delBtn.Text = "删除设备";
            delBtn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1124, 750);
            Controls.Add(delBtn);
            Controls.Add(addBtn);
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

        private Button clearLogBtn;



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
                    break;
                }
            }
        }

        private DataGridViewTextBoxColumn equipmentName;
        private DataGridViewTextBoxColumn equipmentIp;
        private DataGridViewTextBoxColumn equipmentPort;
        private DataGridViewTextBoxColumn equipmentStatus;
        private DataGridViewTextBoxColumn lastTimeColumn;
        private Button addBtn;
        private Button delBtn;
    }

}
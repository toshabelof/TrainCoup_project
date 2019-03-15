namespace ModulWrapper
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tBox_path = new System.Windows.Forms.TextBox();
            this.btn_Detect = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDetectionSystem = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.filePickBtn = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.frameCnt = new System.Windows.Forms.Label();
            this.picBox = new ModulWrapper.CustomPicBox(this.components);
            this.picBoxSmall = new ModulWrapper.CustomPicBox(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.Frame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.X1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.X2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btnTreatment = new System.Windows.Forms.Button();
            this.tBoxTreatmentPath = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // tBox_path
            // 
            this.tBox_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBox_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tBox_path.ForeColor = System.Drawing.Color.Gray;
            this.tBox_path.Location = new System.Drawing.Point(10, 15);
            this.tBox_path.Name = "tBox_path";
            this.tBox_path.ReadOnly = true;
            this.tBox_path.Size = new System.Drawing.Size(1027, 20);
            this.tBox_path.TabIndex = 1;
            this.tBox_path.Text = "Press \'File\' Button";
            this.tBox_path.WordWrap = false;
            // 
            // btn_Detect
            // 
            this.btn_Detect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Detect.Location = new System.Drawing.Point(10, 492);
            this.btn_Detect.Name = "btn_Detect";
            this.btn_Detect.Size = new System.Drawing.Size(70, 30);
            this.btn_Detect.TabIndex = 0;
            this.btn_Detect.Text = "PLAY";
            this.btn_Detect.UseVisualStyleBackColor = true;
            this.btn_Detect.Click += new System.EventHandler(this.btn_Detect_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDetectionSystem,
            this.toolStripTimer,
            this.toolStripCounter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1128, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDetectionSystem
            // 
            this.toolStripDetectionSystem.Name = "toolStripDetectionSystem";
            this.toolStripDetectionSystem.Size = new System.Drawing.Size(141, 17);
            this.toolStripDetectionSystem.Text = "toolStripDetectionSystem";
            // 
            // toolStripTimer
            // 
            this.toolStripTimer.Name = "toolStripTimer";
            this.toolStripTimer.Size = new System.Drawing.Size(83, 17);
            this.toolStripTimer.Text = "toolStripTimer";
            // 
            // toolStripCounter
            // 
            this.toolStripCounter.Name = "toolStripCounter";
            this.toolStripCounter.Size = new System.Drawing.Size(95, 17);
            this.toolStripCounter.Text = "toolStripCounter";
            // 
            // filePickBtn
            // 
            this.filePickBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filePickBtn.Location = new System.Drawing.Point(1042, 12);
            this.filePickBtn.Name = "filePickBtn";
            this.filePickBtn.Size = new System.Drawing.Size(75, 26);
            this.filePickBtn.TabIndex = 0;
            this.filePickBtn.Text = "File";
            this.filePickBtn.UseVisualStyleBackColor = true;
            this.filePickBtn.Click += new System.EventHandler(this.btn_filePick_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pauseButton.Location = new System.Drawing.Point(86, 492);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(70, 30);
            this.pauseButton.TabIndex = 6;
            this.pauseButton.Text = "PAUSE";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopButton.Location = new System.Drawing.Point(162, 492);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(70, 30);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // frameCnt
            // 
            this.frameCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.frameCnt.AutoSize = true;
            this.frameCnt.Location = new System.Drawing.Point(266, 502);
            this.frameCnt.Name = "frameCnt";
            this.frameCnt.Size = new System.Drawing.Size(64, 13);
            this.frameCnt.TabIndex = 7;
            this.frameCnt.Text = "Frames: 0/0";
            // 
            // picBox
            // 
            this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(12, 41);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(701, 445);
            this.picBox.TabIndex = 5;
            this.picBox.TabStop = false;
            // 
            // picBoxSmall
            // 
            this.picBoxSmall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxSmall.Location = new System.Drawing.Point(719, 41);
            this.picBoxSmall.Name = "picBoxSmall";
            this.picBoxSmall.Size = new System.Drawing.Size(396, 193);
            this.picBoxSmall.TabIndex = 9;
            this.picBoxSmall.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Frame,
            this.X1,
            this.Y1,
            this.X2,
            this.Y2,
            this.Time});
            this.listView1.Location = new System.Drawing.Point(719, 240);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(396, 246);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Frame
            // 
            this.Frame.Text = "Frame";
            this.Frame.Width = 49;
            // 
            // X1
            // 
            this.X1.Text = "X1";
            this.X1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.X1.Width = 63;
            // 
            // Y1
            // 
            this.Y1.Text = "Y1";
            this.Y1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Y1.Width = 65;
            // 
            // X2
            // 
            this.X2.Text = "X2";
            this.X2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.X2.Width = 65;
            // 
            // Y2
            // 
            this.Y2.Text = "Y2";
            this.Y2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Y2.Width = 59;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(647, 502);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select folder";
            // 
            // btnTreatment
            // 
            this.btnTreatment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTreatment.Location = new System.Drawing.Point(1040, 493);
            this.btnTreatment.Name = "btnTreatment";
            this.btnTreatment.Size = new System.Drawing.Size(75, 30);
            this.btnTreatment.TabIndex = 13;
            this.btnTreatment.Text = "Treatment";
            this.btnTreatment.UseVisualStyleBackColor = true;
            this.btnTreatment.Click += new System.EventHandler(this.btnTreatment_Click);
            // 
            // tBoxTreatmentPath
            // 
            this.tBoxTreatmentPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tBoxTreatmentPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tBoxTreatmentPath.Location = new System.Drawing.Point(719, 498);
            this.tBoxTreatmentPath.Name = "tBoxTreatmentPath";
            this.tBoxTreatmentPath.Size = new System.Drawing.Size(318, 20);
            this.tBoxTreatmentPath.TabIndex = 12;
            this.tBoxTreatmentPath.Text = "Press \'Treatment\' button";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1128, 562);
            this.Controls.Add(this.btnTreatment);
            this.Controls.Add(this.tBoxTreatmentPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.picBoxSmall);
            this.Controls.Add(this.frameCnt);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.filePickBtn);
            this.Controls.Add(this.btn_Detect);
            this.Controls.Add(this.tBox_path);
            this.MinimumSize = new System.Drawing.Size(480, 410);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Train Coup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSmall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btn_Detect;
        public System.Windows.Forms.TextBox tBox_path;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripDetectionSystem;
        public System.Windows.Forms.ToolStripStatusLabel toolStripTimer;
        public System.Windows.Forms.Button filePickBtn;
        public CustomPicBox picBox;
        public System.Windows.Forms.Button pauseButton;
        public System.Windows.Forms.Button stopButton;
        public System.Windows.Forms.Label frameCnt;
        public System.Windows.Forms.ToolStripStatusLabel toolStripCounter;
        public CustomPicBox picBoxSmall;
        private System.Windows.Forms.ColumnHeader Frame;
        private System.Windows.Forms.ColumnHeader X1;
        private System.Windows.Forms.ColumnHeader Y1;
        private System.Windows.Forms.ColumnHeader X2;
        private System.Windows.Forms.ColumnHeader Y2;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTreatment;
        public System.Windows.Forms.TextBox tBoxTreatmentPath;
    }
}


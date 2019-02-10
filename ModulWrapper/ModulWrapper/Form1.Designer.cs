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
            this.button1 = new System.Windows.Forms.Button();
            this.picBoxSmall = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.picBox = new ModulWrapper.CustomPicBox(this.components);
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.frameCnt = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tBox_path
            // 
            this.tBox_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBox_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tBox_path.ForeColor = System.Drawing.Color.Gray;
            this.tBox_path.Location = new System.Drawing.Point(12, 15);
            this.tBox_path.Name = "tBox_path";
            this.tBox_path.ReadOnly = true;
            this.tBox_path.Size = new System.Drawing.Size(838, 20);
            this.tBox_path.TabIndex = 1;
            this.tBox_path.Text = "Press \'File\' Button";
            this.tBox_path.WordWrap = false;
            // 
            // btn_Detect
            // 
            this.btn_Detect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Detect.Location = new System.Drawing.Point(12, 527);
            this.btn_Detect.Name = "btn_Detect";
            this.btn_Detect.Size = new System.Drawing.Size(70, 30);
            this.btn_Detect.TabIndex = 0;
            this.btn_Detect.Text = "PLAY";
            this.btn_Detect.UseVisualStyleBackColor = true;
            this.btn_Detect.Click += new System.EventHandler(this.btn_Detect_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDetectionSystem,
            this.toolStripTimer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 592);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(943, 22);
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
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(856, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_filePick_Click);
            // 
            // picBoxSmall
            // 
            this.picBoxSmall.Location = new System.Drawing.Point(658, 44);
            this.picBoxSmall.Name = "picBoxSmall";
            this.picBoxSmall.Size = new System.Drawing.Size(273, 162);
            this.picBoxSmall.TabIndex = 4;
            this.picBoxSmall.TabStop = false;
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(12, 41);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(640, 480);
            this.picBox.TabIndex = 5;
            this.picBox.TabStop = false;
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(88, 527);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(70, 30);
            this.pauseButton.TabIndex = 6;
            this.pauseButton.Text = "PAUSE";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(164, 527);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(70, 30);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // frameCnt
            // 
            this.frameCnt.AutoSize = true;
            this.frameCnt.Location = new System.Drawing.Point(266, 536);
            this.frameCnt.Name = "frameCnt";
            this.frameCnt.Size = new System.Drawing.Size(64, 13);
            this.frameCnt.TabIndex = 7;
            this.frameCnt.Text = "Frames: 0/0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 614);
            this.Controls.Add(this.frameCnt);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.picBoxSmall);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Detect);
            this.Controls.Add(this.tBox_path);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Train Coup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btn_Detect;
        public System.Windows.Forms.TextBox tBox_path;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripDetectionSystem;
        public System.Windows.Forms.ToolStripStatusLabel toolStripTimer;
        public System.Windows.Forms.Button button1;
        public OpenCvSharp.UserInterface.PictureBoxIpl picBoxSmall;
        public CustomPicBox picBox;
        public System.Windows.Forms.Button pauseButton;
        public System.Windows.Forms.Button stopButton;
        public System.Windows.Forms.Label frameCnt;
    }
}


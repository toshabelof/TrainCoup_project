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
            this.button1 = new System.Windows.Forms.Button();
            this.picBoxSmall = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.frameCnt = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Frame = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOpenFindPhotos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tBox_Treatment = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStartTreatment = new System.Windows.Forms.Button();
            this.lblCountPhotos = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.picBox = new ModulWrapper.CustomPicBox(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.tBox_path.Text = "Укажите путь к видео-файлу";
            this.tBox_path.WordWrap = false;
            // 
            // btn_Detect
            // 
            this.btn_Detect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Detect.Location = new System.Drawing.Point(658, 491);
            this.btn_Detect.Name = "btn_Detect";
            this.btn_Detect.Size = new System.Drawing.Size(87, 30);
            this.btn_Detect.TabIndex = 0;
            this.btn_Detect.Text = "PLAY";
            this.btn_Detect.UseVisualStyleBackColor = true;
            this.btn_Detect.Click += new System.EventHandler(this.btn_Detect_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDetectionSystem,
            this.toolStripTimer,
            this.toolStripCounter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 614);
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
            // toolStripCounter
            // 
            this.toolStripCounter.Name = "toolStripCounter";
            this.toolStripCounter.Size = new System.Drawing.Size(95, 17);
            this.toolStripCounter.Text = "toolStripCounter";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(856, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_filePick_Click);
            // 
            // picBoxSmall
            // 
            this.picBoxSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxSmall.Location = new System.Drawing.Point(658, 41);
            this.picBoxSmall.Name = "picBoxSmall";
            this.picBoxSmall.Size = new System.Drawing.Size(273, 165);
            this.picBoxSmall.TabIndex = 4;
            this.picBoxSmall.TabStop = false;
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(751, 491);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(87, 30);
            this.pauseButton.TabIndex = 6;
            this.pauseButton.Text = "PAUSE";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(844, 491);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(87, 30);
            this.stopButton.TabIndex = 6;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // frameCnt
            // 
            this.frameCnt.AutoSize = true;
            this.frameCnt.Location = new System.Drawing.Point(658, 465);
            this.frameCnt.Name = "frameCnt";
            this.frameCnt.Size = new System.Drawing.Size(64, 13);
            this.frameCnt.TabIndex = 7;
            this.frameCnt.Text = "Frames: 0/0";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Frame,
            this.X1,
            this.Y1,
            this.X2,
            this.Y2});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(658, 212);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(273, 240);
            this.dataGridView1.TabIndex = 8;
            // 
            // Frame
            // 
            this.Frame.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Frame.HeaderText = "Frame";
            this.Frame.Name = "Frame";
            this.Frame.ReadOnly = true;
            // 
            // X1
            // 
            this.X1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X1.HeaderText = "X1";
            this.X1.Name = "X1";
            this.X1.ReadOnly = true;
            // 
            // Y1
            // 
            this.Y1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y1.HeaderText = "Y1";
            this.Y1.Name = "Y1";
            this.Y1.ReadOnly = true;
            // 
            // X2
            // 
            this.X2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X2.HeaderText = "X2";
            this.X2.Name = "X2";
            this.X2.ReadOnly = true;
            // 
            // Y2
            // 
            this.Y2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y2.HeaderText = "Y2";
            this.Y2.Name = "Y2";
            this.Y2.ReadOnly = true;
            // 
            // btnOpenFindPhotos
            // 
            this.btnOpenFindPhotos.Location = new System.Drawing.Point(733, 18);
            this.btnOpenFindPhotos.Name = "btnOpenFindPhotos";
            this.btnOpenFindPhotos.Size = new System.Drawing.Size(87, 30);
            this.btnOpenFindPhotos.TabIndex = 6;
            this.btnOpenFindPhotos.Text = "Открыть";
            this.btnOpenFindPhotos.UseVisualStyleBackColor = true;
            this.btnOpenFindPhotos.Click += new System.EventHandler(this.btnOpenFindPhotos_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Путь к фотографиям:";
            // 
            // tBox_Treatment
            // 
            this.tBox_Treatment.Location = new System.Drawing.Point(128, 24);
            this.tBox_Treatment.Name = "tBox_Treatment";
            this.tBox_Treatment.Size = new System.Drawing.Size(599, 20);
            this.tBox_Treatment.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.lblCountPhotos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tBox_Treatment);
            this.groupBox1.Controls.Add(this.btnStartTreatment);
            this.groupBox1.Controls.Add(this.btnOpenFindPhotos);
            this.groupBox1.Location = new System.Drawing.Point(12, 527);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(919, 84);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Массовая обработка фото";
            // 
            // btnStartTreatment
            // 
            this.btnStartTreatment.Location = new System.Drawing.Point(826, 18);
            this.btnStartTreatment.Name = "btnStartTreatment";
            this.btnStartTreatment.Size = new System.Drawing.Size(87, 30);
            this.btnStartTreatment.TabIndex = 6;
            this.btnStartTreatment.Text = "Обработать";
            this.btnStartTreatment.UseVisualStyleBackColor = true;
            this.btnStartTreatment.Click += new System.EventHandler(this.btnStartTreatment_Click);
            // 
            // lblCountPhotos
            // 
            this.lblCountPhotos.AutoSize = true;
            this.lblCountPhotos.Location = new System.Drawing.Point(6, 59);
            this.lblCountPhotos.Name = "lblCountPhotos";
            this.lblCountPhotos.Size = new System.Drawing.Size(123, 13);
            this.lblCountPhotos.TabIndex = 11;
            this.lblCountPhotos.Text = "Обработано: NaN/NaN";
            this.lblCountPhotos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(169, 54);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(744, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // picBox
            // 
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(12, 41);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(640, 480);
            this.picBox.TabIndex = 5;
            this.picBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 636);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        public System.Windows.Forms.ToolStripStatusLabel toolStripCounter;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frame;
        private System.Windows.Forms.DataGridViewTextBoxColumn X1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y1;
        private System.Windows.Forms.DataGridViewTextBoxColumn X2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y2;
        public System.Windows.Forms.Button btnOpenFindPhotos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBox_Treatment;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btnStartTreatment;
        public System.Windows.Forms.Label lblCountPhotos;
        public System.Windows.Forms.ProgressBar progressBar1;
    }
}


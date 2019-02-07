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
        private void InitializeComponent()
        {
            this.tBox_path = new System.Windows.Forms.TextBox();
            this.btn_Detect = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDetectionSystem = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTimer = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
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
            this.tBox_path.Size = new System.Drawing.Size(838, 20);
            this.tBox_path.TabIndex = 1;
            this.tBox_path.Text = "Enter the path to the photo and to press Enter key";
            this.tBox_path.WordWrap = false;
            this.tBox_path.Enter += new System.EventHandler(this.tBox_path_Enter);
            this.tBox_path.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBox_path_KeyDown);
            this.tBox_path.Leave += new System.EventHandler(this.tBox_path_Leave);
            // 
            // btn_Detect
            // 
            this.btn_Detect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Detect.Location = new System.Drawing.Point(856, 12);
            this.btn_Detect.Name = "btn_Detect";
            this.btn_Detect.Size = new System.Drawing.Size(75, 26);
            this.btn_Detect.TabIndex = 0;
            this.btn_Detect.Text = "Detect";
            this.btn_Detect.UseVisualStyleBackColor = true;
            this.btn_Detect.Click += new System.EventHandler(this.btn_Detect_Click);
            // 
            // picBox
            // 
            this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBox.Location = new System.Drawing.Point(12, 44);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(919, 529);
            this.picBox.TabIndex = 2;
            this.picBox.TabStop = false;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 614);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.btn_Detect);
            this.Controls.Add(this.tBox_path);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Detect;
        private System.Windows.Forms.TextBox tBox_path;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripDetectionSystem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripTimer;
    }
}


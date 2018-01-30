namespace FyColocation
{
    partial class Form_joinlessbegin
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
            this.label1 = new System.Windows.Forms.Label();
            this.button_datasource = new System.Windows.Forms.Button();
            this.textBox_r = new System.Windows.Forms.TextBox();
            this.textBox_prev = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_scan = new System.Windows.Forms.Button();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_begin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_CDS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Location = new System.Drawing.Point(223, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose a input data ：";
            // 
            // button_datasource
            // 
            this.button_datasource.BackgroundImage = global::FyColocation.Properties.Resources.qq9;
            this.button_datasource.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_datasource.FlatAppearance.BorderSize = 0;
            this.button_datasource.Location = new System.Drawing.Point(584, 242);
            this.button_datasource.Name = "button_datasource";
            this.button_datasource.Size = new System.Drawing.Size(323, 23);
            this.button_datasource.TabIndex = 2;
            this.button_datasource.Text = "scan input path";
            this.button_datasource.UseVisualStyleBackColor = true;
            this.button_datasource.Click += new System.EventHandler(this.button_datasource_Click);
            // 
            // textBox_r
            // 
            this.textBox_r.Location = new System.Drawing.Point(471, 390);
            this.textBox_r.Name = "textBox_r";
            this.textBox_r.Size = new System.Drawing.Size(234, 21);
            this.textBox_r.TabIndex = 35;
            this.textBox_r.Text = "10";
            this.textBox_r.TextChanged += new System.EventHandler(this.textBox_r_TextChanged);
            // 
            // textBox_prev
            // 
            this.textBox_prev.Location = new System.Drawing.Point(422, 430);
            this.textBox_prev.Name = "textBox_prev";
            this.textBox_prev.Size = new System.Drawing.Size(283, 21);
            this.textBox_prev.TabIndex = 33;
            this.textBox_prev.Text = "0.3";
            this.textBox_prev.TextChanged += new System.EventHandler(this.textBox_prev_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Location = new System.Drawing.Point(226, 387);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(238, 24);
            this.label7.TabIndex = 32;
            this.label7.Text = "distance threshold:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(226, 424);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "min_prevalence:";
            // 
            // button_scan
            // 
            this.button_scan.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_scan.BackgroundImage = global::FyColocation.Properties.Resources.qq9;
            this.button_scan.Location = new System.Drawing.Point(763, 308);
            this.button_scan.Name = "button_scan";
            this.button_scan.Size = new System.Drawing.Size(143, 23);
            this.button_scan.TabIndex = 39;
            this.button_scan.Text = "scan output path";
            this.button_scan.UseVisualStyleBackColor = false;
            this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
            // 
            // textBox_file
            // 
            this.textBox_file.Location = new System.Drawing.Point(226, 340);
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.Size = new System.Drawing.Size(680, 21);
            this.textBox_file.TabIndex = 38;
            this.textBox_file.TextChanged += new System.EventHandler(this.textBox_file_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(222, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(526, 24);
            this.label2.TabIndex = 37;
            this.label2.Text = "Please choose  a path to store output file:";
            // 
            // button_begin
            // 
            this.button_begin.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_begin.BackgroundImage = global::FyColocation.Properties.Resources.qq9;
            this.button_begin.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_begin.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_begin.Location = new System.Drawing.Point(422, 525);
            this.button_begin.Name = "button_begin";
            this.button_begin.Size = new System.Drawing.Size(346, 40);
            this.button_begin.TabIndex = 40;
            this.button_begin.Text = "Running ";
            this.button_begin.UseVisualStyleBackColor = false;
            this.button_begin.Click += new System.EventHandler(this.button_begin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(225, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "INPUT DATA IS NOT LORDING......";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(226, 468);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(346, 24);
            this.label4.TabIndex = 44;
            this.label4.Text = "Feature Disparity Threshold:";
            // 
            // textBox_CDS
            // 
            this.textBox_CDS.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_CDS.Location = new System.Drawing.Point(584, 471);
            this.textBox_CDS.Name = "textBox_CDS";
            this.textBox_CDS.Size = new System.Drawing.Size(121, 21);
            this.textBox_CDS.TabIndex = 45;
            this.textBox_CDS.Text = "0.2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Consolas", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(61, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1054, 51);
            this.label6.TabIndex = 49;
            this.label6.Text = "Dominant-Feature Co-location Pattern Mining";
            // 
            // Form_joinlessbegin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BackgroundImage = global::FyColocation.Properties.Resources.co_locationcity;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1163, 702);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_CDS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_begin);
            this.Controls.Add(this.button_scan);
            this.Controls.Add(this.textBox_file);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_r);
            this.Controls.Add(this.textBox_prev);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_datasource);
            this.Controls.Add(this.label1);
            this.Name = "Form_joinlessbegin";
            this.Text = "Form_Dominant-Feature Co-location Mining System";
            this.Load += new System.EventHandler(this.Form_joinlessbegin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_datasource;
        private System.Windows.Forms.TextBox textBox_r;
        private System.Windows.Forms.TextBox textBox_prev;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_scan;
        private System.Windows.Forms.TextBox textBox_file;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_begin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_CDS;
        private System.Windows.Forms.Label label6;
    }
}
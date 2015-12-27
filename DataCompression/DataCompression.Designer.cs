namespace DataCompression
{
    partial class DataCompression
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
            this.inputTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openInputFile = new System.Windows.Forms.OpenFileDialog();
            this.selectBtn = new System.Windows.Forms.Button();
            this.labelFS = new System.Windows.Forms.Label();
            this.labelFSbytes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputTB
            // 
            this.inputTB.Location = new System.Drawing.Point(59, 29);
            this.inputTB.Name = "inputTB";
            this.inputTB.ReadOnly = true;
            this.inputTB.Size = new System.Drawing.Size(368, 20);
            this.inputTB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input file";
            // 
            // selectBtn
            // 
            this.selectBtn.Location = new System.Drawing.Point(352, 55);
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.Size = new System.Drawing.Size(75, 23);
            this.selectBtn.TabIndex = 2;
            this.selectBtn.Text = "Browse...";
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // labelFS
            // 
            this.labelFS.AutoSize = true;
            this.labelFS.Location = new System.Drawing.Point(56, 65);
            this.labelFS.Name = "labelFS";
            this.labelFS.Size = new System.Drawing.Size(89, 13);
            this.labelFS.TabIndex = 3;
            this.labelFS.Text = "File size (in bytes)";
            // 
            // labelFSbytes
            // 
            this.labelFSbytes.AutoSize = true;
            this.labelFSbytes.Location = new System.Drawing.Point(187, 65);
            this.labelFSbytes.Name = "labelFSbytes";
            this.labelFSbytes.Size = new System.Drawing.Size(13, 13);
            this.labelFSbytes.TabIndex = 4;
            this.labelFSbytes.Text = "0";
            // 
            // DataCompression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 196);
            this.Controls.Add(this.labelFSbytes);
            this.Controls.Add(this.labelFS);
            this.Controls.Add(this.selectBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataCompression";
            this.Text = "Data Compression Utility";
            this.Load += new System.EventHandler(this.DataCompression_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openInputFile;
        private System.Windows.Forms.Button selectBtn;
        private System.Windows.Forms.Label labelFS;
        private System.Windows.Forms.Label labelFSbytes;
    }
}


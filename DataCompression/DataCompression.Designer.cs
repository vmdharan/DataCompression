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
            this.rbEncode = new System.Windows.Forms.RadioButton();
            this.rbDecode = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.cbAlgorithm = new System.Windows.Forms.ComboBox();
            this.labelAlgorithm = new System.Windows.Forms.Label();
            this.gbFunction.SuspendLayout();
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
            // rbEncode
            // 
            this.rbEncode.AutoSize = true;
            this.rbEncode.Location = new System.Drawing.Point(6, 26);
            this.rbEncode.Name = "rbEncode";
            this.rbEncode.Size = new System.Drawing.Size(62, 17);
            this.rbEncode.TabIndex = 5;
            this.rbEncode.TabStop = true;
            this.rbEncode.Text = "Encode";
            this.rbEncode.UseVisualStyleBackColor = true;
            // 
            // rbDecode
            // 
            this.rbDecode.AutoSize = true;
            this.rbDecode.Location = new System.Drawing.Point(74, 26);
            this.rbDecode.Name = "rbDecode";
            this.rbDecode.Size = new System.Drawing.Size(63, 17);
            this.rbDecode.TabIndex = 6;
            this.rbDecode.TabStop = true;
            this.rbDecode.Text = "Decode";
            this.rbDecode.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.button1);
            this.gbFunction.Controls.Add(this.rbEncode);
            this.gbFunction.Controls.Add(this.rbDecode);
            this.gbFunction.Location = new System.Drawing.Point(276, 105);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(149, 79);
            this.gbFunction.TabIndex = 8;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Function";
            // 
            // cbAlgorithm
            // 
            this.cbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlgorithm.FormattingEnabled = true;
            this.cbAlgorithm.Items.AddRange(new object[] {
            "Huffman"});
            this.cbAlgorithm.Location = new System.Drawing.Point(15, 131);
            this.cbAlgorithm.Name = "cbAlgorithm";
            this.cbAlgorithm.Size = new System.Drawing.Size(144, 21);
            this.cbAlgorithm.TabIndex = 9;
            // 
            // labelAlgorithm
            // 
            this.labelAlgorithm.AutoSize = true;
            this.labelAlgorithm.Location = new System.Drawing.Point(15, 112);
            this.labelAlgorithm.Name = "labelAlgorithm";
            this.labelAlgorithm.Size = new System.Drawing.Size(50, 13);
            this.labelAlgorithm.TabIndex = 10;
            this.labelAlgorithm.Text = "Algorithm";
            // 
            // DataCompression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 196);
            this.Controls.Add(this.labelAlgorithm);
            this.Controls.Add(this.cbAlgorithm);
            this.Controls.Add(this.gbFunction);
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
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
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
        private System.Windows.Forms.RadioButton rbEncode;
        private System.Windows.Forms.RadioButton rbDecode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.ComboBox cbAlgorithm;
        private System.Windows.Forms.Label labelAlgorithm;
    }
}


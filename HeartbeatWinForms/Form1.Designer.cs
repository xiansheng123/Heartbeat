namespace HeartbeatWinForms
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.ReportGenerate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Uploading = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {"full report", "partial report", "-ed report"});
            this.checkedListBox1.Location = new System.Drawing.Point(815, 364);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(218, 64);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // ReportGenerate
            // 
            this.ReportGenerate.Location = new System.Drawing.Point(908, 471);
            this.ReportGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ReportGenerate.Name = "ReportGenerate";
            this.ReportGenerate.Size = new System.Drawing.Size(125, 28);
            this.ReportGenerate.TabIndex = 1;
            this.ReportGenerate.Text = "Give Me Report";
            this.ReportGenerate.UseVisualStyleBackColor = true;
            this.ReportGenerate.Click += new System.EventHandler(this.ReportGenerate_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Uploading
            // 
            this.Uploading.Location = new System.Drawing.Point(29, 471);
            this.Uploading.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Uploading.Name = "Uploading";
            this.Uploading.Size = new System.Drawing.Size(111, 28);
            this.Uploading.TabIndex = 3;
            this.Uploading.Text = "Uploading";
            this.Uploading.UseVisualStyleBackColor = true;
            this.Uploading.Click += new System.EventHandler(this.Uploading_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(29, 10);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1004, 350);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Uploading);
            this.Controls.Add(this.ReportGenerate);
            this.Controls.Add(this.checkedListBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Heartbeat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ReportGenerate;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button Uploading;

        #endregion
    }
}
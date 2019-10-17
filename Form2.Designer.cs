namespace WinReadBook
{
    partial class Form2
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
            this.btnMulu = new System.Windows.Forms.Button();
            this.lblname = new System.Windows.Forms.Label();
            this.txtNameURL = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMulu
            // 
            this.btnMulu.Location = new System.Drawing.Point(698, 12);
            this.btnMulu.Name = "btnMulu";
            this.btnMulu.Size = new System.Drawing.Size(90, 21);
            this.btnMulu.TabIndex = 0;
            this.btnMulu.Text = "提取目录";
            this.btnMulu.UseVisualStyleBackColor = true;
            this.btnMulu.Click += new System.EventHandler(this.BtnMulu_Click);
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Location = new System.Drawing.Point(3, 15);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(77, 12);
            this.lblname.TabIndex = 6;
            this.lblname.Text = "小说目录路径";
            // 
            // txtNameURL
            // 
            this.txtNameURL.Location = new System.Drawing.Point(86, 12);
            this.txtNameURL.Name = "txtNameURL";
            this.txtNameURL.Size = new System.Drawing.Size(603, 21);
            this.txtNameURL.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(5, 63);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(783, 496);
            this.listBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "*不支持有分页的目录";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 567);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblname);
            this.Controls.Add(this.txtNameURL);
            this.Controls.Add(this.btnMulu);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMulu;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.TextBox txtNameURL;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
    }
}
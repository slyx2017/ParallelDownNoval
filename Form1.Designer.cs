namespace WinReadBook
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_StartRead = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameURL = new System.Windows.Forms.TextBox();
            this.lblname = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDownDirectry = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.btnBrowserDialog = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_StartRead
            // 
            this.btn_StartRead.Location = new System.Drawing.Point(452, 124);
            this.btn_StartRead.Name = "btn_StartRead";
            this.btn_StartRead.Size = new System.Drawing.Size(75, 23);
            this.btn_StartRead.TabIndex = 0;
            this.btn_StartRead.Text = "下载章节";
            this.btn_StartRead.UseVisualStyleBackColor = true;
            this.btn_StartRead.Click += new System.EventHandler(this.Btn_StartRead_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 124);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(368, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(380, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // txtNameURL
            // 
            this.txtNameURL.Location = new System.Drawing.Point(89, 27);
            this.txtNameURL.Name = "txtNameURL";
            this.txtNameURL.Size = new System.Drawing.Size(438, 21);
            this.txtNameURL.TabIndex = 3;
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Location = new System.Drawing.Point(8, 30);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(77, 12);
            this.lblname.TabIndex = 4;
            this.lblname.Text = "小说目录网址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(431, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "（例如：https://www.ddxsku.com/files/article/html/36/36102/index.html）";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(89, 85);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(114, 21);
            this.numericUpDown1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "开启线程数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(209, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "章节所在目录";
            // 
            // txtDownDirectry
            // 
            this.txtDownDirectry.Location = new System.Drawing.Point(292, 85);
            this.txtDownDirectry.Name = "txtDownDirectry";
            this.txtDownDirectry.ReadOnly = true;
            this.txtDownDirectry.Size = new System.Drawing.Size(235, 21);
            this.txtDownDirectry.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.txtLocalPath);
            this.groupBox1.Controls.Add(this.btnBrowserDialog);
            this.groupBox1.Location = new System.Drawing.Point(12, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 240);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "合并章节操作";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(10, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(517, 184);
            this.listBox1.TabIndex = 5;
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Location = new System.Drawing.Point(10, 16);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(436, 21);
            this.txtLocalPath.TabIndex = 4;
            // 
            // btnBrowserDialog
            // 
            this.btnBrowserDialog.Location = new System.Drawing.Point(452, 14);
            this.btnBrowserDialog.Name = "btnBrowserDialog";
            this.btnBrowserDialog.Size = new System.Drawing.Size(75, 23);
            this.btnBrowserDialog.TabIndex = 3;
            this.btnBrowserDialog.Text = "选择章节";
            this.btnBrowserDialog.UseVisualStyleBackColor = true;
            this.btnBrowserDialog.Click += new System.EventHandler(this.BtnBrowserDialog_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDownDirectry);
            this.groupBox2.Controls.Add(this.btn_StartRead);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNameURL);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.lblname);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(533, 155);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "提取网站章节";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 419);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_StartRead;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameURL;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDownDirectry;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.Button btnBrowserDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}


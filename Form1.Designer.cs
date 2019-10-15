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
            this.txtXsName = new System.Windows.Forms.TextBox();
            this.lblxsname = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_StartRead
            // 
            this.btn_StartRead.Location = new System.Drawing.Point(588, 197);
            this.btn_StartRead.Name = "btn_StartRead";
            this.btn_StartRead.Size = new System.Drawing.Size(129, 45);
            this.btn_StartRead.TabIndex = 0;
            this.btn_StartRead.Text = "开始读取";
            this.btn_StartRead.UseVisualStyleBackColor = true;
            this.btn_StartRead.Click += new System.EventHandler(this.Btn_StartRead_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(2, 257);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(715, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // txtNameURL
            // 
            this.txtNameURL.Location = new System.Drawing.Point(114, 87);
            this.txtNameURL.Name = "txtNameURL";
            this.txtNameURL.Size = new System.Drawing.Size(603, 21);
            this.txtNameURL.TabIndex = 3;
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Location = new System.Drawing.Point(31, 90);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(77, 12);
            this.lblname.TabIndex = 4;
            this.lblname.Text = "小说目录路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(431, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "（例如：https://www.ddxsku.com/files/article/html/36/36102/index.html）";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(114, 159);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "开启线程数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "下载所在目录";
            // 
            // txtDownDirectry
            // 
            this.txtDownDirectry.Location = new System.Drawing.Point(325, 161);
            this.txtDownDirectry.Name = "txtDownDirectry";
            this.txtDownDirectry.ReadOnly = true;
            this.txtDownDirectry.Size = new System.Drawing.Size(392, 21);
            this.txtDownDirectry.TabIndex = 9;
            // 
            // txtXsName
            // 
            this.txtXsName.Location = new System.Drawing.Point(114, 38);
            this.txtXsName.Name = "txtXsName";
            this.txtXsName.Size = new System.Drawing.Size(603, 21);
            this.txtXsName.TabIndex = 10;
            // 
            // lblxsname
            // 
            this.lblxsname.AutoSize = true;
            this.lblxsname.Location = new System.Drawing.Point(67, 41);
            this.lblxsname.Name = "lblxsname";
            this.lblxsname.Size = new System.Drawing.Size(41, 12);
            this.lblxsname.TabIndex = 11;
            this.lblxsname.Text = "小说名";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 284);
            this.Controls.Add(this.lblxsname);
            this.Controls.Add(this.txtXsName);
            this.Controls.Add(this.txtDownDirectry);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblname);
            this.Controls.Add(this.txtNameURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_StartRead);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox txtXsName;
        private System.Windows.Forms.Label lblxsname;
    }
}


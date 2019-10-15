﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinReadBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        MatchCollection sMC1;
        public delegate void AsynUpdateUI(int step);
        public delegate void UpdateUI(int step);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;
        int processNum = 2;
        // txt文本输出
        string path = "C://Noval//DownFile//"+DateTime.Now.ToString("yyyy-MM-dd")+"//";//AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "DownFile/";
        private void Btn_StartRead_Click(object sender, EventArgs e)
        {
            string novalurl = txtNameURL.Text;
            string xsname = txtXsName.Text;
            processNum = Convert.ToInt32(numericUpDown1.Value);
            if (processNum==0)
            {
                processNum = 2;
            }
            if (string.IsNullOrWhiteSpace(xsname))
            {
                Accomplish("小说名不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(novalurl))
            {
                Accomplish("小说目录路径不能为空");
                return;
            }
            path += xsname+"//";
            //novalurl = "https://www.ddxsku.com/files/article/html/36/36102/index.html";
            //抓取整本小说
            string html = HttpHelpr.HttpGet(novalurl, "","0");
            // 获取小说名字
            Match ma_name = Regex.Match(html, @"<meta name=""keywords"".+content=""(.+)""./>");
            string name = ma_name.Groups[1].Value.ToString().Split(',')[0];
            // 获取章节目录
            Regex reg_mulu = new Regex(@"<table cellspacing=""1"" cellpadding=""0"" bgcolor=""#E4E4E4"" id=""at"">(.|\n)*?</table>");
            var mat_mulu = reg_mulu.Match(html);
            string mulu = mat_mulu.Groups[0].ToString();

            // 匹配a标签里面的url
            Regex hreflist = new Regex("<a[^>]+?href=\"([^\"]+)\"[^>]*>([^<]+)</a>", RegexOptions.Compiled);
            MatchCollection sMC = hreflist.Matches(mulu);

            if (sMC.Count != 0)
            {
                sMC1 = sMC;
                int taskCount = sMC.Count; //任务量为10000
                this.progressBar1.Maximum = taskCount;
                this.progressBar1.Value = 0;
                new Thread(ParallelFunction) { IsBackground = true }.Start();
                txtDownDirectry.Text = path;
            }

        }
        public void ParallelFunction()
        {
            //多线程方式二：
            Parallel.For(0, sMC1.Count,
                     new ParallelOptions() { MaxDegreeOfParallelism = processNum },
                     (i, loopState) =>
                     {

                         Thread.Sleep(1);
                             //获取文章标题
                             string title = sMC1[i].Groups[2].Value.Replace("正文", "").Replace("_", "").Replace(" ", "").Replace("?", "").Replace("*", "").Replace(":","");
                             //获取文章内容
                             string html_z = HttpHelpr.HttpGet(sMC1[i].Groups[1].Value, "", "1");
                             // 获取正文
                             Regex reg = new Regex(@"<dd id=""contents"">(.|\n)*?</dd>");
                         var mat = reg.Match(html_z);
                         string content = mat.Groups[0].ToString().Replace("<dd id=\"contents\">", "").Replace("</dd>", "").Replace("&nbsp;", "").Replace("<br />", "\r\n");
                         DownFiles.Novel(title + "\r\n" + content, title.Replace("两","二"), path);
                         UpdataUIStatus(1);
                     });
            Accomplish("任务完成");
        }
        //更新UI
        private void UpdataUIStatus(int step)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (int s)
                {
                    this.progressBar1.Value += s;
                    this.label1.Text = this.progressBar1.Value.ToString() + "/" + this.progressBar1.Maximum.ToString();
                }), step);
            }
            else
            {
                this.progressBar1.Value += step;
                this.label1.Text = this.progressBar1.Value.ToString() + "/" + this.progressBar1.Maximum.ToString();
            }
        }
        //完成任务时需要调用
        private void Accomplish(string msg)
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            MessageBox.Show(msg);
        }

    }
}

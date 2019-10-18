using System;
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
        Dictionary<string, string> list=null;
        public delegate void AsynUpdateUI(int step);
        public delegate void UpdateUI(int step);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;
        int processNum = 2;
        string charset = "utf-8";
        string path1 = "";
        // txt文本输出
        string path = "C://Noval//DownFile//"+DateTime.Now.ToString("yyyy-MM-dd")+"//";//AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "DownFile/";
        private void Btn_StartRead_Click(object sender, EventArgs e)
        {
            //string str = "第二四零 这里我有些熟悉";
            //string msg=DownFiles.Chinese2Num(str);
            //MessageBox.Show(msg);
            //return;
            this.progressBar1.Maximum = 0;
            this.progressBar1.Value = 0;
            txtDownDirectry.Text = "";
            string novalurl = txtNameURL.Text;
            string xsname = "我的小说"+DateTime.Now.ToString("yyyyMMddHHmmss");
            processNum = Convert.ToInt32(numericUpDown1.Value);
            if (processNum==0)
            {
                processNum = 2;
            }
            if (string.IsNullOrWhiteSpace(novalurl))
            {
                Accomplish("小说目录路径不能为空");
                return;
            }
            
            //novalurl = "https://www.ddxsku.com/files/article/html/36/36102/index.html";
            //抓取整本小说
            string html = HttpHelpr.HttpGet(novalurl, "", "1", "utf-8");
            charset = DownFiles.GetEncode(html);
            if (charset != "utf-8")
            {
                html = HttpHelpr.HttpGet(novalurl, "", "1", charset);
            }
            string title= DownFiles.GetNovelName(html);
            xsname = string.IsNullOrEmpty(title) ? xsname : title;
            string muluroot = DownFiles.GetMuluRoot(html);
            MatchCollection sMC = DownFiles.GetMulu(muluroot);
            list = DownFiles.RemoteUrlList(sMC);
            path1 = path + xsname + "//";
            if (list.Count != 0)
            {
                int taskCount = list.Count;
                this.progressBar1.Maximum = taskCount;
                this.progressBar1.Value = 0;
                new Thread(ParallelFunction) { IsBackground = true }.Start();
                txtDownDirectry.Text = path1;
            }
            else
            {
                Accomplish("没有提取到章节地址");
            }

        }
        public void ParallelFunction()
        {
            //多线程方式一：
            //Parallel.ForEach(list,(item,loopState)=>
            //{
            //    Thread.Sleep(1);
            //    //获取文章标题

            //    string title = item.Key.Replace("正文", "").Replace("_", "").Replace(" ", "").Replace("?", "").Replace("*", "").Replace(":", "");
            //    //获取文章内容
            //    string html_z = HttpHelpr.HttpGet(item.Value, "", "1",charset);
            //    // 获取正文
            //    Regex reg = new Regex(@"<dd id=""contents"">(.|\n)*?</dd>");
            //    var mat = reg.Match(html_z);
            //    if (string.IsNullOrEmpty(mat.Groups[0].ToString()))
            //    {
            //        reg = new Regex(@"<div id=""content(s?)"">(.|\n)*?</div>");
            //        mat= reg.Match(html_z);
            //    }
            //    string content = mat.Groups[0].ToString().Replace("<div id=\"content\">","").Replace("</div>", "").Replace("<dd id=\"contents\">", "").Replace("</dd>", "").Replace("&nbsp;", "").Replace("<br />", "\r\n");
            //    DownFiles.Novel(DownFiles.Chinese2Num(title) + "\r\n" + content, DownFiles.Chinese2Num(title), path);
            //    UpdataUIStatus(1);
            //});

            //多线程方式二：
            Parallel.For(0, list.Keys.Count,
                     new ParallelOptions() { MaxDegreeOfParallelism = processNum },
                     (i, loopState) =>
                     {
                         Thread.Sleep(1);
                         
                         //获取文章标题
                         string title = list.Keys.ElementAt(i).Replace("正文", "").Replace("_", "").Replace("?", "").Replace("*", "").Replace(":", "");
                         //获取文章内容
                         string html_z = HttpHelpr.HttpGet(list[list.Keys.ElementAt(i)], "", "1", charset);
                         // 获取正文
                         string content=DownFiles.FindContent(html_z);
                         // 获取章节序号
                         string chartname = DownFiles.Chinese2Num(title);
                         DownFiles.Novel(chartname + "\r\n" + content, chartname, path1);
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

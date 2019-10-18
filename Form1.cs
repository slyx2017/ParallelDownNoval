using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        string batfile = "C://Noval//combinebat.bat";
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
            MessageBox.Show(msg,"提示");
        }

        List<string> list3 = new List<string>();
        List<string> list4 = new List<string>();
        List<string> list5 = new List<string>();
        List<string> list6 = new List<string>();
        private void BtnBrowserDialog_Click(object sender, EventArgs e)
        {
            txtLocalPath.Text = "";
            listBox1.Items.Clear();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocalPath.Text = folderBrowserDialog1.SelectedPath;
            }
            if (string.IsNullOrEmpty(txtLocalPath.Text))
            {
                return;
            }
            // 判断文件是否存在，不存在则创建
            if (!System.IO.File.Exists(batfile))
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory+ "combinebat.bat", batfile, true);
            }

            string[] txtFiles = Directory.GetFiles(txtLocalPath.Text, "*.txt");
            for (int i = 0; i < txtFiles.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(txtFiles[i]);
                string filename = fileInfo.Name.Split(' ')[0];
                if (filename.Length == 3)
                {
                    list3.Add(txtFiles[i]);
                }
                if (filename.Length == 4)
                {
                    list4.Add(txtFiles[i]);
                }
                if (filename.Length == 5)
                {
                    list5.Add(txtFiles[i]);
                }
                if (filename.Length == 6)
                {
                    list6.Add(txtFiles[i]);
                }
            }
            for (int j = 0; j < list3.Count; j++)
            {
                listBox1.Items.Add(list3[j]);
            }

            for (int j = 0; j < list4.Count; j++)
            {
                listBox1.Items.Add(list4[j]);
            }

            for (int j = 0; j < list5.Count; j++)
            {
                listBox1.Items.Add(list5[j]);
            }

            for (int j = 0; j < list6.Count; j++)
            {
                listBox1.Items.Add(list6[j]);
            }

            Thread thread1 = new Thread(CombineFile1);
            thread1.IsBackground = true;
            thread1.Start();
            Thread thread2 = new Thread(CombineFile2);
            thread2.IsBackground = true;
            thread2.Start();
            Thread thread3 = new Thread(CombineFile3);
            thread3.IsBackground = true;
            thread3.Start();
            Thread thread4 = new Thread(CombineFile4);
            thread4.IsBackground = true;
            thread4.Start();
            while (true)
            {
                // 判断文件是否存在，不存在则创建
                if (!System.IO.File.Exists("C://Noval//mytext.txt"))
                {
                    if (Directory.GetFiles("C://Noval//","*.txt").Length>0)
                    {
                        Callbackbat();
                    }
                }
                else
                {
                    break;
                }
            }
            MessageBox.Show("合并文件所在路径C:\\Noval\\");
        }
        public void CombineFile1()
        {
            string outfileName = "C:\\Noval\\mytext1.txt";
            CombineFile(list3, outfileName);
        }
        public void CombineFile2()
        {
            string outfileName = "C:\\Noval\\mytext2.txt";
            CombineFile(list4, outfileName);
        }
        public void CombineFile3()
        {
            string outfileName = "C:\\Noval\\mytext3.txt";
            CombineFile(list5, outfileName);
        }
        public void CombineFile4()
        {
            string outfileName = "C:\\Noval\\mytext4.txt";
            CombineFile(list6, outfileName);
        }
        public void CombineFile(List<string> infileName, string outfileName)
        {
            int b;
            int n = infileName.Count;
            FileStream[] fileIn = new FileStream[n];
            using (FileStream fileOut = new FileStream(outfileName, FileMode.Create))
            {
                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        fileIn[i] = new FileStream(infileName[i], FileMode.Open);
                        while ((b = fileIn[i].ReadByte()) != -1)
                            fileOut.WriteByte((byte)b);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        fileIn[i].Close();
                    }

                }
            }
        }

        public void Callbackbat()
        {
            Process proc = null;
            try
            {
                string targetDir = string.Format(@"C:\Noval\");//这是bat存放的目录
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = "combinebat.bat";//bat文件名称
                proc.StartInfo.Arguments = string.Format("10");//this is argument
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }
    }
}

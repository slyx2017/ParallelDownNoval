using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinReadBook
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        private void BtnMulu_Click(object sender, EventArgs e)
        {
            //string cn = "第二八八章 危险的地方";
            //string num = Chinese2Num(cn);
            //listBox1.Items.Add(num);
            //return;
            string novalurl = txtNameURL.Text;

            //novalurl = "https://www.qb5.tw/shu/100487.html";
            if (string.IsNullOrWhiteSpace(novalurl))
            {
                AlertMessage("小说目录路径不能为空");
                return;
            }

            //抓取目录页面dom
            string html = HttpHelpr.HttpGet(novalurl, "", "1", "utf-8");

            string charset= GetEncode(html);
            if (charset!="utf-8")
            {
                html = HttpHelpr.HttpGet(novalurl, "", "1", charset);
            }
                  
            string muluroot=GetMuluRoot(html);
            MatchCollection sMC = GetMulu(muluroot);
            Dictionary<string, string> list= RemoteUrlList(sMC);
            foreach (var item in list)
            {
                listBox1.Items.Add(item.Value + DownFiles.Chinese2Num(item.Key));
            }
            AlertMessage("读取完成" + list.Count.ToString());
        }


        public void AlertMessage(string msg)
        {
            MessageBox.Show(msg);
        }
        /// <summary>
        /// 提取编码
        /// </summary>
        /// <param name="html">html页面</param>
        /// <returns></returns>
        public string GetEncode(string html)
        {
            string valule;//
            html = Regex.Match(html, "<meta[^>]*?charset=([\"\']?)([a-zA-z0-9\\-]+)(\\1)[^>]*?>").Value.ToString();
            // 获取编码
            Match ma_chartset = Regex.Match(html, @"<meta http-equiv=""[C|c]ontent-[T|t]ype"".+content=""(.+)"".*/>");
            if (string.IsNullOrWhiteSpace(ma_chartset.Value))
            {
                ma_chartset = Regex.Match(html, @"<meta charset=""(.+)"">");
                if (string.IsNullOrWhiteSpace(ma_chartset.Value))
                {
                    ma_chartset = Regex.Match(html, @"<meta content=""(.+)"".+http-equiv=""[C|c]ontent-[T|t]ype"".*/>");
                }
            }
            string chartset = ma_chartset.Groups[1].Value.ToString();
            if (chartset.Contains(";"))
            {
                chartset=chartset.Split(';')[1];
                valule = chartset.Replace("charset", "").Replace("=", "").Replace(" ", "");
            }
            else
            {
                valule = chartset;
            }
            valule =valule.ToLower();
            if (valule == "gbk")
            {
                valule = "gb2312";
            }
            if (valule=="utf8")
            {
                valule = "utf-8";
            }
            return valule;
        }
        /// <summary>
        /// 提取目录集合
        /// </summary>
        /// <param name="muluRoot">目录根节点</param>
        /// <returns></returns>
        public MatchCollection GetMulu(string muluRoot)
        {
            muluRoot = muluRoot.Replace("'", "\"");
            // 匹配a标签里面的url 
            Regex hreflist = new Regex("<a[^>]+?href.?=\"([^\"]+)\"[^>]*>([^<]+)</a>", RegexOptions.Compiled);
            MatchCollection sMC = hreflist.Matches(muluRoot);
            return sMC;
        }
        /// <summary>
        /// 提取根节点
        /// </summary>
        /// <param name="html">html</param>
        /// <returns></returns>
        public string GetMuluRoot(string html)
        {
            // 获取章节目录 匹配规则一：
            Regex reg_muluroot = new Regex(@"<div class="".*"">(.|\n)*?</div>");
            MatchCollection rootMC = reg_muluroot.Matches(html);
            string muluroot=GetMuluRootFor(rootMC);
            if (string.IsNullOrWhiteSpace(muluroot))
            {
                // 获取章节目录 匹配规则二：
                reg_muluroot = new Regex(@"<table.*>(.|\n)*?</table>");
                rootMC = reg_muluroot.Matches(html);
                muluroot = GetMuluRootFor(rootMC);
            }
            return muluroot;
        }
        /// <summary>
        /// 循环其他匹配规则
        /// </summary>
        /// <param name="rootMC"></param>
        /// <returns></returns>
        public string GetMuluRootFor(MatchCollection rootMC)
        {
            string muluroot="";
            for (int i = 0; i < rootMC.Count; i++)
            {
                string hreflist = rootMC[i].Value.ToString();
                if (Regex.Matches(hreflist, @"\.htm").Count > 100 || Regex.Matches(hreflist, @"\.aspx").Count > 100)
                {
                    muluroot = hreflist;
                    break;
                }
            }

            return muluroot;
        }
        /// <summary>
        /// 章节目录地址集合
        /// </summary>
        /// <param name="MatchCollection">list</param>
        /// <returns></returns>
        public Dictionary<string, string> RemoteUrlList(MatchCollection list)
        {
            Dictionary<string,string> dic = new Dictionary<string, string>();
            for (int i = 0; i < list.Count; i++)
            {
                string href;
                if (list[i].Groups[1].Value.Contains("http"))
                {
                    href = list[i].Groups[1].Value;
                }
                else
                {
                    href = HttpHelpr.Host + list[i].Groups[1].Value;
                }
                string name = list[i].Groups[2].Value;
                if (!dic.Keys.Contains(name))
                {
                    dic.Add(name, href);
                }
            }

            return dic;
        }
        ///// <summary>
        ///// 中文数字转纯数字
        ///// </summary>
        ///// <param name="word">word</param>
        ///// <returns></returns>
        //public string Chinese2Num(string word)
        //{
        //    string titlename;
        //    string targetword;
        //    if (word.Contains(" "))
        //    {
        //        string[] charItems = word.Split(' ');
        //        word = charItems[0].ToString();
        //        titlename = charItems[1].ToString();
        //    }
        //    else
        //    {
        //        if (word.Contains("章"))
        //        {
        //            word = word.Insert(word.IndexOf("章") + 1, " ");
        //            string[] charItems = word.Split(' ');
        //            word = charItems[0].ToString();
        //            titlename = charItems[1].ToString();
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    string cnword=FileterChineseNum(word);
        //    targetword =DownFiles.Convert2Number(cnword).ToString();
        //    return "第"+targetword+"章 "+titlename;
        //}
        ///// <summary>
        ///// 获取章节中的中文数字
        ///// </summary>
        ///// <param name="word">word</param>
        ///// <returns></returns>
        //public string FileterChineseNum(string word)
        //{
        //    string cnword;
        //    Match ma_word = Regex.Match(word, @"[第|\s](.*)[章|\s]");
        //    if (!string.IsNullOrEmpty(ma_word.Groups[1].Value))
        //    {
        //        cnword = ma_word.Groups[1].Value.ToString().Replace("第", "").Replace("章", "");
        //    }
        //    else
        //    {
        //        if (!word.Contains("第") && !word.Contains("章"))
        //        {
        //            cnword = "0";
        //        }
        //        else
        //        {
        //            cnword = word.Replace("第", "").Replace("章", "");
        //        }
        //    }
        //    return cnword;
        //}
    }
}

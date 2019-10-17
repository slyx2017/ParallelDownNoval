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

            string charset= DownFiles.GetEncode(html);
            if (charset!="utf-8")
            {
                html = HttpHelpr.HttpGet(novalurl, "", "1", charset);
            }
                  
            string muluroot= DownFiles.GetMuluRoot(html);
            MatchCollection sMC = DownFiles.GetMulu(muluroot);
            Dictionary<string, string> list= DownFiles.RemoteUrlList(sMC);
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
    }
}

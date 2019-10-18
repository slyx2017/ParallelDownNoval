using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WinReadBook
{
    public class DownFiles
    {
        // <summary>
        /// 创建文本
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="name">名字</param>
        /// <param name="path">路径</param>
        public static void Novel(string content, string name, string path)
        {
            string Log = content + "\r\n";
            // 创建文件夹，如果不存在就创建file文件夹
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                // 判断文件是否存在，不存在则创建
                if (!System.IO.File.Exists(path + name + ".txt"))
                {
                    FileStream fs1 = new FileStream(path + name + ".txt", FileMode.Create, FileAccess.Write);// 创建写入文件 
                    StreamWriter sw = new StreamWriter(fs1);
                    sw.WriteLine(Log);// 开始写入值
                    sw.Close();
                    fs1.Close();
                }
                else
                {
                    FileStream fs = new FileStream(path + name + ".txt" + "", FileMode.Append, FileAccess.Write);
                    StreamWriter sr = new StreamWriter(fs);
                    sr.WriteLine(Log);// 开始写入值
                    sr.Close();
                    fs.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取小说名称
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetNovelName(string html)
        {
            string name="";
            // 获取小说名字
            Match ma_name = Regex.Match(html, @"<meta[^>]*?name=""[K|k]eywords"".+content=""(.*?)""*?>");
            string maname = ma_name.Groups[1].Value;
            if (string.IsNullOrEmpty(maname))
            {
                ma_name = Regex.Match(html, @"<meta[^>]*?content=""(.*?)"".+name=""[K|k]eywords"".*?>");
                maname = ma_name.Groups[1].Value;
            }
            if (maname.Contains(","))
            {
                name = maname.Split(',')[0];
            }
            if (maname.Contains("，"))
            {
                name = maname.Split('，')[0];
            }
            return name.Replace("最新章节","");
        }
        /// <summary>
        /// 提取编码
        /// </summary>
        /// <param name="html">html页面</param>
        /// <returns></returns>
        public static string GetEncode(string html)
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
                chartset = chartset.Split(';')[1];
                valule = chartset.Replace("charset", "").Replace("=", "").Replace(" ", "");
            }
            else
            {
                valule = chartset;
            }
            valule = valule.ToLower();
            if (valule == "gbk")
            {
                valule = "gb2312";
            }
            if (valule == "utf8")
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
        public static MatchCollection GetMulu(string muluRoot)
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
        public static string GetMuluRoot(string html)
        {
            // 获取章节目录 匹配规则一：
            Regex reg_muluroot = new Regex(@"<div class="".*"">(.|\n)*?</div>");
            MatchCollection rootMC = reg_muluroot.Matches(html);
            string muluroot = GetMuluRootFor(rootMC);
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
        public static string GetMuluRootFor(MatchCollection rootMC)
        {
            string muluroot = "";
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
        public static Dictionary<string, string> RemoteUrlList(MatchCollection list)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < list.Count; i++)
            {
                string href;
                if (list[i].Groups[1].Value.Contains("http"))
                {
                    href = list[i].Groups[1].Value;
                }
                else
                {
                    if (!list[i].Groups[1].Value.StartsWith("/"))
                    {
                        href = HttpHelpr.Host +"/"+ list[i].Groups[1].Value;
                    }
                    else
                    {
                        href = HttpHelpr.Host + list[i].Groups[1].Value;
                    }
                }
                string name = list[i].Groups[2].Value;
                if (!dic.ContainsKey(name))
                {
                    dic.Add(name, href);
                }
            }
            return dic;
        }

        /// <summary>
        /// 中文数字转纯数字
        /// </summary>
        /// <param name="word">word</param>
        /// <returns></returns>
        public static string Chinese2Num(string word)
        {
            string titlename;
            string targetword;
            if (word.Contains(" "))
            {
                string[] charItems = word.Split(' ');
                word = charItems[0].ToString();
                titlename = charItems[1].ToString();
            }
            else
            {
                if (word.Contains("章"))
                {
                    word = word.Insert(word.IndexOf("章") + 1, " ");
                    string[] charItems = word.Split(' ');
                    word = charItems[0].ToString();
                    titlename = charItems[1].ToString();
                }
                else
                {
                    return "";
                }
            }
            string cnword = FileterChineseNum(word);
            targetword = Convert2Number(cnword).ToString();
            return "第" + targetword + "章 " + titlename;
        }
        /// <summary>
        /// 获取章节中的中文数字
        /// </summary>
        /// <param name="word">word</param>
        /// <returns></returns>
        private static string FileterChineseNum(string word)
        {
            string cnword;
            Match ma_word = Regex.Match(word, @"[第|\s](.*)[章|\s]");
            string temp = "" ;
            if (!string.IsNullOrEmpty(ma_word.Groups[1].Value))
            {
                cnword = ma_word.Groups[1].Value.ToString().Replace("第", "").Replace("章", "");
            }
            else
            {
                if (!word.Contains("第") && !word.Contains("章"))
                {
                    cnword = "0";
                }
                else
                {
                    temp = word;
                    if (temp.StartsWith("第"))
                    {
                        temp = temp.Replace("第","");
                    }
                    if (temp.Contains("章"))
                    {
                        temp = temp.Replace("章","");
                    }
                    temp = temp.Replace("一",
                    "").Replace("二",
                    "").Replace("三",
                    "").Replace("四",
                    "").Replace("五",
                    "").Replace("六",
                    "").Replace("七",
                    "").Replace("八",
                    "").Replace("九",
                    "").Replace("零",
                    "").Replace("〇",
                    "").Replace("两",
                    "").Replace("百",
                    "").Replace("千",
                    "");
                    if (temp.Length>0)
                    {
                        cnword = "0";
                    }
                    else
                    {
                        cnword = word.Replace("第", "").Replace("章", "");
                    }
                    
                }
            }
            return cnword.Replace("两", "二");
        }
        /// <summary>
        /// 1万以内中文转数字
        /// </summary>
        /// <param name="src">源文本如：四千三百二十一</param>
        /// <returns></returns>
        private static int Convert2Number(string src)
        {
            // 定义包含所有数字的字符串，用以判断字符是否为数字。
            string numberString = "零一二三四五六七八九";
            string numberStringOrign = "0123456789〇零一二三四五六七八九";
            // 定义单位字符串，用以判断字符是否为单位。
            string unitString = "零十百千";
            // 把数字字符串转换为char数组，方便截取。
            char[] charArr = src.Replace(" ", "").ToCharArray();
            // 返回结果
            int result = 0;
            // 如果源为空指针、空字符串、空白字符串 则返回0
            if (string.IsNullOrEmpty(src) || string.IsNullOrWhiteSpace(src))
            {
                return 0;
            }
            if (src.StartsWith("十"))
            {
                if (charArr.Length==1)
                {
                    result = 10;
                    return result;
                }
                else
                {
                    if (charArr.Length==3)
                    {
                        src = src.Replace("十", "四");
                    }
                    else
                    {
                        result = 10 + numberString.IndexOf(charArr[1]);
                        return result;
                    }
                }
            }
            else
            {
                if (src.IndexOf("十") ==2)
                {
                    src = src.Replace("十", "");
                }
            }
            

            if (!src.Contains("十") && !src.Contains("百") && !src.Contains("千"))
            {
                src=src.Replace("一",
                    "1").Replace("二",
                    "2").Replace("三",
                    "3").Replace("四",
                    "4").Replace("五",
                    "5").Replace("六",
                    "6").Replace("七",
                    "7").Replace("八",
                    "8").Replace("九",
                    "9").Replace("零",
                    "0").Replace("〇",
                    "0");
                char[] charArr1 = src.Replace(" ", "").ToCharArray();
                for (int i = 0; i < charArr1.Length; i++)
                {
                    if (numberStringOrign.IndexOf(charArr1[i])==-1)
                    {
                        src = src.Remove(i, 1);
                    }
                }
                result = Convert.ToInt32(src);
                return result;
            }
            // 遍历字符数组
            for (int i = 0; i < charArr.Length; i++)
            {
                // 遍历单位字符串
                for (int j = 0; j < unitString.Length; j++)
                {
                    // 如果字符为单位则进行计算
                    if (charArr[i] == unitString[j])
                    {
                        // 如果字符为非'零'字符，则计算出十位以上到万位以下数字的和
                        if (charArr[i] != '零' && charArr[i] != '〇')
                        {

                            result += Convert.ToInt32(int.Parse(numberString.IndexOf(charArr[i - 1]).ToString()) * Math.Pow(10, j));

                        }
                    }
                }
            }
            // 如果源文本末尾字符为'零'-'九'其中之一，则计算结果和个位数相加。
            if (numberString.IndexOf(charArr[charArr.Length - 1]) != -1)
            {
                result += numberString.IndexOf(charArr[charArr.Length - 1]);
            }
            // 返回计算结果。
            return result;
        }
    }
}

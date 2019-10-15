using System;
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
                throw ;
            }
        }
    }
}

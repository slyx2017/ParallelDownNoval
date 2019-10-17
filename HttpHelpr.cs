using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinReadBook
{
    public class HttpHelpr
    {

        public static string Host { get; set; }
        public static string HttpGet(string url,string postDataStr,string ismulu,string contentType= "utf-8")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.KeepAlive = false;
            HttpWebResponse response;
            Host = request.Address.Scheme.ToString()+"://"+request.Address.Host.ToString();
            
            request.ContentType = "text/html;charset=UTF-8";
            try
            {
                System.Net.ServicePointManager.DefaultConnectionLimit = 50;
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            //如果返回的数据进行压缩的话要解压缩
            if (response.ContentEncoding.Contains("gzip"))
            {
                ismulu = "0";
            }
            Stream myResponseStream;
            if (ismulu=="0")
            {
                myResponseStream = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);

            }
            else
            {
                myResponseStream = response.GetResponseStream();
            }


            string retString;
            try
            {
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(contentType));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

            }
            catch (Exception ex)
            {
                if (ismulu == "0")
                {
                    myResponseStream = response.GetResponseStream();
                }
                else
                {
                    myResponseStream = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);

                }
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(contentType));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

            }
            
            
            return retString;
        }

    }
}

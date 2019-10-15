using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinReadBook
{
    public class LogHelper
    {
        /// <summary>
        /// 静态只读实体对象info信息
        /// </summary>
        public static readonly log4net.ILog Loginfo = log4net.LogManager.GetLogger("loginfo");
        public static readonly log4net.ILog Loginfo1 = log4net.LogManager.GetLogger("loginfo1");
        public static readonly log4net.ILog Loginfo2 = log4net.LogManager.GetLogger("loginfo2");
        public static readonly log4net.ILog Loginfo3 = log4net.LogManager.GetLogger("loginfo3");

        /// <summary>
        ///  添加info信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog(string info)
        {
            try
            {
                if (Loginfo.IsInfoEnabled)
                {
                    Loginfo.Info(info);
                }
            }
            catch { }
        }
        /// <summary>
        ///  添加info信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog1(string info)
        {
            try
            {
                if (Loginfo1.IsInfoEnabled)
                {
                    Loginfo1.Info(info);
                }
            }
            catch { }
        }
        /// <summary>
        ///  添加info信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog2(string info)
        {
            try
            {
                if (Loginfo2.IsInfoEnabled)
                {
                    Loginfo2.Info(info);
                }
            }
            catch { }
        }
        /// <summary>
        ///  添加info信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog3(string info)
        {
            try
            {
                if (Loginfo3.IsInfoEnabled)
                {
                    Loginfo3.Info(info);
                }
            }
            catch { }
        }
    }
}

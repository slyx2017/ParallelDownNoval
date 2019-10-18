using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinReadBook
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        List<string> list3 = new List<string>();
        List<string> list4 = new List<string>();
        List<string> list5 = new List<string>();
        List<string> list6 = new List<string>();
        private void BtnBrowserDialog_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                txtLocalPath.Text = folderBrowserDialog1.SelectedPath;
            }

            
            string[] txtFiles = Directory.GetFiles(txtLocalPath.Text, "*.txt");
            for (int i = 0; i < txtFiles.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(txtFiles[i]);
                string filename = fileInfo.Name.Split(' ')[0];
                if (filename.Length==3)
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
            //for (int j = 0; j < list3.Count; j++)
            //{
            //    listBox1.Items.Add(list3[j]);
            //}

            //for (int j = 0; j < list4.Count; j++)
            //{
            //    listBox1.Items.Add(list4[j]);
            //}

            //for (int j = 0; j < list5.Count; j++)
            //{
            //    listBox1.Items.Add(list5[j]);
            //}

            //for (int j = 0; j < list6.Count; j++)
            //{
            //    listBox1.Items.Add(list6[j]);
            //}
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
    }
}

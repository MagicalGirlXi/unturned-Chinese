using GetUnItems.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetUnItems
{
    public partial class Form1 : Form
    {
        private DFS xixi;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            xixi = new DFS("C:\\Users\\magic\\Documents\\Git\\unturned-Chinese\\Items");
            xixi.Search();
            MessageBox.Show("Generated!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //StreamReader读取
            int count = 0;
            
            //using (Stream readerStream = new FileStream(@"d:\list.txt", FileMode.Open))
            //using (StreamReader reader = new StreamReader(readerStream, Encoding.UTF8))
            using (WebClient client = new WebClient())
            {
                string line = "http://img.unturnedid.cn/item/";
                for (int i = 1010;i < 1525;i++)
                {
                    count++;
                    Console.WriteLine(line + i + ".png");
                    Uri uri = new Uri(line+i+".png");
                    if (uri != null)
                    {
                        string filename = Path.GetFileName(uri.LocalPath);
                        try
                        {
                            client.DownloadFile(uri, @"C:\Users\magic\Documents\Git\unturned-Chinese\pic\" + filename);
                        }catch(Exception E)
                        {
                            ;
                        }
                        Console.WriteLine("文件：" + filename + " 下载成功！" + " 计数：" + count);
                    }
                    else
                    {
                        Console.WriteLine("路径：" + line + " 不是下载地址！失败序号：" + count);
                    }

                }
            }

            MessageBox.Show("下载完成！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx0712b34214fed882&secret=");
            req.Method = "Get";
            req.ContentType = "application/x-www-form-urlencoded";
            /*
            byte[] data = Encoding.UTF8.GetBytes(str);//把字符串转换为字节

            req.ContentLength = data.Length; //请求长度

            using (Stream reqStream = req.GetRequestStream()) //获取
            {
                reqStream.Write(data, 0, data.Length);//向当前流中写入字节
                reqStream.Close(); //关闭当前流
            }
            */
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            textBox1.Text = result; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/datacube/getweanalysisappiddailyretaininfo?access_token=36_FXuTSKeM6wKpAiAGF_UHVVjttFmrltkoxQ4h5CcQCPzUMWmEteme74DHg3emjgV6qR_43QNVA57ZcYkQpuRiXiuPjRQYjgxNpoCPLxqeNU_fS-1zZP64024CSWtRfTHfC8bAaymSyOdRu76RCZGcAIAJGN");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string str = "{\"begin_date\" : \"20200810\",\"end_date\" : \"20200810\"}";
            byte[] data = Encoding.UTF8.GetBytes(str);//把字符串转换为字节

            req.ContentLength = data.Length; //请求长度

            using (Stream reqStream = req.GetRequestStream()) //获取
            {
                reqStream.Write(data, 0, data.Length);//向当前流中写入字节
                reqStream.Close(); //关闭当前流
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            textBox1.Text = result;
        }
    }
}

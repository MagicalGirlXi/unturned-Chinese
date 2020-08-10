using GetUnItems.domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetUnItems.util
{
    class DFS
    {
        private String path;
        public DFS(String SelectedPath)
        {
            path = SelectedPath;
        }
        public DFS()
        {
            path = Application.StartupPath;
        }
        public void Search()
        {
            string lei = "";
            DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();//目录们
            Dictionary<String, ArrayList> d = new Dictionary<String, ArrayList>();//物品类的map
            for (int i = 0;i < directoryInfos.Length; i++)
            {
                lei += directoryInfos[i].Name;
                //MessageBox.Show(directoryInfos[i].Name);
                lei += "\n";
                DirectoryInfo[] temp = directoryInfos[i].GetDirectories();//当前物品类的文件夹
                ArrayList CurrentTypeItems = new ArrayList();//当前类的UnItem集合
                for (int j = 0; j < temp.Length; j++)
                {
                    UnItem CurrentItem = new UnItem();
                    String cnname = GetName(temp[j]);
                    if (cnname == null)
                    {
                        cnname = temp[j].Name;
                    }
                    CurrentItem.CnName = cnname;
                    CurrentItem.Name = temp[j].Name;
                    CurrentItem.Description = GetDescription(temp[j]);
                    CurrentItem.Props = ItemUtil.GetProperities(temp[j]);
                    CurrentTypeItems.Add(CurrentItem);
                    //MessageBox.Show(CurrentItem.Name + CurrentItem.Description);
                }
                d.Add(directoryInfos[i].Name, CurrentTypeItems);//把这个类的集合添加到所有物品中
                //MessageBox.Show(CurrentTypeItems.ToString());
                //showChilds(temp);
            }
            //MessageBox.Show(lei);
            //Display(d);
            GenerateJson(d);
        }
        private void showChilds(DirectoryInfo[] a)
        {
            string s = "";
            for (int i = 0; i < a.Length; i++)
            {
                s += a[i].Name;
                s += "\n";
            }
            MessageBox.Show(s);
        }
        private string GetDescription(DirectoryInfo a)
        {
            string result;
            FileInfo[] fileInfos = a.GetFiles();
            for (int i = 0; i < fileInfos.Length; i++)
            {
                if (fileInfos[i].Name.Equals("Schinese.dat"))
                {
                    StreamReader sr = new StreamReader(fileInfos[i].FullName);
                    sr.ReadLine();
                    while (sr.Read() != ' ' && !sr.EndOfStream)
                        ;
                    result = sr.ReadLine();
                    //MessageBox.Show(result);
                    sr.Close();
                    return result;

                }
            }
            return null;
        }
        private string GetName(DirectoryInfo a)
        {
            string result;
            FileInfo[] fileInfos = a.GetFiles();
            for (int i = 0; i < fileInfos.Length; i++)
            {
                if (fileInfos[i].Name.Equals("Schinese.dat"))
                {
                    StreamReader sr = new StreamReader(fileInfos[i].FullName);
                    while (sr.Read() != ' ' && !sr.EndOfStream)
                        ;
                    result = sr.ReadLine();
                    //MessageBox.Show(result);
                    sr.Close();
                    return result;

                }
            }
            return null;
        }
        private string Display(Dictionary<String, ArrayList> d)
        {
            StreamWriter sw = new StreamWriter("result.txt");
            foreach (string key in d.Keys)

            {

                sw.WriteLine("——————————————————————" + key + "——————————————————————");
                foreach (UnItem u in d[key])
                {
                    string id = null;
                    if (u.Props.ContainsKey("ID"))
                    {
                        id = u.Props["ID"];
                    }
                    else
                    {
                        id = "Null";
                    }
                    sw.WriteLine(id + " = " + u.Name + " | " + u.Description);
                }

            }
            return null;
        }
        private void GenerateJson(Dictionary<String, ArrayList> d)
        {
            StreamWriter sw = new StreamWriter("json.txt");
            sw.WriteLine("[");
            Boolean firstLei = true;
            int index = 0;
            foreach (string key in d.Keys)

            {
                if (!firstLei)
                {
                    sw.Write(",");
                }
                else
                {
                    firstLei = false;
                }
                sw.WriteLine("{");
                sw.WriteLine("\"Name\": \"" + key + "\",");
                sw.WriteLine("\"Index\": \"" + index++ + "\",");
                sw.WriteLine("\"Contain\": ");
                sw.WriteLine("[");
                Boolean first = true;
                foreach (UnItem u in d[key])
                {
                    if (!first)
                    {
                        sw.Write(",");
                    }
                    else
                    {
                        first = false;
                    }
                    sw.WriteLine("{");
                    string id = null;
                    if (u.Props.ContainsKey("ID"))
                    {
                        id = u.Props["ID"];
                    }
                    else
                    {
                        id = "Null";
                    }
                    string url = null;
                    if (File.Exists(@"C:\Users\magic\Documents\Git\unturned-Chinese\pic\"+id+".png"))
                    {
                        url = "https://ut-1257119641.cos.ap-beijing.myqcloud.com/" + id + ".png";
                    }
                    else
                    {
                        url = "../../resources/erpic.jpg";
                    }
                    string rar = null;
                    if (u.Props.ContainsKey("Rarity"))
                    {
                        rar = u.Props["Rarity"];
                    }
                    else
                    {
                        rar = "Null";
                    }
                    sw.WriteLine("\"Name\": \"" + u.Name + "\",");
                    sw.WriteLine("\"CnName\": \"" + u.CnName + "\",");
                    sw.WriteLine("\"ID\": \"" + id + "\",");
                    sw.WriteLine("\"Rarity\": \"" + rar + "\",");//Rarity Epic
                    sw.WriteLine("\"pic\": \"" + url + "\",");//Rarity Epic
                    string desc = u.Description;//删除description里的html标签
                    if (desc != null)
                    {
                        desc = System.Text.RegularExpressions.Regex.Replace(desc, "<", "");
                        desc = System.Text.RegularExpressions.Regex.Replace(desc, ">", "");
                        desc = System.Text.RegularExpressions.Regex.Replace(desc, "\"", "\'");
                        sw.WriteLine("\"Description\": \"" + desc + "\"");
                    }
                    else
                    {
                        sw.WriteLine("\"Description\": \"" + "该物品无描述" + "\"");
                    }
                    sw.WriteLine("}");
                }
                sw.WriteLine("]");
                sw.WriteLine("}");

            }
            sw.WriteLine("]");
            sw.Flush();
            sw.Close();
        }
    }
}

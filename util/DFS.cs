using GetUnItems.domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                lei += "\n";
                DirectoryInfo[] temp = directoryInfos[i].GetDirectories();//当前物品类的文件夹
                ArrayList CurrentTypeItems = new ArrayList();//当前类的UnItem集合
                for (int j = 0; j < temp.Length; j++)
                {
                    UnItem CurrentItem = new UnItem();
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
            DisplayJson(d);
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
                if (fileInfos[i].Name.Equals("English.dat"))
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
        private string DisplayJson(Dictionary<String, ArrayList> d)
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
    }
}

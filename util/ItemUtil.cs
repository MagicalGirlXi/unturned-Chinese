using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetUnItems.util
{
    class ItemUtil
    {
        public static Dictionary<String, String> GetProperities(DirectoryInfo a)
        {
            Dictionary<String, String> prop = new Dictionary<String, String>();
            FileInfo[] fileInfos = a.GetFiles();
            Console.WriteLine(fileInfos.Length);
            for (int i = 0; i < fileInfos.Length; i++)
            {
                if (fileInfos[i].Name.Equals(a.Name + ".dat") || fileInfos[i].Name.Equals("Asset.dat"))
                {
                    Console.WriteLine(fileInfos[i].FullName);
                    StreamReader sr = new StreamReader(fileInfos[i].FullName);
                    char temp;
                    while (sr.Peek() != -1)
                    {
                        string key="";
                        string value="";
                        while (sr.Peek()!=-1 && (temp = (char)sr.Read()) != ' ')
                        {
                            if (temp != '\n' && temp != '\r')
                                key += temp;
                        }
                        if (!key.Equals("//"))
                        {
                            value = sr.ReadLine();
                            prop.Add(key, value);
                        }
                        else
                        {
                            sr.ReadLine();
                        }
                    }

                }
            }
            return prop;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBNiuBi.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class DirectoryUtil
    {
        public static List<string> GetFileList(string path, string extName)
        {
            var lst = new List<string>();
            DirectoryInfo fdir = new DirectoryInfo(path);
            FileInfo[] file = fdir.GetFiles();
            //FileInfo[] file = Directory.GetFiles(path); //文件列表   
            if (file.Length != 0) //当前目录文件或文件夹不为空                   
            {
                foreach (FileInfo f in file) //显示当前目录所有文件   
                {
                    if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                    {
                        lst.Add(f.FullName);
                    }
                }
            }

            return lst;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CrabRobot.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class FileUtil
    {
        public static long GetFileLength(string file)
        {
            if (File.Exists(file))
            {
                FileStream s = new FileStream(file, FileMode.Open, FileAccess.Read);
                var len = s.Length;
                s.Close();
                return len;
            }

            return 0;
        }
        //public static void DeleteDirectory(string path)
        //{
        //    //目录为空，删除目录
        //    DirectoryInfo directoryInfo = new DirectoryInfo(path);
        //    //如果有子目录，先循环删除子目录，再删除当前目录
        //    //directoryInfo.Delete(true);
        //    Directory.DeleteDirectory(path);
        //}
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
        /// <summary>
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void MoveFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //覆盖模式
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Move(c, destFile);
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。
                    //Directory.Move(c, destDir);

                    //采用递归的方法实现
                    MoveFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }

        public static int GetFileCount(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return 0;
            }
            string[] files = Directory.GetFiles(directory);
            return files.Length;
        }

        public static List<string> GetFileList(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return new List<string>();
            }
            return Directory.GetFiles(directory).ToList();
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

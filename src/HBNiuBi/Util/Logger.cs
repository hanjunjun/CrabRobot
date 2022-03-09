using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HBNiuBi.Util
{
    public class Logger
    {
        private static readonly object _lock = new object();
        //队列元素
        private static ConcurrentQueue<Tuple<string, string>> logQueue = new ConcurrentQueue<Tuple<string, string>>();

        private static Task writeTask = default;

        //static ManualResetEvent pause = new ManualResetEvent(false);//开始是无信号的

        static Logger()
        {
            //开一个长时间运行的task
            writeTask = new Task((obj) =>
            {
                while (true)
                {
                    try
                    {
                        //pause.WaitOne();//等待信号到来
                        //pause.Reset();//设置无信号
                        List<string[]> temp = new List<string[]>();
                        foreach (var logItem in logQueue)
                        {
                            string logPath = logItem.Item1;
                            string logMergeContent = string.Concat(logItem.Item2, Environment.NewLine);//, Environment.NewLine, ""
                            string[] logArr = temp.FirstOrDefault(d => d[0].Equals(logPath));//取出路径相同的记录
                            if (logArr != null)
                            {
                                //如果找到相同路径的记录，就在写入内容后面加上。
                                logArr[1] = string.Concat(logArr[1], logMergeContent);
                            }
                            else
                            {
                                //如果没找到相同路径的记录，加一个新的list
                                logArr = new string[] { logPath, logMergeContent };
                                temp.Add(logArr);
                            }
                            Tuple<string, string> val = default;
                            logQueue.TryDequeue(out val);//删除队列头的元素
                        }
                        foreach (string[] item in temp)//写入文件
                        {
                            WriteText(item[0], item[1]);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            , null
            , TaskCreationOptions.LongRunning);//意味着该任务将长时间运行，因此他不是在线程池中执行。
            writeTask.Start();
        }

        public static string GetStackTrace()
        {
            StackTrace trace = new StackTrace(true);
            StackFrame frame = trace.GetFrame(3);//1代表上级，2代表上上级，以此类推
            var codeLineNumb = frame.GetFileLineNumber();
            MethodBase method = frame.GetMethod();
            var nameSpace = method.ReflectedType.FullName;
            return $"{nameSpace}.{method.Name}:{codeLineNumb}";
        }

        /// <summary>
        /// 记录关键信息日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logDir">指定日志存储路径</param>
        /// <param name="isShowConsoleLog">是否在控制台显示日志</param>
        public static void Info(string logContent, string[] logDir = null, bool isShowConsoleLog = true, bool isShowMiniProfiler = true, ConsoleColor consoleColor = ConsoleColor.Black)
        {
            WriteProfilerLog(logContent, "Info", isShowConsoleLog, logDir, isShowMiniProfiler, consoleColor);
        }

        /// <summary>
        /// 记录调试信息日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logDir">指定日志存储路径</param>
        /// <param name="isShowConsoleLog">是否在控制台显示日志</param>
        public static void Debug(string logContent, string[] logDir = null, bool isShowConsoleLog = true, bool isShowMiniProfiler = true, ConsoleColor consoleColor = ConsoleColor.Black)
        {
            WriteProfilerLog(logContent, "Debug", isShowConsoleLog, logDir, isShowMiniProfiler, consoleColor);
        }

        /// <summary>
        /// 记录错误信息日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logDir">指定日志存储路径</param>
        /// <param name="isShowConsoleLog">是否在控制台显示日志</param>
        public static void Error(string logContent, string[] logDir = null, bool isShowConsoleLog = true, bool isShowMiniProfiler = true, ConsoleColor consoleColor = ConsoleColor.Black)
        {
            WriteProfilerLog(logContent, "Error", isShowConsoleLog, logDir, isShowMiniProfiler, consoleColor);
        }

        /// <summary>
        /// 1.多线程+信号量+队列写本地日志
        /// 2.日志调试入口
        /// </summary>
        /// <param name="pathLevel">日志目录级别</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="isError">是否报错</param>
        /// <param name="errorTitle">错误标题</param>
        private static void WriteProfilerLog(string logContent, string logLevel, bool isShowConsoleLog, string[] logDir, bool isShowMiniProfiler, ConsoleColor consoleColor)
        {
            var now = DateTime.Now.ToString("HH:mm:ss:fff");
            //日志目录
            var requestId = string.Empty;
            var pathItem = new string[0];
            if (logDir == null)
            {

            }
            else
            {
                pathItem = logDir;
            }
            //日志级别颜色
            //ConsoleColor consoleColor;
            //输出内容
            var consoleLog = logContent;
            //日志内容
            logContent =
            $"{now}|{logLevel}{(string.IsNullOrEmpty(requestId) ? "" : $@"|{requestId}")}|{GetStackTrace()}|{logContent}";
            if (consoleColor == ConsoleColor.Black)
            {
                if (logLevel.Equals("Error"))
                {
                    consoleColor = ConsoleColor.Red;

                }
                else if (logLevel.Equals("Debug"))
                {
                    consoleColor = ConsoleColor.Blue;
                }
                else
                {
                    consoleColor = ConsoleColor.DarkYellow;
                }
            }
            else
            {
            }

            WriteLog(pathItem, logContent);
        }

        private static void WriteLog(string[] customDirectory, string infoData)
        {
            string logPath = GetLogPath(customDirectory, string.Empty);
            logQueue.Enqueue(new Tuple<string, string>(logPath, infoData));
            try
            {
                //只保留x天日志
                var dir = Path.GetDirectoryName(logPath);
                var list = FileUtil.GetFileList(dir);
                var data = list.Where(x =>
                {
                    var fileName = Path.GetFileNameWithoutExtension(x);
                    var spName = fileName.Substring(fileName.IndexOf('('));
                    fileName = fileName.Replace(spName, "");
                    fileName = DateTime.ParseExact(fileName, "yyyyMMdd", CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
                    DateTime value;
                    var dt = DateTime.TryParse(fileName, out value);
                    if (dt)
                    {
                        if (value < DateTime.Now.AddDays(-30))
                        {
                            return true;
                        }
                    }
                    return false;
                }).ToList();
                foreach (var path in data)
                {
                    FileUtil.DeleteFile(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="customDirectory"></param>
        /// <param name="preFile"></param>
        /// <returns></returns>
        private static string GetLogPath(string[] customDirectory, string preFile)
        {
            string newFilePath = string.Empty;
            string logDir = string.Empty;
            var list = customDirectory.ToList();
            var logPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\";
            foreach (var item in list)
            {
                logPath += item + @"\";
            }
            logDir = logPath;
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            string extension = ".log";
            string fileNameNotExt = string.Concat(preFile, DateTime.Now.ToString("yyyyMMdd"));
            string fileName = string.Concat(fileNameNotExt, extension);
            string fileNamePattern = string.Concat(fileNameNotExt, "(*)", extension);
            List<string> filePaths = Directory.GetFiles(logDir, fileNamePattern, SearchOption.TopDirectoryOnly).ToList();

            if (filePaths.Count > 0)
            {
                int fileMaxLen = filePaths.Max(d => d.Length);
                string lastFilePath = filePaths.Where(d => d.Length == fileMaxLen).OrderByDescending(d => d).FirstOrDefault();
                //超过50mb就要用新目录保存
                if (new FileInfo(lastFilePath).Length > 30 * 1024 * 1024)
                {
                    string no = new Regex(@"(?is)(?<=\()(.*)(?=\))").Match(Path.GetFileName(lastFilePath)).Value;
                    int tempno = 0;
                    bool parse = int.TryParse(no, out tempno);
                    string formatno = string.Format("({0})", parse ? tempno + 1 : tempno);
                    string newFileName = string.Concat(fileNameNotExt, formatno, extension);
                    newFilePath = Path.Combine(logDir, newFileName);
                }
                else
                {
                    newFilePath = lastFilePath;
                }
            }
            else
            {
                string newFileName = string.Concat(fileNameNotExt, string.Format("({0})", 0), extension);
                newFilePath = Path.Combine(logDir, newFileName);
            }
            return newFilePath;
        }

        private static void WriteText(string logPath, string logContent)
        {
            lock (_lock)
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                    }
                    if (!File.Exists(logPath))
                    {
                        var create = File.CreateText(logPath);
                        create.Close();
                        create.Dispose();
                    }
                    StreamWriter sw = File.AppendText(logPath);
                    sw.Write(logContent);
                    sw.Close();
                    sw.Dispose();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"写日志出现异常：{ex.Message}", ConsoleColor.Red);
                }
            }
        }
    }
}

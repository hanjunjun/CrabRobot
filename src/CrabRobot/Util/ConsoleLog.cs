using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrabRobot.Util
{
    public class ConsoleLog
    {
        private static readonly object _lock = new object();
        //队列元素
        private static ConcurrentQueue<Tuple<string, MessageContainer>> logQueue = new ConcurrentQueue<Tuple<string, MessageContainer>>();

        private static Task writeTask = default;

        static ManualResetEvent pause = new ManualResetEvent(false);
        static ConsoleLog()
        {
            //开一个长时间运行的task
            writeTask = new Task((obj) =>
            {
                while (true)
                {
                    try
                    {
                        pause.WaitOne();//等待信号到来
                        pause.Reset();//设置无信号
                        List<object[]> temp = new List<object[]>();
                        foreach (var logItem in logQueue)
                        {
                            string logPath = logItem.Item1;
                            string logMergeContent = string.Concat(logItem.Item2.Message, Environment.NewLine);//, Environment.NewLine, ""
                            var logArr = temp.FirstOrDefault(d => ((RichTextBox)d[0]).Tag.ToString().Equals(logPath));//取出路径相同的记录
                            if (logArr != null)
                            {
                                //如果找到相同路径的记录，就在写入内容后面加上。
                                logArr[1] = string.Concat(logArr[1], logMergeContent);
                            }
                            else
                            {
                                //如果没找到相同路径的记录，加一个新的list
                                logArr = new object[] { logItem.Item2.RichTextBox, logMergeContent };
                                temp.Add(logArr);
                            }
                            logQueue.TryDequeue(out var val);//删除队列头的元素
                        }
                        foreach (var item in temp)//写入文件9
                        {
                            WriteIntoRichTextBox((RichTextBox)item[0], item[1].ToString());
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
        private static void WriteIntoRichTextBox(RichTextBox richTextBox, string msg, Color? color = null)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new EventHandler(delegate
                {
                    //richTextBox.SelectionColor = color;
                    richTextBox.SelectionColor = Color.Blue;
                    richTextBox.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{msg}");
                    richTextBox.SelectionStart = richTextBox.Text.Length;
                    richTextBox.ScrollToCaret();
                    if (richTextBox.TextLength > 5000)
                    {
                        richTextBox.Clear();
                    }
                }), null);
            }
            else
            {
                //richTextBox.SelectionColor = color;
                richTextBox.SelectionColor = Color.Blue;
                richTextBox.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{msg}");
                richTextBox.SelectionStart = richTextBox.Text.Length;
                richTextBox.ScrollToCaret();
                if (richTextBox.TextLength > 10000)
                {
                    richTextBox.Clear();
                }
            }
        }
        public static void AddMessage(RichTextBox richTextBox, string msg, Color color)
        {
            logQueue.Enqueue(new Tuple<string, MessageContainer>(richTextBox.Tag.ToString(),
                new MessageContainer()
                {
                    RichTextBox = richTextBox,
                    Message = msg,
                    Color = color
                }));
            pause.Set();
            //写日志到不同用户
            var userTag = richTextBox.Tag.ToString();
            if (color == Color.Red)
            {
                Logger.Error(msg, new string[] { userTag });
            }
            else if (color == Color.Green)
            {
                Logger.Success(msg, new string[] { userTag });
            }
            else if (color == Color.Yellow)
            {
                Logger.Warning(msg, new string[] { userTag });
            }
            else
            {
                Logger.Info(msg, new string[] { userTag });
            }
        }
        public class MessageContainer
        {
            public RichTextBox RichTextBox { get; set; }
            public string Message { get; set; }
            public Color Color { get; set; }
        }
    }
}

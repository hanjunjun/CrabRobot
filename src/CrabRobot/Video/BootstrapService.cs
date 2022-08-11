using CrabRobot;
using CrabRobot.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CrabRobot.Video
{
    public class BootstrapService
    {
        private readonly Task ScreenVideoTask = default;
        private bool IsExit = false;
        private ScreenVideoManager ScreenVideo;
        private string path;
        public BootstrapService()
        {
            Logger.Info($"准备开始录像");
            while (true)
            {
                if (IsExit) return;
                try
                {
                    //判断是否在配置的时间段内，不在时间段不录像
                    ToolSettingsConfig toolSettingsConfig = new ToolSettingsConfig(Const.ScriptXmlConfig.ToolSettingsConfig);
                    var videoConfigs = toolSettingsConfig.GetConfig().VideoConfig;
                    if (videoConfigs.EnableVideo)
                    {
                        path = videoConfigs.VideoSavePath;
                        var day = videoConfigs.SaveDayLine;
                        var startSlot = videoConfigs.StartHour;
                        var endSlot = videoConfigs.EndHour;
                        var startTime = DateTime.Now.Date.AddHours(startSlot);
                        var endTime = DateTime.Now.Date.AddHours(endSlot).AddMinutes(59).AddSeconds(59);
                        var now = DateTime.Now;
                        var inTime = TimeUtil.IsInTimeSlot(now, startTime, endTime);
                        if (!inTime)
                        {
                            //不在时间段内
                            if (DateTime.Now.Minute == 30)
                            {
                                Logger.Debug($"{now.ToString("yyyy-MM-dd HH:mm:ss")}不在录像{videoConfigs.StartHour}-{videoConfigs.EndHour}时间段内");
                            }
                            //如果不在录像时间段内，且正在录像中，则停止录像
                            if (ScreenVideo != null && ScreenVideo.Running)
                            {
                                Logger.Info("不在录像时间段内停止录像服务", new string[] { "录像" });
                                ScreenVideo.ExitScreenVideo(false);
                            }
                            Thread.Sleep(5000);
                            continue;
                        }
                        if (ScreenVideo != null)
                        {
                            //正在录像中
                            string savePath = GetFileName(path);
                            if (File.Exists(savePath))
                            {
                                //文件已存在录像中
                            }
                            else
                            {
                                //文件不存在，重新录制下个小时
                                //只保留最近x天的录像,删除x天之前的ZIP包
                                var fileList = DirectoryUtil.GetFileList(path, ".zip");
                                var list = fileList.Where(x =>
                                {
                                    //筛选出x天之前的包
                                    var dateLine = DateTime.Now.Date.AddDays(0 - day);
                                    var dt = DateTime.Parse(Path.GetFileNameWithoutExtension(x));
                                    if (dt < dateLine)
                                    {
                                        //删除
                                        return true;
                                    }

                                    return false;
                                }).ToList();
                                foreach (var file in list)
                                {
                                    File.Delete(file);
                                    Logger.Info($"删除文件：{file}", new string[] { "录像" });
                                }
                                //当前flv文件数量大于x个，则执行zip打包，x=配置的小时时间段小时数量
                                fileList = DirectoryUtil.GetFileList(path, ".flv");
                                var count = endSlot - startSlot;
                                Logger.Info($"flv文件：{fileList.Count}个，上限：{count}", new string[] { "录像" });
                                //先停止录像，否则文件被占用打包会报错
                                ScreenVideo.ExitScreenVideo(false);
                                Logger.Info($"停止录像", new string[] { "录像" });
                                if (fileList.Count >= count)
                                {
                                    //执行zip打包
                                    var zipPackName = path + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";
                                    //在开一个线程打包，防止打包时间太久，录像丢失
                                    Thread compressTask = new Thread(() =>
                                    {
                                        try
                                        {
                                            var op = SharpZipLibHelper.CompressFile(fileList, zipPackName);
                                            Logger.Info($"打包成功：{zipPackName}", new string[] { "录像" });
                                            if (op)
                                            {
                                                //删除遗留文件
                                                foreach (var sfile in fileList)
                                                {
                                                    File.Delete(sfile);
                                                    Logger.Info($"删除遗留文件：{sfile}", new string[] { "录像" });
                                                }
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Logger.Error($"打包失败：{e.Message},{zipPackName}");
                                        }
                                    });
                                    compressTask.IsBackground = true;
                                    compressTask.Start();
                                }
                                //开始录制当前小时的录像
                                ScreenVideo = new ScreenVideoManager(savePath);
                                ScreenVideo.StartRec();
                                Logger.Info($"开始录像：{savePath}", new string[] { "录像" });
                            }
                        }
                        else
                        {
                            //重新配置录像
                            string savePath = GetFileName(path);
                            ScreenVideo = new ScreenVideoManager(savePath);
                            ScreenVideo.StartRec();
                            Logger.Info($"开始录像：{savePath}", new string[] { "录像" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"录像服务出错：{ex}", new string[] { "录像" });
                }
                finally
                {
                    Thread.Sleep(10 * 1000);
                }
            }
            //录像
            ScreenVideoTask = new Task((obj) =>
            {
                
            }, null
                , TaskCreationOptions.LongRunning);
        }

        public string GetFileName(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string newFilePath = string.Empty;
            string extension = ".flv";
            string fileNameNotExt = string.Format(@"{0}", DateTime.Now.ToString("yyyy-MM-dd_HH点"));
            string fileName = string.Concat(fileNameNotExt, extension);
            string fileNamePattern = string.Concat(fileNameNotExt, "(*)", extension);
            List<string> filePaths = Directory.GetFiles(path, fileNamePattern, SearchOption.TopDirectoryOnly).ToList();

            if (filePaths.Count > 0)
            {
                int fileMaxLen = filePaths.Max(d => d.Length);
                string lastFilePath = filePaths.Where(d => d.Length == fileMaxLen).OrderByDescending(d => d).FirstOrDefault();
                if (ScreenVideo == null)
                {
                    //首次运行
                    string no = new Regex(@"(?is)(?<=\()(.*)(?=\))").Match(Path.GetFileName(lastFilePath)).Value;
                    int tempno = 0;
                    bool parse = int.TryParse(no, out tempno);
                    string formatno = string.Format("({0})", parse ? tempno + 1 : tempno);
                    string newFileName = string.Concat(fileNameNotExt, formatno, extension);
                    newFilePath = Path.Combine(path, newFileName);
                }
                else
                {
                    newFilePath = lastFilePath;
                }
            }
            else
            {
                string newFileName = string.Concat(fileNameNotExt, string.Format("({0})", 0), extension);
                newFilePath = Path.Combine(path, newFileName);
            }
            return newFilePath;
        }

        public void ScreenVideoService()
        {
            ScreenVideoTask.Start();
        }
    }
}

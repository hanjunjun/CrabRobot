using HBNiuBi.Config;
using HBNiuBi.From.Impl;
using HBNiuBi.Model;
using HBNiuBi.Util;
using HBNiuBi.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBNiuBi.From
{
    public partial class ToolSettingsForm : NoMinMaxButtonTopForm
    {
        private readonly ToolSettingsModel toolSettingsModel;
        public ToolSettingsForm()
        {
            InitializeComponent();
            //加载现有的配置
            ToolSettingsConfig toolSettingsConfig = new ToolSettingsConfig(Const.ScriptXmlConfig.ToolSettingsConfig);
            var config = toolSettingsConfig.GetConfig();
            toolSettingsModel = config;
            //dm配置
            txtDmCode.Text = config.DMSecret.Code;
            txtDmVer.Text = config.DMSecret.Ver;
            ckbDebug.Checked = config.DMSecret.Debug;
            //录像设置
            ckbEnableVideo.Checked = config.VideoConfig.EnableVideo;
            dtpStart.Value = DateTime.Now.Date.AddHours(config.VideoConfig.StartHour);
            dtpEnd.Value = DateTime.Now.Date.AddHours(config.VideoConfig.EndHour);
            cmbSaveDayLine.SelectedItem = config.VideoConfig.SaveDayLine.ToString();
            txtVideSavePath.Text = config.VideoConfig.VideoSavePath;
            //预警邮箱设置
            txtMailServer.Text = config.MailServerConfig.MailServer;
            txtMailServerPort.Text = config.MailServerConfig.MailServerPort.ToString();
            txtSendMail.Text = config.MailServerConfig.SendMail;
            txtMailPassword.Text = config.MailServerConfig.MailPassword;
            txtTestReceiveMail.Text = config.MailServerConfig.ReceiveMail;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ToolSettingsModel toolSettingsConfig = new ToolSettingsModel();
            //dm组件设置
            toolSettingsConfig.DMSecret.Code = txtDmCode.Text;
            toolSettingsConfig.DMSecret.Ver = txtDmVer.Text;
            toolSettingsConfig.DMSecret.Debug = ckbDebug.Checked;
            //录像设置
            toolSettingsConfig.VideoConfig.EnableVideo = ckbEnableVideo.Checked;
            toolSettingsConfig.VideoConfig.StartHour = dtpStart.Value.Hour;
            toolSettingsConfig.VideoConfig.EndHour = dtpEnd.Value.Hour;
            toolSettingsConfig.VideoConfig.SaveDayLine = Convert.ToInt32(cmbSaveDayLine.SelectedItem);
            toolSettingsConfig.VideoConfig.VideoSavePath = txtVideSavePath.Text;
            //预警邮箱设置
            toolSettingsConfig.MailServerConfig.MailServer = txtMailServer.Text;
            toolSettingsConfig.MailServerConfig.MailServerPort = Convert.ToInt32(txtMailServerPort.Text);
            toolSettingsConfig.MailServerConfig.SendMail = txtSendMail.Text;
            toolSettingsConfig.MailServerConfig.MailPassword = txtMailPassword.Text;
            toolSettingsConfig.MailServerConfig.ReceiveMail = txtTestReceiveMail.Text;
            //保存到本地xml
            var configManager = new ToolSettingsConfig(Const.ScriptXmlConfig.ToolSettingsConfig);
            configManager.SaveOrUpdateConfig(toolSettingsConfig);
            var dialog = MessageBox.Show(this, "保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (dialog == DialogResult.OK)
            {
                this.Close();
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 邮件发送测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            SendMail("", AppDomain.CurrentDomain.BaseDirectory+ @"Resources\loginSelect.bmp");
        }
        public void SendMail(string filePath, string imagePath)
        {
            try
            {
                var subject = toolSettingsModel.MailServerConfig.MailSubject;
                var body = toolSettingsModel.MailServerConfig.MailBody;
                var sendMail = txtSendMail.Text;
                var account = sendMail.Split("@")[0];
                var receiveMail = txtTestReceiveMail.Text;
                var mailServer = txtMailServer.Text;
                var mailServerPort = Convert.ToInt32(txtMailServerPort.Text);
                var mailPassword = txtMailPassword.Text;
                System.Net.Mail.MailMessage Mailmsg = new System.Net.Mail.MailMessage();
                Mailmsg.Subject = subject;
                Mailmsg.From = new MailAddress(sendMail);
                Mailmsg.To.Add(new MailAddress(receiveMail));
                string content = "如果您邮件客户端不支持HTML格式，请切换到“普通文本”视图，将看到此内容";
                Mailmsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, null, "text/plain"));
                body += "<br /><img src=\"cid:weblogo\">";   //注意此处嵌入的图片资源
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                LinkedResource lrImage = new LinkedResource(imagePath, "image/gif");
                //此处的ContentId 对应 htmlBodyContent 内容中的 cid: ，如果设置不正确，请不会显示图片
                lrImage.ContentId = "weblogo";
                htmlBody.LinkedResources.Add(lrImage);
                Mailmsg.AlternateViews.Add(htmlBody);
                if (filePath.Trim() != "")
                {
                    if (File.Exists(filePath))
                    {
                        Mailmsg.Attachments.Add(new Attachment(filePath));
                    }
                }
                System.Net.Mail.SmtpClient smtp = new SmtpClient(mailServer, mailServerPort);
                smtp.Credentials = new NetworkCredential(account, mailPassword);
                smtp.Send(Mailmsg);
                Mailmsg.Dispose();
                if (filePath.Trim() != "")
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                MessageBox.Show("邮件发送成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 选取路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            HBNiuBi.Controls.FolderBrowserDialog folderBrowserDialog1 = new HBNiuBi.Controls.FolderBrowserDialog();
            folderBrowserDialog1.DirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txtVideSavePath.Text = folderBrowserDialog1.DirectoryPath;
            }
        }
        /// <summary>
        /// 删除录像目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (Directory.Exists(txtVideSavePath.Text))
            {
                var dialog = MessageBox.Show(this, $"此操作将会删除目录{txtVideSavePath.Text}下所有文件！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    FileUtil.DeleteDirectory(txtVideSavePath.Text);
                }
            }
        }
    }
}

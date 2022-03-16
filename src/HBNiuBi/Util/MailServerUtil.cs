using HBNiuBi.Config;
using HBNiuBi.ScriptTask;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HBNiuBi.Util
{
    public class MailServerUtil
    {
        public static void SendMail(WarnReportModel warnReportModel, MailServerConfig mailServerConfig)
        {
            System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();
            var mail = mailServerConfig.SendMail;
            var account = mail.Split("@")[0];
            var sendMail = new MailAddress(mail);
            var receiveMail = new MailAddress(mailServerConfig.ReceiveMail);
            myMail = new System.Net.Mail.MailMessage(sendMail, receiveMail);
            myMail.Subject = warnReportModel.MailSubject;
            myMail.Body = warnReportModel.MailBody;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(mailServerConfig.MailServer, mailServerConfig.MailServerPort);
            client.Credentials = new System.Net.NetworkCredential(account, mailServerConfig.MailPassword);
            client.Send(myMail);
        }

        public static void SendMailImage(WarnReportModel warnReportModel, MailServerConfig mailServerConfig)
        {
            try
            {
                var subject = warnReportModel.MailSubject;
                var body = warnReportModel.MailBody;
                var sendMail = mailServerConfig.SendMail;
                var account = sendMail.Split("@")[0];
                var receiveMail = mailServerConfig.ReceiveMail;
                var mailServer = mailServerConfig.MailServer;
                var mailServerPort = Convert.ToInt32(mailServerConfig.MailServerPort);
                var mailPassword = mailServerConfig.MailPassword;
                System.Net.Mail.MailMessage Mailmsg = new System.Net.Mail.MailMessage();
                Mailmsg.Subject = subject;
                Mailmsg.From = new MailAddress(sendMail);
                Mailmsg.To.Add(new MailAddress(receiveMail));
                string content = "如果您邮件客户端不支持HTML格式，请切换到“普通文本”视图，将看到此内容";
                Mailmsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, null, "text/plain"));
                body += "<br /><img src=\"cid:weblogo\">";   //注意此处嵌入的图片资源
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                LinkedResource lrImage = new LinkedResource(warnReportModel.ImagePath, "image/gif");
                //此处的ContentId 对应 htmlBodyContent 内容中的 cid: ，如果设置不正确，请不会显示图片
                lrImage.ContentId = "weblogo";
                htmlBody.LinkedResources.Add(lrImage);
                Mailmsg.AlternateViews.Add(htmlBody);
                if (!string.IsNullOrWhiteSpace(warnReportModel.FilePath))
                {
                    if (File.Exists(warnReportModel.FilePath))
                    {
                        Mailmsg.Attachments.Add(new Attachment(warnReportModel.FilePath));
                    }
                }
                System.Net.Mail.SmtpClient smtp = new SmtpClient(mailServer, mailServerPort);
                smtp.Credentials = new NetworkCredential(account, mailPassword);
                smtp.Send(Mailmsg);
                Mailmsg.Dispose();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(warnReportModel.FilePath))
                {
                    if (File.Exists(warnReportModel.FilePath))
                    {
                        File.Delete(warnReportModel.FilePath);
                    }
                }
                if (!string.IsNullOrWhiteSpace(warnReportModel.ImagePath))
                {
                    if (File.Exists(warnReportModel.ImagePath))
                    {
                        File.Delete(warnReportModel.ImagePath);
                    }
                }
            }
        }
    }
}

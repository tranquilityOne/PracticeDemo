using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml;

namespace e3net.Common
{
    public class MsgService
    {

        /// <summary>
        /// 获取注册码验证
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <param name="msg_text">内容</param>
        /// <param name="code">国家区号</param>
        /// <returns></returns>
        public static bool SendPhoneCheckCodeMessage(string phone, string msg_text, string code)
        {
            int iret = 1;
            var msg = "";
            try
            {
                msg_text +="【ACA】";
                var text = string.Format("aj_sms_template".ToAppSetting(), msg_text);
                msg = System.Web.HttpUtility.UrlEncode(text, System.Text.Encoding.UTF8);
                String url = String.Format("http://api.accessyou.com/sms/sendsms-vercode.php?user={0}&msg={1}&phone={2}{3}&pwd={4}&accountno={5}", "precisionhk", msg, code, phone, "09310070", "11029897");
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                var response = req.GetResponse();
                var stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                String retXml = sr.ReadToEnd();
                XmlDocument document = new XmlDocument();
                document.LoadXml(retXml);
                var status = document.GetElementsByTagName("msg_status")[0].InnerText;
                if (status != "100")
                {
                    iret = int.Parse(status);
                }
            }
            catch (Exception ex)
            {
                iret = 0;
            }
            return iret>0;
        }


        /// <summary>
        /// 发送QQ邮箱验证
        /// </summary>
        /// <param name="to"></param>
        /// <param name="msg_text"></param>
        /// <returns></returns>
        //private static int SendQQEmail(String to, String msg_text)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendLine("亲爱的用户：<br/></br/>&nbsp;&nbsp;&nbsp;&nbsp;您好！<br /><br />");
        //        sb.AppendLine("&nbsp;&nbsp;&nbsp;感谢您使用" + SysConfig.email_company + "科技有限公司产品，您的验证码为：");
        //        sb.AppendLine(msg_text);
        //        sb.AppendLine("<br /><br />【" + SysConfig.email_company + "】");
        //        sb.AppendLine("<hr>");
        //        MailAddress from = new MailAddress(Common.SysConfig.email_from, SysConfig.email_name);
        //        MailMessage msg = new MailMessage();
        //        msg.From = from;
        //        msg.To.Add(to);
        //        msg.IsBodyHtml = true; //是否是HTML邮件 
        //        msg.SubjectEncoding = Encoding.UTF8;
        //        msg.BodyEncoding = Encoding.UTF8;
        //        msg.Priority = MailPriority.High; //邮件优先级
        //        msg.Subject = "验证码";  //邮件标题
        //        msg.Body = sb.ToString();  //邮件内容
        //        SmtpClient smtp = new SmtpClient(SysConfig.email_host);
        //        smtp.Host = SysConfig.email_host;  //Smtp服务器
        //        smtp.Port = SysConfig.email_port;    //端口号
        //        smtp.EnableSsl = false;//经过ssl加密
        //        smtp.Credentials = new NetworkCredential(Common.SysConfig.email_from, Common.SysConfig.email_from_pwd);

        //        //邮件发送
        //        smtp.Send(msg);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(typeof(HaoService).FullName, "SendQQEmail(String to, String msg_text) 发生错误：" + ex.Message);
        //        return 0;
        //    }
        //}

        //private static int SendEmailCheckCodeMessage(String phone, String msg_text)
        //{

        //    try
        //    {

        //        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        //        msg.To.Add(phone);

        //        msg.From = new MailAddress(SysConfig.email_from, SysConfig.email_company, System.Text.Encoding.UTF8);
        //        /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
        //        msg.Subject = "设备绑定验证码";//邮件标题 
        //        msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
        //        msg.Body = "尊敬的用户:<br/>&nbsp;&nbsp;&nbsp;&nbsp;您好!<br/>感谢您使用" + SysConfig.email_company + "产品，您的验证码为<u>" + msg_text + "</u><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;【" + SysConfig.email_company + "】";//邮件内容 
        //        msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
        //        msg.IsBodyHtml = true;//是否是HTML邮件 
        //        msg.Priority = MailPriority.Normal;//邮件优先级 

        //        SmtpClient client = new SmtpClient();
        //        client.Credentials = new System.Net.NetworkCredential(SysConfig.email_from, SysConfig.email_from_pwd);

        //        client.Host = SysConfig.email_host;
        //        client.Port = SysConfig.email_port;
        //        object userState = msg;
        //        client.Send(msg);
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(typeof(HaoService).FullName, "SendEmailCheckCodeMessage(String phone, String msg_text) 发生错误：" + ex.Message);
        //    }
        //    return 0;
        //}
    }
}

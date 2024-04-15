using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace www.otoparcabul.com.Models
{
    public static class MailHelper
    {
        private static readonly SmtpClient sc = new SmtpClient("mail.otoparcabul.net");

        public static void Init()
        {
            sc.Credentials = new NetworkCredential("bilgi@otoparcabul.net", "Opb1234");
        }

        public static void SendMail(MailMessage EMail)
        {
            try
            {
                sc.Send(EMail);
            }
            catch
            {
            }
        }

        public static void SendMail(string to, string subject, string body)
        {
            MailMessage eMail = new MailMessage("bilgi@otoparcabul.net", to, subject, body);
            eMail.IsBodyHtml = true;
            eMail.BodyEncoding = Encoding.UTF8;
            eMail.HeadersEncoding = Encoding.UTF8;
            eMail.SubjectEncoding = Encoding.UTF8;

            try
            {
                sc.Send(eMail);
            }
            catch (Exception ex)
            {
                OtoParcaBulEntities dc = new OtoParcaBulEntities();

                Log hata = new Log();

                hata.MemberID = 1;
                hata.Body = ex.Message;
                hata.Subject = "Send EMail";
                hata.CreatedDate = DateTime.Now;

                dc.Logs.Add(hata);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace DomainModel.Concrete.Helpers.Email
{
    public static class Email
    {
        private static readonly string SMTPServerUserName = "";
        private static readonly string SMTPServerPassword = "";

        public static void Send(EmailData data)
        {
            SmtpClient serv = new SmtpClient();
            MailMessage msg = new MailMessage();
            msg.To.Add(data.To);
            msg.Body = data.Body;
            msg.Subject = data.Subject;
            msg.BodyEncoding = System.Text.Encoding.ASCII;
            msg.IsBodyHtml = true;
            serv.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            serv.Credentials = new NetworkCredential(SMTPServerUserName,SMTPServerPassword);
            serv.Send(msg);
        }

    }
}

using System.Net.Mail;
using System.Net;

namespace Bookkeepify.Services
{
    public class EmailSender
    {
        public bool SendMail(string email, string message1)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("yourFrom@email.com");
            message.To.Add(email);
            message.Subject = "Confirm Your Email";
            message.IsBodyHtml = true;
            message.Body = "<a href='" + message1 + "'>Click here to confirm you email</a>";

            smtpClient.Port = 587;
            smtpClient.Host = "sandbox.smtp.mailtrap.io";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("b7a0fd8832541d", "d8a241e7e43198");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
            return true;
        }
    }
}

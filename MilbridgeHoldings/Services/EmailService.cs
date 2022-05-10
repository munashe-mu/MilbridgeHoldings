namespace MilbridgeHoldings.Services
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.Run(() =>
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(message.Destination);
                msg.From = new MailAddress(message.Destination, "Milbridge Holdings SA", System.Text.Encoding.UTF8);
                msg.Subject = message.Subject;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.Body = message.Body;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                SmtpClient client = new()
                {
                    Credentials = new System.Net.NetworkCredential("nyashagumbo0@gmail.com", "nyasha gumbo"),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false
                };

                try
                {
                    client.Send(msg);
                    return "Message Successfully Sent...";
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    string errorMessage = string.Empty;
                    while (ex2 != null)
                    {
                        errorMessage += ex2.ToString();
                        ex2 = ex2.InnerException;
                    }
                    return "Sending Failed...";
                }
            });
        }
    }
}

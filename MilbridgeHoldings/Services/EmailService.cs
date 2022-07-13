namespace MilbridgeHoldings.Services
{
    using Microsoft.AspNet.Identity;
    using MilbridgeHoldings.Models.Data.Local;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration) => _configuration = configuration;

        public Task<bool> SendAsync(EmailMessage email)
        {
            MailMessage emailMessage = new()
            {
                Sender = new MailAddress(_configuration["EmailService:UserName"], _configuration["EmailService:DisplayName"]),
                From = new MailAddress(_configuration["EmailService:Address"], _configuration["EmailService:DisplayName"]),
                IsBodyHtml = true,
                Subject = email.Subject,
                Body = email.Body,
                Priority = MailPriority.High,
            };

            emailMessage.To.Add(email.To!);

            using SmtpClient mailClient = new(_configuration["EmailService:Host"], int.Parse(_configuration["EmailService:Port"].ToString()));
            mailClient.Credentials = new NetworkCredential(_configuration["EmailService:Address"], _configuration["EmailService:Password"]);
            mailClient.EnableSsl = true;

            try
            {
                mailClient.Send(emailMessage);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}

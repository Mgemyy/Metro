using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetroApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Debug.WriteLine("==================================================");
            Debug.WriteLine($"[EMAIL SIMULATION] Sending To: {email}");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {htmlMessage}");
            Debug.WriteLine("==================================================");

            return Task.CompletedTask;
        }
    }
}

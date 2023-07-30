using Azure.Storage.Blobs.Models;
using BussinessLogic.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.Models;

namespace BussinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }

        public bool SendMail(List<TimeCapsuleEmail1> EmailReceivers)
        {

            foreach(var EmailReceiver in EmailReceivers)
            {
                try
                {
                    using (MimeMessage emailMessage = new MimeMessage())
                    {
                        MailboxAddress emailFrom = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
                        emailMessage.From.Add(emailFrom);
                        MailboxAddress emailTo = new MailboxAddress(EmailReceiver.Email, EmailReceiver.Email);
                        emailMessage.To.Add(emailTo);
                        emailMessage.Subject = "Message from past...";
                        BodyBuilder emailBodyBuilder = new BodyBuilder();
                        emailBodyBuilder.TextBody = "Hi in {submit date} {someone} submitted Time Capsule with you! now it is sealed by link you can see what {someone} wanted to sahre with you \n link:https://www.youtube.com/watch?v=dQw4w9WgXcQ ";
                        emailMessage.Body = emailBodyBuilder.ToMessageBody();
                        using (SmtpClient mailClient = new SmtpClient())
                        {
                            mailClient.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                            mailClient.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                            mailClient.Send(emailMessage);
                            mailClient.Disconnect(true);
                        }
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

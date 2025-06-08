using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Email.Interfaces;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Email.Models;
using CleanArchitectureTemplate.Infrastructure.Email.Smtp.Configurations;

namespace CleanArchitectureTemplate.Infrastructure.Email.Smtp.Services
{
    public class SmtpService : IEmailService
    {
        private readonly SmtpConfig config;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SmtpService(SmtpConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Send email through smtp service
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(EmailReceiver receiver, string subject, string body)
        {
            ArgumentNullException.ThrowIfNull(receiver);

            MailAddress To = new(receiver.Email, "Receiver");
            MailAddress From = new(config.EmailAddressFrom, "Sender");

            var emailMessage = new MailMessage(From, To)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
            };

            var client = new SmtpClient
            {
                EnableSsl = config.EnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Port = config.EmailSmtpPort,
                Host = config.EmailSmtpHost,
            };

            if (!string.IsNullOrEmpty(config.EmailAddressFrom) && !string.IsNullOrEmpty(config.EmailFromPassword))
                client.Credentials = new NetworkCredential(config.EmailAddressFrom, config.EmailFromPassword);
            else
                client.UseDefaultCredentials = true;

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);

            try
            {
                await client.SendMailAsync(emailMessage);
                return true;

            }
            catch (SmtpException)
            {
                throw;
            }
            finally
            {
                emailMessage.Dispose();
            }
        }

        #region Private Methods

        private bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return config.IgnoreSslErrors || policyErrors == SslPolicyErrors.None;
        }

        #endregion
    }
}

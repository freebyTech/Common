
using System;
using System.Collections.Specialized;
using freebyTech.Common.Interfaces;
using freebyTech.Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace freebyTech.Common.Messaging.Mail
{
    /// <summary>
    /// This provider provides simple SMTP mail services for freebyTech applications defined by the IMailProvider interface.
    /// </summary>
    /// <example>
    /// This class can be used in configuration startup like this:
    /// <code>
    /// services.Configure<SmtpMailOptions>(Configuration.GetSection("MAIL"));
    /// </code>
    /// and used via dependency injection like this:
    /// <code>
    /// public ClassName(IMailProvider mailProvider) { }
    /// </code>
    /// </example>
    public class SmtpMailProvider : IMailProvider
    {
        SmtpMailOptions _mailOptions { get; }
        ILogger _logger { get; set; }
        public SmtpMailProvider(IOptionsMonitor<SmtpMailOptions> mailOptionsAccessor, ILogger<SmtpMailProvider> logger) {
            _mailOptions = mailOptionsAccessor.CurrentValue;
            _logger = logger;
        }

        public void SendMail(string subject, string receivers, string messageBody, bool isHtmlBody)
        {
            LogSend(subject, receivers);
            SmtpMailService.SendMail(subject, receivers, _mailOptions.SmtpUserName, messageBody, isHtmlBody, _mailOptions.SmtpServer, _mailOptions.SmtpPort, _mailOptions.SmtpUserName, _mailOptions.SmtpPassword, _mailOptions.SmtpEnableSSL);
        }

        public void SendMail(string subject, string receivers, string htmlTemplate, NameValueCollection expansionValues, bool isHtmlBody)
        {
            LogSend(subject, receivers);
            SmtpMailService.SendMail(subject, receivers, _mailOptions.SmtpUserName, htmlTemplate, expansionValues, isHtmlBody, _mailOptions.SmtpServer, _mailOptions.SmtpPort, _mailOptions.SmtpUserName, _mailOptions.SmtpPassword, _mailOptions.SmtpEnableSSL);
        }

        private void LogSend(string subject, string receivers)
        {
            _logger?.LogInformation($"Attempting to send a message with subject '{subject}' from '{_mailOptions?.SmtpUserName}' to '{receivers}' via server '{_mailOptions?.SmtpServer}' on port '{_mailOptions?.SmtpPort} with user '{_mailOptions?.SmtpUserName} SmtpEnableSSL = {_mailOptions?.SmtpEnableSSL}.");
        }
    }
}

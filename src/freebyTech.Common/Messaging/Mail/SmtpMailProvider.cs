
using System.Collections.Specialized;
using freebyTech.Common.Interfaces;
using freebyTech.Common.Options;
using Microsoft.Extensions.Options;

namespace freebyTech.Common.Messaging.Mail
{
    /// <summary>
    /// This provider provides simple SMTP mail services for freebyTech applications defined by the IMailProvider interface.
    /// </summary>
    /// <example>
    /// This class can be used in configuration startup like this:
    /// <code>
    /// services.Configure<MailOptions>(Configuration.GetSection("mail"));
    /// </code>
    /// and used via dependency injection like this:
    /// <code>
    /// public ClassName(IMailProvider mailProvider) { }
    /// </code>
    /// </example>
    public class SmtpMailProvider : IMailProvider
    {
        SmtpMailOptions _mailOptions { get; }
        public SmtpMailProvider(IOptionsMonitor<SmtpMailOptions> mailOptionsAccessor) {
            _mailOptions = mailOptionsAccessor.CurrentValue;
        }

        public void SendMail(string subject, string receivers, string from, string messageBody, bool isHtmlBody)
        {
            SmtpMailService.SendMail(subject, receivers, from, messageBody, isHtmlBody, _mailOptions.SmtpServer, _mailOptions.SmtpPort, _mailOptions.SmtpUserName, _mailOptions.SmtpPassword, _mailOptions.SmtpEnableSSL);
        }

        public void SendMail(string subject, string receivers, string from, string htmlTemplate, NameValueCollection expansionValues, bool isHtmlBody)
        {
            SmtpMailService.SendMail(subject, receivers, from, htmlTemplate, expansionValues, isHtmlBody, _mailOptions.SmtpServer, _mailOptions.SmtpPort, _mailOptions.SmtpUserName, _mailOptions.SmtpPassword, _mailOptions.SmtpEnableSSL);
        }
    }
}

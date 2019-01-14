using System.Collections.Specialized;

namespace freebyTech.Common.Interfaces
{
    public interface IMailProvider
    {
        void SendMail(string subject, string receivers, string from, string messageBody, bool isHtmlBody);

        void SendMail(string subject, string receivers, string from, string htmlTemplate, NameValueCollection expansionValues, bool isHtmlBody);
    }
}


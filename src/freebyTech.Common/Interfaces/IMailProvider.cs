using System.Collections.Specialized;

namespace freebyTech.Common.Interfaces
{
    public interface IMailProvider
    {
        void SendMail(string subject, string receivers, string messageBody, bool isHtmlBody);

        void SendMail(string subject, string receivers, string htmlTemplate, NameValueCollection expansionValues, bool isHtmlBody);
    }
}


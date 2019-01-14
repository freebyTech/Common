using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Collections.Specialized;

namespace freebyTech.Common.Messaging.Mail
{
    internal class SmtpMailService
    {
        public static void SendMail(string subject, string receivers, string from, string messageBody, bool isHtmlBody, string smtpServer, int port, string userName, string password, bool enableSSL)
        {
            var message = new MailMessage();
            var rec = receivers.Replace(";", ",");
            message.To.Add(rec);
            message.Subject = subject;
            message.From = new MailAddress(from);
            message.IsBodyHtml = isHtmlBody;
            message.Body = messageBody;
            var smtpClient = new SmtpClient(smtpServer, port);
            var networkCred = new NetworkCredential(userName, password);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = networkCred;
            smtpClient.EnableSsl = enableSSL;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }

        public static void SendMail(string subject, string receivers, string from, string htmlTemplate, string expansionArray, bool isHtmlBody, string smtpServer, int port, string userName, string password, bool enableSSL)
        {
            var expansionValues = ExpandCollection(expansionArray);
            SendMail(subject, receivers, from, htmlTemplate, expansionValues, isHtmlBody, smtpServer, port, userName, password, enableSSL);
        }

        public static void SendMail(string subject, string receivers, string from, string htmlTemplate, NameValueCollection expansionValues, bool isHtmlBody, string smtpServer, int port, string userName, string password, bool enableSSL)
        {
            var messageBody = BuildHtmlMessage(htmlTemplate, expansionValues);
            SendMail(subject, receivers, from, messageBody, isHtmlBody, smtpServer, port, userName, password, enableSSL);
        }

        public static NameValueCollection ExpandCollection(string expansionArray)
        {
            var keywordsCollection = new NameValueCollection();

            var keywords = expansionArray.Split('|');
            foreach (var namevalue in keywords.Select(keyvalue => keyvalue.Split('^')).Where(namevalue => namevalue.Count() == 2))
            {
                keywordsCollection.Add(namevalue[0], namevalue[1]);
            }
            return keywordsCollection;
        }

        public static string BuildHtmlMessage(string htmlTemplate, NameValueCollection keywordsCollection)
        {
            return keywordsCollection.Count == 0 ? (htmlTemplate) : (ConvertKeywordsToValue(htmlTemplate, keywordsCollection));
        }

        public static string ConvertKeywordsToValue(string unconvertedValue, NameValueCollection keywordsCollection)
        {
            var convertedValue = unconvertedValue;

            for (var inc = 0; inc < keywordsCollection.Count; inc++)
            {
                convertedValue = convertedValue.Replace("@" + keywordsCollection.Keys[inc] + "@", keywordsCollection[inc]);
            }

            return (convertedValue);
        }
    }
}

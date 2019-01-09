using System.IO;
using System.Net;
using System.Text;
using freebyTech.Common.Messaging.Slack.Model;
using Newtonsoft.Json;

namespace freebyTech.Common.Messaging.Slack
{
    public class SlackMessenger
    {
        public string ApiBaseUrl { get; private set; }

        public SlackMessenger()
        {
            ApiBaseUrl = "https://slack.com/api";
        }

        public SlackMessenger(string apiBaseUrl)
        {
            ApiBaseUrl = apiBaseUrl;
        }

        public SlackResponse PostMessage(SlackMessage message)
        {
            var httpWebRequest = HttpWebRequest.Create(string.Format("{0}/chat.postMessage", ApiBaseUrl));
            SlackResponse response = null;

            // At the moment, slack does not support json encoded bodies in messages WHICH IS REALLY REALLY CRAZY, everything has to be URI encoded. Who knew!
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    var json = JsonConvert.SerializeObject(message);

            //    streamWriter.Write(json);
            //    streamWriter.Flush();
            //}

            ASCIIEncoding ascii = new ASCIIEncoding();
            var messageEncoded = message.EncodeAsURI();
            byte[] postBytes = ascii.GetBytes(messageEncoded);

            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.ContentLength = postBytes.Length;
            using (var stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                response = JsonConvert.DeserializeObject<SlackResponse>(result);
            }
            return response;
        }
    }
}

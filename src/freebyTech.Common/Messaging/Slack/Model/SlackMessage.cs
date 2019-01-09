using System.Collections.Generic;

namespace freebyTech.Common.Messaging.Slack.Model
{
    public class SlackMessage
    {
        public SlackMessage() {  attachments = new List<Attachment>(); }

        public string token { get; set; }
        public string channel { get; set; }
        public string text { get; set; }
        public string parse { get; set; }
        public string link_names { get; set; }
        public bool? unfurl_links { get; set; }
        public bool? unfurl_media { get; set; }
        public string username { get; set; }
        public bool? as_user { get; set; }
        public string icon_url { get; set; }
        public string icon_emoji { get; set; }
        public List<Attachment> attachments { get; set; }
    }    
}

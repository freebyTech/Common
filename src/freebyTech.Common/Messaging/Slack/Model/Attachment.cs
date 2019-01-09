using System.Collections.Generic;

namespace freebyTech.Common.Messaging.Slack.Model
{
  /// <summary>
  /// See https://api.slack.com/docs/attachments for more information concerning this model definition.
  /// </summary>
  public class Attachment
    {
        public Attachment()
        {
            fields = new List<Fields>();
        }

        public string fallback { get; set; }

        public string color { get; set; }
        
        public string pretext { get; set; }

        public string title { get; set; }

        public string title_link { get; set; }

        public string text { get; set; }

        public List<Fields> fields { get; set; }

    }
}

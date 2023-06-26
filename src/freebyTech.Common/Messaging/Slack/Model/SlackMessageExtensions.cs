using System;
using System.Text;
using System.Text.Json;
using freebyTech.Common.ExtensionMethods;

namespace freebyTech.Common.Messaging.Slack.Model
{
  public static class SlackMessageExtensions
  {
    public static string EncodeAsURI(this SlackMessage message)
    {
      var sb = new StringBuilder();
      if (message.token.IsNullOrEmpty())
      {
        throw new ArgumentException("message.token cannot be null or empty, message will not be accepted by Slack.");
      }
      if (message.channel.IsNullOrEmpty())
      {
        throw new ArgumentException("message.channel cannot be null or empty, message will not be accepted by Slack.");
      }

      sb.AppendUriEncoded(nameof(message.token), message.token);
      sb.AppendUriEncoded(nameof(message.channel), message.channel);

      if (!message.text.IsNullOrEmpty())
      {
        sb.AppendUriEncoded(nameof(message.text), message.text);
      }
      if (!message.parse.IsNullOrEmpty())
      {
        sb.AppendUriEncoded(nameof(message.parse), message.parse);
      }
      if (!message.link_names.IsNullOrEmpty())
      {
        sb.AppendUriEncoded(nameof(message.link_names), message.link_names);
      }
      if (message.unfurl_links.HasValue)
      {
        sb.AppendUriEncoded(nameof(message.unfurl_links), message.unfurl_links.Value.ToString());
      }
      if (message.unfurl_media.HasValue)
      {
        sb.AppendUriEncoded(nameof(message.unfurl_media), message.unfurl_media.Value.ToString());
      }
      if (!message.username.IsNullOrEmpty())
      {
        sb.AppendUriEncoded(nameof(message.username), message.username);
      }
      if (message.as_user.HasValue)
      {
        sb.AppendUriEncoded(nameof(message.as_user), message.as_user.Value.ToString());
      }
      if (!message.icon_url.IsNullOrEmpty())
      {
        sb.AppendUriEncoded(nameof(message.icon_url), message.icon_url);
      }
      if (!message.icon_emoji.IsNullOrEmpty())
      {
        sb.AppendUriEncoded(nameof(message.icon_emoji), message.icon_emoji);
      }
      if (message.attachments.Count > 0)
      {
        var attachmentsJson = JsonSerializer.Serialize(message.attachments);
        sb.AppendUriEncoded(nameof(message.attachments), attachmentsJson);
      }

      return sb.ToString();
    }
  }
}

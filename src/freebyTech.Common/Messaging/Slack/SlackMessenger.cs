using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using freebyTech.Common.Messaging.Slack.Model;

namespace freebyTech.Common.Messaging.Slack
{
  public class SlackMessenger
  {
    private static readonly HttpClient _httpClient = new HttpClient();

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
      // At the moment, Slack does not support JSON-encoded bodies in messages; everything has to be
      // URI (form) encoded and posted as application/x-www-form-urlencoded.
      var messageEncoded = message.EncodeAsURI();
      using var content = new StringContent(messageEncoded, Encoding.ASCII);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

      using var httpResponse = _httpClient
        .PostAsync($"{ApiBaseUrl}/chat.postMessage", content)
        .GetAwaiter()
        .GetResult();

      var result = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
      return JsonSerializer.Deserialize<SlackResponse>(result)!;
    }
  }
}

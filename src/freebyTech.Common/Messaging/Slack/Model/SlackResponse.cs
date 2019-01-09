namespace freebyTech.Common.Messaging.Slack.Model
{
    public class SlackResponse
    {
        public bool ok { get; set; }
        public string ts { get; set; }
        public string channel { get; set; }
        public string error { get; set; }
        public string warning { get; set; }
        public SlackMessage message { get; set; }
    }
}

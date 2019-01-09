using freebyUtil.Messaging.Slack;
using freebyUtil.Messaging.Slack.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace freebyUtil.Messaging.Tests
{
  public class TestSlackMessaging
  {
    private string testOAuthToken = "need-to-get-a-freebytech-oauth-token";

    [Test]
    public void TestSendOfGoodMessage()
    {
      var message = new SlackMessage
      {
        token = testOAuthToken,
        channel = "#unittesting",
        as_user = false,
        username = "NUnit",
        attachments = new List<Attachment>() { new Attachment()
            {
                fallback = "Test Good Message!",
                color = "good",
                pretext = @"This is ""Good"" message from NUnit! Yeah!",
                title = "Go all slack happy!",
                title_link = "https://api.slack.com/",
                text = "Optional text that appears within the attachment area.",
            }}
      };
      var messenger = new SlackMessenger();

      var response = messenger.PostMessage(message);
      Assert.IsTrue(response.ok);
    }

    [Test]
    public void TestSendOfWarningMessage()
    {
      var message = new SlackMessage
      {
        token = testOAuthToken,
        channel = "#unittesting",
        as_user = false,
        username = "NUnit",
        attachments = new List<Attachment>() { new Attachment()
            {
                fallback = "Test Warning Message!",
                color = "warning",
                pretext = @"This is ""Warning"" message from NUnit! Potentially bad!",
                title = "Go all slack happy!",
                title_link = "https://api.slack.com/",
                text = "Optional text that appears within the attachment area.",
            }}
      };
      var messenger = new SlackMessenger();

      var response = messenger.PostMessage(message);
      Assert.IsTrue(response.ok);
    }

    [Test]
    public void TestSendOfErrorMessage()
    {
      var message = new SlackMessage
      {
        token = testOAuthToken,
        channel = "#unittesting",
        as_user = false,
        username = "NUnit",
        attachments = new List<Attachment>() { new Attachment()
            {
                fallback = "Test Danger Message!",
                color = "danger",
                pretext = @"This is ""Danger"" message from NUnit! Ruh Roh!",
                title = "Go all slack happy!",
                title_link = "https://api.slack.com/",
                text = "Optional text that appears within the attachment area.",
            }}
      };
      var messenger = new SlackMessenger();

      var response = messenger.PostMessage(message);
      Assert.IsTrue(response.ok);
    }

    [Test]
    public void TestOfBuildStartedMessage()
    {
      var message = new SlackMessage
      {
        token = testOAuthToken,
        channel = "#unittesting",
        as_user = false,
        username = "BuildStarted",
        icon_emoji = ":timer_clock:",
        attachments = new List<Attachment>() { new Attachment()
            {
                fallback = @"freebyUtil Messaging Service Build Operation Started",
                pretext = @"Build Operation Started",
                fields = new List<Fields>()
                {
                    new Fields() { @short = true, title = "Project Name", value = "freebyUtil Messaging Service" },
                    new Fields() { @short = true, title = "Build #", value = "1" },
                    new Fields() { @short = true, title = "Jenkins", value = "<http://build01.serenity.dom:8080/job/Build%20freebyUtil%20Messaging%20Service/1/console|View Live Build Console>" },
                    new Fields() { @short = true, title = "BitBucket", value = "<https://bitbucket.org/temporafugiunt/freebyutil/overview|View Repository>" }
                } }}
      };
      var messenger = new SlackMessenger();

      var response = messenger.PostMessage(message);
      Assert.IsTrue(response.ok);
    }
    

    [Test]
    public void TestOfBuildFailedMessage()
    {
      var message = new SlackMessage
      {
        token = testOAuthToken,
        channel = "#unittesting",
        as_user = false,
        username = "BuildFailed",
        icon_emoji = ":thunder_cloud_and_rain:",
        attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        fallback = @"freebyUtil Messaging Service Build Operation Failed!",
                        pretext = @"<!everyone>, The Build Operation Failed!",
                        color = "danger",
                        fields = new List<Fields>()
                        {
                            new Fields() { @short = true, title = "Project Name", value = "freebyUtil Messaging Service" },
                            new Fields() { @short = true, title = "Build #", value = "1" },
                            new Fields() { @short = true, title = "Jenkins", value = "<http://build01.serenity.dom:8080/job/Build%20freebyUtil%20Messaging%20Service/1/console|View Live Build Console>" },
                            new Fields() { @short = true, title = "BitBucket", value = "<https://bitbucket.org/temporafugiunt/freebyutil/commits/1ad4a4ba3115f0b54111696374b8580862c9bf6e|View Last Commit>"}

                        }
                    }
                }
      };
      var messenger = new SlackMessenger();

      var response = messenger.PostMessage(message);
      Assert.IsTrue(response.ok);
    }

    [Test]
    public void TestOfBuildCompletedMessage()
    {
      var message = new SlackMessage
      {
        token = testOAuthToken,
        channel = "#unittesting",
        as_user = false,
        username = "BuildCompleted",
        icon_emoji = ":sunny:",
        attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        pretext = @"Build Operation Success!",
                        color = "good",
                        fields = new List<Fields>()
                        {
                            new Fields() { @short = true, title = "Project Name", value = "freebyUtil Messaging Service" },
                            new Fields() { @short = true, title = "Version #", value = "1.0.1.0204" },
                            new Fields() { @short = true, title = "Jenkins", value = "<http://build01.serenity.dom:8080/job/Build%20freebyUtil%20Messaging%20Service/1/consoleFull|View Build Output>"},
                            new Fields() { @short = true, title = "BitBucket", value = "<https://bitbucket.org/temporafugiunt/freebyutil/overview|View Repository>"}
                        }
                    }
                }
      };
      var messenger = new SlackMessenger();

      var response = messenger.PostMessage(message);
      Assert.IsTrue(response.ok);
    }
  }
}

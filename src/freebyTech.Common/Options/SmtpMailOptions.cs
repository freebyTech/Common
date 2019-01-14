namespace freebyTech.Common.Options
{
    /// <summary>
    /// This configuration options class contains the configuration items necessary for SMTP Server communication. It works in conjuction with Kubernetes environment secrets
    /// to populate these settings. 
    /// </summary>
    /// <example>
    /// This class can be used in configuration startup like this:
    /// <code>
    /// services.Configure<MailOptions>(Configuration.GetSection("mail"));
    /// </code>
    /// and used via dependency injection like this:
    /// <code>
    /// public ClassName(IOptionsSnapshot<MailOptions> mailOptions) { }
    /// </code>
    /// </example>
    public class SmtpMailOptions {

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUserName { get; set; }

        public string SmtpPassword { get; set; }

        public bool SmtpEnableSSL { get; set; } = true;
    }    
}
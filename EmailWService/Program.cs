using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System.ServiceProcess;

namespace EmailWService
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main method, which is the entry point of the application.
        /// </summary>
        private static void Main()
        {
            // Array to hold the services to run
            ServiceBase[] ServicesToRun;

            // Create a new instance of the ServiceCollection
            IServiceCollection services = new ServiceCollection();

            // Configuration for the email service
            var config = new
            {
                Port = 465,                      // SMTP server port
                From = "ownemail@gmail.com",     // Email address to send from
                Password = "Get App Password",   // Email account password
                SmtpServer = "smtp.gmail.com",   // SMTP server address
                UserName = "ownemail@gmail.com"  // Email account username
            };

            // Register the configuration in the service collection
            services.AddSingleton(config);

            // Add and configure the MailKit email service
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions
                {
                    // SMTP server settings
                    Server = config.SmtpServer,
                    Port = config.Port,
                    SenderName = config.UserName,
                    SenderEmail = config.From,

                    // Authentication details (optional if no authentication is required)
                    Account = config.From,
                    Password = config.Password,

                    // Enable SSL or TLS for secure email sending
                    Security = true
                });
            });

            // Build the service provider from the service collection
            var sp = services.BuildServiceProvider();

            // Initialize the array with the email service
            ServicesToRun = new ServiceBase[]
            {
                new DrMadWillEmailService(sp)
            };

            // Run the services
            ServiceBase.Run(ServicesToRun);
        }
    }
}
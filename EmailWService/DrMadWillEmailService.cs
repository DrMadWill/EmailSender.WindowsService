using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace EmailWService
{
    /// <summary>
    /// DrMadWillEmailService class handles the Windows service for sending emails periodically.
    /// </summary>
    public partial class DrMadWillEmailService : ServiceBase
    {
        // Timer to trigger email sending at intervals
        private Timer timer = new Timer();
        // List of email addresses to which the emails will be sent
        private List<string> emailList = new List<string> { "someone@gmail.com" };
        // Email service to send emails
        private readonly IEmailService _sysEmailService;

        /// <summary>
        /// Constructor for the DrMadWillEmailService class.
        /// </summary>
        /// <param name="serviceProvider">Service provider to get email service instance</param>
        public DrMadWillEmailService(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _sysEmailService = serviceProvider.GetService<IEmailService>();
        }

        /// <summary>
        /// Method called when the service starts.
        /// </summary>
        /// <param name="args">Arguments passed to the service</param>
        protected override void OnStart(string[] args)
        {
            // Write a log indicating that the service has started
            WriteLog("Start Service...");
            // Attach the Elapsed event handler to the timer
            timer.Elapsed += new ElapsedEventHandler(OnElapsed);
            // Set the timer interval to 5000 milliseconds (5 seconds)
            timer.Interval = 5000;
            // Enable the timer
            timer.Enabled = true;
        }


        /// <summary>
        /// Method called when the service stops.
        /// </summary>
        protected override void OnStop()
        {
            // Write a log indicating that the service has stopped
            WriteLog("End Service...");
        }


        /// <summary>
        /// Event handler for the timer's Elapsed event. Called at each timer interval.
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Elapsed event data</param>
        public void OnElapsed(object sender, ElapsedEventArgs e)
        {
            // Write a log indicating that the timer elapsed event has occurred
            WriteLog("Work");
            // Call the method to send emails
            SendEmail();
        }


        /// <summary>
        /// Method to send emails to all addresses in the email list.
        /// </summary>
        public void SendEmail()
        {
            // Loop through each email address in the email list
            foreach (var email in emailList)
            {
                // Write a log indicating that an email is being sent
                WriteLog($" {email} | Email Sending ... ");
                try
                {
                    // Simulate some work with a delay
                    System.Threading.Thread.Sleep(1000);
                    // Send the email using the email service
                    _sysEmailService.Send(email, "Auto Service", "Windows Service Send Message", isHtml: false);
                    // Write a log indicating that the email was sent successfully
                    WriteLog($" {email} | Email Sent.");
                }
                catch (Exception ex)
                {
                    // Write a log indicating that there was an error sending the email
                    WriteLog($" {email} | Email Sending Error | err {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Method to write messages to a log file.
        /// </summary>
        /// <param name="log">Message to be logged</param>
        public void WriteLog(string log)
        {
            // Prepend the current time to the log message
            log = $"Time : {DateTime.UtcNow.AddHours(4)} | " + log;
            // Directory to store log files
            string folderDir = AppDomain.CurrentDomain.BaseDirectory + "/Logs";
            // Create the directory if it does not exist
            if (!Directory.Exists(folderDir)) Directory.CreateDirectory(folderDir);

            // File path for the log file
            string fileDir = folderDir + "/" + "log.txt";
            // Append to the log file if it exists, otherwise create a new file
            if (File.Exists(fileDir))
            {
                using (StreamWriter sw = File.AppendText(fileDir))
                    sw.WriteLine(log);
            }
            else
            {
                using (StreamWriter sw = File.CreateText(fileDir))
                    sw.WriteLine(log);
            }
        }
    }
}
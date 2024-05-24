## Overview

`DrMadWillEmailService` is a Windows service designed to periodically send emails to a predefined list of email addresses. The service logs its activity, including when it starts, stops, and sends emails.

## Features

-   Periodically sends emails to a list of recipients.
-   Logs all activities to a log file.
-   Configurable timer interval for sending emails.
-   Handles exceptions and logs errors during the email sending process.

## Prerequisites

-   .NET Framework (version compatible with your development environment)
-   Visual Studio or any other C# development environment
-   A compatible email service implementing `IEmailService`

## Installation

### 1. Clone the Repository

Clone the repository to your local machine:


```bash
git clone https://github.com/DrMadWill/EmailSender.WindowsService.git
``` 

### 2. Open in Visual Studio

Open the solution file (`.sln`) in Visual Studio.

### 3. Register IEmailService

Register your implementation of `IEmailService` in the service provider. For example, using Dependency Injection in .NET Core:

```cs

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

``` 





### 4. Implement IEmailService

Ensure you have an implementation of the `IEmailService` interface. This service will be used to send emails. Your implementation might look something like this:

```cs
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

``` 

### 5. Install the Service

To install the Windows service, open the Developer Command Prompt for Visual Studio and navigate to the project directory. Then run the following command:

Go to dir
```bash
    cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
``` 

Run bash script

```bash
    InstallUtil.exe C:\ProjectLocation\EmailWService\bin\Debug\EmailWService.exe
``` 

### 6. Start the Service

Start the service using the Service Control Manager (SCM):

![start](/Start.png)

![stop](/Stop.png)

## Usage

### OnStart

The `OnStart` method is called when the service starts. It initializes the timer and starts the logging process.

### OnStop

The `OnStop` method is called when the service stops. It logs the stop event.

### OnElapsed

The `OnElapsed` method is the event handler for the timer's `Elapsed` event. It logs the event and calls the `SendEmail` method.

### SendEmail

The `SendEmail` method sends emails to all addresses in the `emailList`. It logs the start and completion of each email sending process and handles any exceptions that occur.

### WriteLog

The `WriteLog` method writes messages to a log file located in the `Logs` directory under the application's base directory. It creates the directory and file if they do not exist.

## Configuration

### Timer Interval

The timer interval is set to 5000 milliseconds (5 seconds) in the `OnStart` method. You can change this interval as needed:



```cs
timer.Interval = 10000; // Set to 10 seconds
``` 

### Email List

The list of email addresses is hard-coded in the `emailList` field. You can modify this list or load it from a configuration file or database as needed.

csharp

Kodu kopyala

```cs
private List<string> emailList = new List<string> { "example1@gmail.com", "example2@gmail.com" };
``` 

## Logging

Log messages include timestamps and are stored in the `Logs` directory. Each log entry is appended to the `log.txt` file. The logging mechanism ensures that the log file is created if it does not exist.

## Exception Handling

The `SendEmail` method includes a try-catch block to handle exceptions during the email sending process. Errors are logged with the corresponding exception message.

## Uninstallation

To uninstall the service, use the Service Control Manager:



Go to dir
```bash
    cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
``` 

Run bash script

```bash
    InstallUtil.exe -u C:\ProjectLocation\EmailWService\bin\Debug\EmailWService.exe
```  

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any improvements.

## License

This project is licensed under the MIT License.

## Contact

For any questions or issues, please contact nofelsalahov9@gmail.com.
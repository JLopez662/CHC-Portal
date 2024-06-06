using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BLL.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("CPA Portal", "cpatest1234@outlook.com"));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html")
        {
            Text = message
        };

        using var client = new SmtpClient();
        try
        {
            _logger.LogInformation("Connecting to SMTP server smtp.office365.com on port 587...");
            await client.ConnectAsync("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            _logger.LogInformation("Connected to SMTP server. Authenticating...");
            await client.AuthenticateAsync("cpatest1234@outlook.com", "123Uchihazyx12");
            _logger.LogInformation("Authenticated. Sending email to {Email}...", toEmail);
            await client.SendAsync(emailMessage);
            _logger.LogInformation("Email sent successfully to {Email}.", toEmail);
        }
        catch (SmtpCommandException ex)
        {
            _logger.LogError("SMTP command error: {Message}", ex.Message);
            throw;
        }
        catch (MailKit.Security.AuthenticationException ex)
        {
            _logger.LogError("Authentication error: {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("An unexpected error occurred while sending email: {Exception}", ex);
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}

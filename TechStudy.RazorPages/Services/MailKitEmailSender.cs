using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using TechStudy.RazorPages.Helpers.Options;
using MailKit.Net.Smtp;

namespace TechStudy.RazorPages.Services;

public class MailKitEmailSender : IEmailSender
{
    private readonly IOptions<EmailOption> _emailOption;
    private readonly ILogger<MailKitEmailSender> _logger;

    public MailKitEmailSender(IOptions<EmailOption> emailOption, ILogger<MailKitEmailSender> logger)
    {
        _emailOption = emailOption;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromInfo = _emailOption.Value;
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(fromInfo.Email, fromInfo.Email));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;

        message.Body = new TextPart(TextFormat.Html)
        {
            Text = htmlMessage
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(fromInfo.SMPTPServer, 465, true);

        await client.AuthenticateAsync(fromInfo.Email, fromInfo.Password);
        _logger.LogInformation("Sending message to {Email}", email);
        var res = await client.SendAsync(message);
        await client.DisconnectAsync(true);


    }
}

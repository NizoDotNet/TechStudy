﻿using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using TechStudy.Options;
using MimeKit.Text;
namespace TechStudy.Services;

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
        await client.ConnectAsync("smtp.gmail.com", 465, true);

        await client.AuthenticateAsync(fromInfo.Email, fromInfo.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

    }
}

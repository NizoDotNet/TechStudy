using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using TechStudy.Options;

namespace TechStudy.Services;

public class EmailSender : IEmailSender
{
    private IOptions<EmailOption> _emailOptions;

    public EmailSender(IOptions<EmailOption> emailOptions)
    {
        _emailOptions = emailOptions;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var e = _emailOptions.Value;
        SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 465,
        };

        // set smtp-client with basicAuthentication
        mySmtpClient.UseDefaultCredentials = false;

        System.Net.NetworkCredential basicAuthenticationInfo = new
           System.Net.NetworkCredential(e.Email, e.Password);
        mySmtpClient.Credentials = basicAuthenticationInfo;

        // add from,to mailaddresses
        MailAddress from = new MailAddress(e.Email, "TestFromName");
        MailAddress to = new MailAddress(email, "TestToName");
        MailMessage myMail = new MailMessage(from, to);

        // set subject and encoding
        myMail.Subject = subject;
        myMail.SubjectEncoding = System.Text.Encoding.UTF8;

        // set body-message and encoding
        myMail.Body = htmlMessage;
        myMail.BodyEncoding = System.Text.Encoding.UTF8;
        // text or html
        myMail.IsBodyHtml = true;

        mySmtpClient.Send(myMail);
    }
}

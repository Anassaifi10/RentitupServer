using Microsoft.Extensions.Options;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using Azure;

namespace RentalApp.Infrastructure.Services;
public class SendGridEmailServices : IEmailSenderServices
{
    private readonly SendGridSettings _settings;

    public SendGridEmailServices(IOptions<SendGridSettings> options)
    {
        _settings = options.Value;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var client = new SendGridClient(_settings.ApiKey);
            var from = new EmailAddress(_settings.SenderEmail, _settings.SenderName);
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlMessage);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send email: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to send email: {ex.Message}");
        }
    }
}

namespace RentalApp.Application.Common.Interfaces;
public interface IEmailSenderServices
{        Task SendEmailAsync(string email, string subject, string htmlMessage);
}

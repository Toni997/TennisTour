using TennisTour.Application.Common.Email;

namespace TennisTour.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}

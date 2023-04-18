using System.Threading.Tasks;
using TennisTour.Application.Common.Email;
using TennisTour.Application.Services;

namespace TennisTour.Api.IntegrationTests.Helpers.Services;

public class EmailTestService : IEmailService
{
    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        await Task.Delay(100);
    }
}

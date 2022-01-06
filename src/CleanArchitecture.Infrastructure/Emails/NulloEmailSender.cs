using CleanArchitecture.Application.Common.Emails;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Emails;

public class NulloEmailSender : IEmailSender
{
    private readonly ILogger<NulloEmailSender> _logger;

    public NulloEmailSender(ILogger<NulloEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string to, string from, string subject, string body, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Sending email to {To} from {From} with subject {Subject}", to, from, subject);

        return Task.CompletedTask;
    }
}

using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Email.Models;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Email.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailReceiver receiver, string subject, string body);
    }
}

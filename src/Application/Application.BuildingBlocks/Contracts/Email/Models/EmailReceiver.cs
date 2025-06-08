namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Email.Models
{
    public record EmailReceiver
    {
        public EmailReceiver(string email, string fullName = null, string title = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            Email = email;
            FullName = fullName ?? email;
            Title = title ?? email;
        }

        public string Email { get; private set; }

        public string FullName { get; private set; }

        public string Title { get; private set; }
    }
}

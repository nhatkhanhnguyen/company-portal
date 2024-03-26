using MediatR;

namespace CompanyPortal.CQRS.Email;

public record SendEmailCommand(string From, string To, string Title, string Content) : IRequest
{
    public class Handler : IRequestHandler<SendEmailCommand>
    {
        public Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

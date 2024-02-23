using CompanyPortal.ViewModels;

using MediatR;

namespace CompanyPortal.CQRS.Resources.Commands;

public record CreateResourceCommand(ResourceViewModel Resource) : IRequest<bool>
{

}
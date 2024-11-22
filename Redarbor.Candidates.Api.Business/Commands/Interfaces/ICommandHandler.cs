using Redarbor.Candidates.Api.Domain.Commands;

namespace Redarbor.Candidates.Api.Business.Commands.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
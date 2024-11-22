using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;

namespace Redarbor.Candidates.Api.Business.Commands.Handlers
{
    public class UpdateCandidateCommandHandler : ICommandHandler<UpdateCandidateCommand>
    {
        private readonly ICandidateRepository _candidateRepository;

        public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(UpdateCandidateCommand command)
        {
        }
    }
}
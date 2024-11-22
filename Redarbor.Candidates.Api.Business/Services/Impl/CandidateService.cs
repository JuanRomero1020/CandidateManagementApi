using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Business.Services.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Commands.Delete;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Domain.Dtos;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;

namespace Redarbor.Candidates.Api.Business.Services.Impl
{
    public class CandidateService : ICandidateService
    {
        private readonly ICommandHandler<CreateCandidateCommand> _createCandidateCommandHandler;
        private readonly ICommandHandler<UpdateCandidateCommand> _updateCandidateCommandHandler;
        private readonly ICommandHandler<DeleteCandidateCommand> _deleteCandidateCommandHandler;
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(
            ICommandHandler<CreateCandidateCommand> createCandidateCommandHandler,
            ICommandHandler<UpdateCandidateCommand> updateCandidateCommandHandler,
            ICommandHandler<DeleteCandidateCommand> deleteCandidateCommandHandler,
            ICandidateRepository candidateRepository)
        {
            _createCandidateCommandHandler = createCandidateCommandHandler;
            _updateCandidateCommandHandler = updateCandidateCommandHandler;
            _deleteCandidateCommandHandler = deleteCandidateCommandHandler;
            _candidateRepository = candidateRepository;
        }

        public async Task CreateCandidateAsync(CreateCandidateCommand command)
        {
            await _createCandidateCommandHandler.Handle(command);
        }

        public async Task UpdateAsync(UpdateCandidateCommand command)
        {
            var candidate = await GetByIdAsync(command.Id);
            if (candidate == null) throw new ArgumentException("Candidate not found");
            await _updateCandidateCommandHandler.Handle(command);
        }

        public async Task DeleteAsync(int id)
        {
            var candidate = await GetByIdAsync(id);
            if (candidate == null) throw new ArgumentException("Candidate not found");

            var command = new DeleteCandidateCommand { Id = id };
            await _deleteCandidateCommandHandler.Handle(command);
        }

        public async Task<IEnumerable<CandidateDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CandidateDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
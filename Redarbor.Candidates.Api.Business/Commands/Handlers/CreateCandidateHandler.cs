using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Entities;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;

namespace Redarbor.Candidates.Api.Business.Commands.Handlers
{
    public class CreateCandidateCommandHandler : ICommandHandler<CreateCandidateCommand>
    {
        private readonly ICandidateRepository _candidateRepository;

        public CreateCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task Handle(CreateCandidateCommand command)
        {
           // TODO: Validate if the candidate exists

            var candidate = new Candidate
            {
                Name = command.Name,
                Surname = command.Surname,
                Birthdate = command.Birthdate,
                Email = command.Email,
                InsertDate = DateTime.UtcNow,
                ModifyDate = null,
                CandidateExperiences = command.Experiences?.Select(exp => new CandidateExperience
                {
                    Company = exp.Company,
                    Job = exp.Job,
                    Description = exp.Description,
                    Salary = exp.Salary,
                    BeginDate = exp.BeginDate,
                    EndDate = exp.EndDate,
                    InsertDate = DateTime.UtcNow
                }).ToList()
            };
            await _candidateRepository.AddAsync(candidate);
        }
    }
}
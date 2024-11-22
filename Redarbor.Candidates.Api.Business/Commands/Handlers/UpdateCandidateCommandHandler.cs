using AutoMapper;
using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Domain.Dtos;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;
using Redarbor.Candidates.Api.Domain.Entities;

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
            var candidate = await _candidateRepository.GetByIdAsync(command.IdCandidate);

            if (candidate == null)
            {
                throw new ArgumentException("Candidate not found");
            }

            candidate.Name = command.Name;
            candidate.Surname = command.Surname;
            candidate.Birthdate = command.Birthdate;
            candidate.Email = command.Email;
            candidate.ModifyDate = DateTime.UtcNow;
            candidate.CandidateExperiences = await UpdateCandidateExperiencesAsync(candidate, command.Experiences);
            await _candidateRepository.UpdateAsync(candidate);
        }


        private async Task<List<CandidateExperience>> UpdateCandidateExperiencesAsync(Candidate candidate,
            IEnumerable<UpdateCandidateExperienceCommand>? experiences)
        {
            var existingExperiences = candidate.CandidateExperiences.ToList();

            foreach (var exp in experiences)
            {
                if (!exp.IdCandidateExperience.HasValue) continue;
                var existingExperience =
                    existingExperiences.FirstOrDefault(e => e.IdCandidateExperience == exp.IdCandidateExperience);
                if (existingExperience != null)
                {
                    existingExperience.Company = exp.Company;
                    existingExperience.Job = exp.Job;
                    existingExperience.Description = exp.Description;
                    existingExperience.Salary = exp.Salary;
                    existingExperience.BeginDate = exp.BeginDate;
                    existingExperience.EndDate = exp.EndDate;
                    existingExperience.ModifyDate = DateTime.UtcNow;
                }
                else
                {
                    await InsertNewExperiencesAsync(candidate,exp);
                }
            }

            return existingExperiences;
        }


        private async Task InsertNewExperiencesAsync(Candidate candidate,UpdateCandidateExperienceCommand experience)
        {
            await _candidateRepository.AddCandidateExperienceAsync(new CandidateExperience
            {
                IdCandidate = candidate.IdCandidate,
                Company = experience.Company,
                Job = experience.Job,
                Description = experience.Description,
                Salary = experience.Salary,
                BeginDate = experience.BeginDate,
                EndDate = experience.EndDate,
                InsertDate = DateTime.UtcNow,
            });
        }
    }
}
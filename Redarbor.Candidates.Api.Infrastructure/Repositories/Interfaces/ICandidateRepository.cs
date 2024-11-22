using Redarbor.Candidates.Api.Domain.Entities;

namespace Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateRepository
    {
        Task AddAsync(Candidate candidate);

        Task<Candidate> GetByIdAsync(int id);

        Task<IEnumerable<Candidate>> GetAllAsync();

        Task UpdateAsync(Candidate candidate);

        Task DeleteAsync(Candidate candidate);

        Task AddCandidateExperienceAsync(CandidateExperience experience);

    }
}
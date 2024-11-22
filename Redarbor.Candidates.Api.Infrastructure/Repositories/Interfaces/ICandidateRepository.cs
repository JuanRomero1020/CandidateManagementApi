using Redarbor.Candidates.Api.Domain.Entities;

namespace Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces
{
    public interface ICandidateRepository
    {
        Task AddAsync(Candidate? candidate);
    }
}
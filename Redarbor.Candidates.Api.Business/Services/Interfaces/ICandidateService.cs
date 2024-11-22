using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Domain.Dtos;

namespace Redarbor.Candidates.Api.Business.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetAllAsync();
        Task<CandidateDto> GetByIdAsync(int id);
        Task CreateCandidateAsync(CreateCandidateCommand command);
        Task UpdateAsync(UpdateCandidateCommand candidateDto);
        Task DeleteAsync(int id);
    }
}
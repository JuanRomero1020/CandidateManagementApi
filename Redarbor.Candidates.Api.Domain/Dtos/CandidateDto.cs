namespace Redarbor.Candidates.Api.Domain.Dtos;

public class CandidateDto
{
    public int IdCandidate { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
    public string Email { get; set; }
    public List<CandidateExperienceDto> Experiences { get; set; }
}
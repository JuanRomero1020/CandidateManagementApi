namespace Redarbor.Candidates.Api.Domain.Dtos;

public class CandidateExperienceDto
{
    public int IdCandidateExperience { get; set; }
    public string Company { get; set; }
    public string Job { get; set; }
    public string Description { get; set; }
    public decimal Salary { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}
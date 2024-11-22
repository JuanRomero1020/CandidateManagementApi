namespace Redarbor.Candidates.Api.Domain.Entities;

public class CandidateExperience
{
    public int IdCandidateExperience { get; set; } // PK

    public int IdCandidate { get; set; } // FK

    public string Company { get; set; }
    public string Job { get; set; }
    public string Description { get; set; }
    public decimal Salary { get; set; }

    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; } // Puede ser nulo

    public DateTime InsertDate { get; set; }
    public DateTime? ModifyDate { get; set; } // Puede ser nulo

    // Relación inversa con Candidate
    public Candidate Candidate { get; set; }
}
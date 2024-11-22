namespace Redarbor.Candidates.Api.Domain.Entities;

public class Candidate
{
    public int IdCandidate { get; set; } // PK

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
    public string Email { get; set; } // AK (Unique)

    public DateTime InsertDate { get; set; }
    public DateTime? ModifyDate { get; set; } // Puede ser nulo

    // Relación con CandidateExperience (Uno a muchos)
    public ICollection<CandidateExperience>? CandidateExperiences { get; set; } = new List<CandidateExperience>();
}
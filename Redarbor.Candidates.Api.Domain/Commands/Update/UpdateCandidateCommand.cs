﻿using Redarbor.Candidates.Api.Domain.Dtos;

namespace Redarbor.Candidates.Api.Domain.Commands.Update;

public class UpdateCandidateCommand : ICommand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthdate { get; set; }
    public string Email { get; set; }
    public List<CandidateExperienceDto> CandidateExperiences { get; set; }
}
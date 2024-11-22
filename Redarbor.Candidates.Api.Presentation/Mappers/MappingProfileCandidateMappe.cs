using AutoMapper;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Dtos;

namespace Redarbor.Candidates.Api.Presentation.Mappers;

public class MappingProfileCandidateMappe : Profile
{
    public MappingProfileCandidateMappe()
    {
        CreateMap<CandidateDto, CreateCandidateCommand>();
        CreateMap<CandidateExperienceDto, CreateCandidateExperienceCommand>();
    }
}
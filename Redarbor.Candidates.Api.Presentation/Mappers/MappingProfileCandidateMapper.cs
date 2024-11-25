using AutoMapper;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Domain.Dtos;
using Redarbor.Candidates.Api.Domain.Entities;

namespace Redarbor.Candidates.Api.Presentation.Mappers;

public class MappingProfileCandidateMapper : Profile
{
    public MappingProfileCandidateMapper()
    {
        CreateMap<CandidateDto, CreateCandidateCommand>();
        CreateMap<CandidateDto, UpdateCandidateCommand>();
        CreateMap<CandidateExperienceDto, CreateCandidateExperienceCommand>();
        CreateMap<CandidateExperienceDto, UpdateCandidateExperienceCommand>();
        CreateMap<Candidate, CandidateDto>()
            .ForMember(dest => dest.Experiences, opt => opt.MapFrom(src => src.CandidateExperiences)); 
            
        CreateMap<CandidateExperience, CandidateExperienceDto>();
    }
}
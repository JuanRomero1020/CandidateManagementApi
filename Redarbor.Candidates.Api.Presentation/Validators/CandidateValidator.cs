using FluentValidation;
using Redarbor.Candidates.Api.Domain.Dtos;

namespace Redarbor.Candidates.Api.Presentation.Validators
{
    public class CandidateValidator : AbstractValidator<CandidateDto>
    {
        public CandidateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");
        }
    }
}
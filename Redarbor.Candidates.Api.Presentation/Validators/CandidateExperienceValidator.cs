using System;
using FluentValidation;
using Redarbor.Candidates.Api.Domain.Dtos;

namespace Redarbor.Candidates.Api.Presentation.Validators;

public class CandidateExperienceValidator : AbstractValidator<CandidateExperienceDto>
{
    public CandidateExperienceValidator()
    {
        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Company is required.")
            .MinimumLength(3).WithMessage("Company name must be at least 3 characters long.");

        RuleFor(x => x.Job)
            .NotEmpty().WithMessage("Job title is required.")
            .MinimumLength(3).WithMessage("Job title must be at least 3 characters long.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than zero.");

        RuleFor(x => x.BeginDate)
            .NotEmpty().WithMessage("Start date is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Start date cannot be in the future.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.BeginDate).WithMessage("End date cannot be before start date.")
            .When(x => x.EndDate.HasValue);
    }
}
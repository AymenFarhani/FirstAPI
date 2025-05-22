
using FluentValidation;

namespace FirstAPI.Validators
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(project => project.Title).NotEmpty();
            RuleFor(project => project.Description).NotEmpty();
            RuleFor(project => project.StartDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Start date must be in the past or present.");
            RuleFor(project => project.Budget).GreaterThanOrEqualTo(0);
            RuleFor(project => project.Status)
                .NotEmpty()
                .WithMessage("Status must be a valid enum value.");
            RuleFor(project => project.Contributors)
                .NotEmpty()
                .Must(contributors => contributors.All(c => !string.IsNullOrWhiteSpace(c)))
                .WithMessage("Contributors cannot be empty or whitespace.");
        }
    }
}
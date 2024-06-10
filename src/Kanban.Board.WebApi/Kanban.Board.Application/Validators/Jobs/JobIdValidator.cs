using FluentValidation;

namespace Kanban.Board.Application.Validators.Jobs
{
    public class JobIdValidator : AbstractValidator<int>
    {
        public JobIdValidator()
        {
            RuleFor(n => n)
                .GreaterThan(0)
                .WithMessage("Job Id must be greater than 0.");
        }
    }
}

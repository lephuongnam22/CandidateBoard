using FluentValidation;

namespace Kanban.Board.Application.Validators.Interviewers
{
    public class InterviewerIdValidator : AbstractValidator<int>
    {
        public InterviewerIdValidator()
        {
            RuleFor(n => n)
                .GreaterThan(0)
                .WithMessage("Interviewer Id must be greater than 0.");
        }
    }
}

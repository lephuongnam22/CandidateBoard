using FluentValidation;
using Kanban.Board.Application.Commands.Candidates;
using Kanban.Board.Application.Validators.Interviewers;
using Kanban.Board.Application.Validators.Jobs;

namespace Kanban.Board.Application.Validators.Candidates
{
    public class AddCandidateCommandValidator : AbstractValidator<AddCandidateCommand>
    {
        public AddCandidateCommandValidator()
        {
            RuleFor(n => n.AddCandidateRequest)
                .NotNull()
                .WithMessage("Parameter cannot be null.");

            RuleFor(n => n.AddCandidateRequest.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email cannot be null.")
                .EmailAddress()
                .WithMessage("A valid email address is required.");

            RuleFor(n => n.AddCandidateRequest.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("FirstName cannot be null.");

            RuleFor(n => n.AddCandidateRequest.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("LastName cannot be null.");

            RuleFor(n => n.AddCandidateRequest.PhoneNumber)
                .NotNull()
                .WithMessage("Phone Number cannot be null.");

            RuleFor(n => n.AddCandidateRequest.JobIds)
                .NotNull()
                .Must(n => n.Any())
                .WithMessage("Job Ids cannot be null.");

            RuleFor(n => n.AddCandidateRequest.InterviewerIds)
                .NotNull()
                .Must(n => n.Any())
                .WithMessage("Interviewer Ids cannot be null.");

            RuleForEach(n => n.AddCandidateRequest.JobIds)
                .SetValidator(new JobIdValidator());

            RuleForEach(n => n.AddCandidateRequest.InterviewerIds)
                .SetValidator(new InterviewerIdValidator());
        }
    }
}

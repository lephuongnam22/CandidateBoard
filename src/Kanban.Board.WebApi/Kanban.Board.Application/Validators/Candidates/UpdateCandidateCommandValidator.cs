using FluentValidation;
using Kanban.Board.Application.Commands.Candidates;
using Kanban.Board.Application.Validators.Interviewers;
using Kanban.Board.Application.Validators.Jobs;

namespace Kanban.Board.Application.Validators.Candidates
{
    public class UpdateCandidateCommandValidator : AbstractValidator<UpdateCandidateCommand>
    {
        public UpdateCandidateCommandValidator()
        {
            RuleFor(n => n.UpdateCandidateRequest)
                .NotNull()
                .WithMessage("Parameter cannot be null.");

            RuleFor(n => n.UpdateCandidateRequest.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email cannot be null.")
                .EmailAddress()
                .WithMessage("A valid email address is required.");

            RuleFor(n => n.UpdateCandidateRequest.OldEmail)
                .NotNull()
                .NotEmpty()
                .WithMessage("OldEmail cannot be null.")
                .EmailAddress()
                .WithMessage("A valid email address is required.");

            RuleFor(n => n.UpdateCandidateRequest.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("FirstName cannot be null.");

            RuleFor(n => n.UpdateCandidateRequest.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("LastName cannot be null.");

            RuleFor(n => n.UpdateCandidateRequest.PhoneNumber)
                .NotNull()
                .WithMessage("Phone Number cannot be null.");

            RuleFor(n => n.UpdateCandidateRequest.JobIds)
                .NotNull()
                .Must(n => n.Any())
                .WithMessage("Job Ids cannot be null.");

            RuleFor(n => n.UpdateCandidateRequest.InterviewerIds)
                .NotNull()
                .Must(n => n.Any())
                .WithMessage("Interviewer Ids cannot be null.");

            RuleForEach(n => n.UpdateCandidateRequest.JobIds)
                .SetValidator(new JobIdValidator());

            RuleForEach(n => n.UpdateCandidateRequest.InterviewerIds)
                .SetValidator(new InterviewerIdValidator());
        }
    }
}

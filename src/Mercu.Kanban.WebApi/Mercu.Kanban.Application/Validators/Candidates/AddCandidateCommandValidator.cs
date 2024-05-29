using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercu.Kanban.Application.Commands.Candidates;
using Mercu.Kanban.Application.Validators.Interviewers;
using Mercu.Kanban.Application.Validators.Jobs;

namespace Mercu.Kanban.Application.Validators.Candidates
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

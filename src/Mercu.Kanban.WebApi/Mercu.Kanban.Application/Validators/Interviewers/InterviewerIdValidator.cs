using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Mercu.Kanban.Application.Validators.Interviewers
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

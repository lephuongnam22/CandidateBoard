using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercu.Kanban.Application.Validators.Jobs
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

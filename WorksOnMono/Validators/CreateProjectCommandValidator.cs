using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Commands;
using FluentValidation;

namespace Wom.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.ProjectUrl)
                .NotEmpty();
            RuleFor(x => x.Summary)
                .NotEmpty();
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }
}
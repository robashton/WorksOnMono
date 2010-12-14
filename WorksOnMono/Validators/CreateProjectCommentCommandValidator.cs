using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Commands;
using FluentValidation;

namespace Wom.Validators
{
    public class CreateProjectCommentCommandValidator : AbstractValidator<CreateProjectCommentCommand>
    {
        public CreateProjectCommentCommandValidator()
        {
            RuleFor(x => x.CommentType)
             .NotEmpty();
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.MonoVersion)
                .NotEmpty();
            RuleFor(x => x.ProjectVersion)
                .NotEmpty();
            RuleFor(x => x.ProjectId)
              .NotEmpty();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Commands;
using FluentValidation;

namespace Wom.Validators
{
    public class CreateCommentVoteCommandValidator : AbstractValidator<CreateCommentVoteCommand>
    {
        public CreateCommentVoteCommandValidator()
        {
            RuleFor(x => x.CommentId)
                .NotEmpty();
            RuleFor(x => x.ProjectId)
                .NotEmpty();
            RuleFor(x => x.CurrentUserId)
                .NotEmpty();
        }
    }
}
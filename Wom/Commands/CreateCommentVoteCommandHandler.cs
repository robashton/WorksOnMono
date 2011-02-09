using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Commands;
using Wom.Core;
using Raven.Client;
using Wom.Documents;

namespace Wom.CommandHandlers
{
    public class CreateCommentVoteCommandHandler : ICommandHandler<CreateCommentVoteCommand>
    {
        private IDocumentSession documentSession;

        public CreateCommentVoteCommandHandler(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public void Handle(CreateCommentVoteCommand command)
        {
            documentSession.Store(new ProjectCommentVote()
            {
                ProjectCommentId = command.CommentId,
                ProjectId = command.ProjectId,
                UserId = command.CurrentUserId
            });
        }
    }
}
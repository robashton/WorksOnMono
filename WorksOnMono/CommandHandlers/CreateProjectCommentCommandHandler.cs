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
    public class CreateProjectCommentCommandHandler : ICommandHandler<CreateProjectCommentCommand>
    {
           private IDocumentSession documentSession;

           public CreateProjectCommentCommandHandler(IDocumentSession documentSession)
           {
               this.documentSession = documentSession;
           }

        public void Handle(CreateProjectCommentCommand command)
        {
            documentSession.Store(new ProjectComment()
            {
                ProjectId = command.ProjectId,
                MonoVersion = command.MonoVersion,
                ProjectVersion = command.ProjectVersion,
                Description = command.Description,
                UserId = command.CurrentUserId
            });
        }
    }
}
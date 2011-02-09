using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Core;
using Wom.Commands;
using Raven.Client;
using Wom.Documents;

namespace Wom.CommandHandlers
{
    public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private IDocumentSession documentSession;

        public CreateProjectCommandHandler(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public void Handle(CreateProjectCommand command)
        {
            documentSession.Store(new Project()
            {
                CreatorId = command.CurrentUserId,
                ProjectUrl = command.ProjectUrl,
                Title = command.Title,
                Summary = command.Summary
            });
        }
    }
}
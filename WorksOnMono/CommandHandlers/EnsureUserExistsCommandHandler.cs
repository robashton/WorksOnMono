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
    public class EnsureUserExistsCommandHandler : ICommandHandler<EnsureUserExistsCommand>
    {
        private IDocumentSession documentSession;

        public EnsureUserExistsCommandHandler(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }
        public void Handle(EnsureUserExistsCommand command)
        {
            User user = documentSession.Load<User>(command.Id);
            if (user == null)
            {
                user = new User()
                {
                    Id = command.Id,
                    Username = command.DisplayName
                };
                documentSession.Store(user);
            }
        }
    }
}
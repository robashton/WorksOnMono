using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wom.CommandHandlers;
using Raven.Client;
using Moq;
using NUnit.Framework;
using Wom.Commands;
using Wom.Documents;

namespace WorksOnMono.Tests.CommandHandlers
{
    [TestFixture]
    public class EnsureUserExistsCommandHandlerTests
    {
        EnsureUserExistsCommandHandler handler;
        Mock<IDocumentSession> documentSession;

        [SetUp]
        public void SetupTests()
        {
            documentSession = new Mock<IDocumentSession>();
            handler = new EnsureUserExistsCommandHandler(documentSession.Object);
        }

        [Test]
        public void Handle_CreatesNewUser_WhenUserDoesNotExist()
        {
            EnsureUserExistsCommand command = new EnsureUserExistsCommand("userId", "Whatever");
            handler.Handle(command);
            documentSession.Verify(x => x.Store(It.Is<User>(y =>
                y.Id == command.Id &&
                y.Username == command.DisplayName)), Times.Once());
        }

        [Test]
        public void Handle_DoesNothing_WhenUserExists()
        {
            EnsureUserExistsCommand command = new EnsureUserExistsCommand("userId", "Whatever");
            documentSession.Setup(x => x.Load<User>("userId")).Returns(new User());
            handler.Handle(command);
            documentSession.Verify(x => x.Store(It.IsAny<User>()), Times.Never());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wom.CommandHandlers;
using Moq;
using Raven.Client;
using Wom.Commands;
using Wom.Documents;

namespace WorksOnMono.Tests.CommandHandlers
{
    [TestFixture]
    public class CreateProjectCommandHandlerTests
    {
        CreateProjectCommandHandler handler;
        Mock<IDocumentSession> documentSession;

        [SetUp]
        public void SetupTests()
        {
            documentSession = new Mock<IDocumentSession>();
            handler = new CreateProjectCommandHandler(documentSession.Object);
        }

        [Test]
        public void Handle_StoresNewProject_WithCorrectDetails()
        {
            CreateProjectCommand command = new CreateProjectCommand()
            {
                CurrentUserId = "2",
                ProjectUrl = "http://example.com",
                Summary = "Something",
                Title = "Stuff"
            };

            handler.Handle(command);

            documentSession.Verify(x => x.Store(
                It.Is<Project>(y =>
                    y.ProjectUrl == command.ProjectUrl &&
                    y.Summary == command.Summary &&
                    y.Title == y.Title &&
                    y.CreatorId == command.CurrentUserId)), Times.Once());
        }
    }
}

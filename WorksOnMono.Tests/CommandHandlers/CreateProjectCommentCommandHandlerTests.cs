using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wom.CommandHandlers;
using Raven.Client;
using Moq;
using Wom.Commands;
using Wom.Documents;

namespace WorksOnMono.Tests.CommandHandlers
{
    [TestFixture]
    public class CreateProjectCommentCommandHandlerTests
    {
        CreateProjectCommentCommandHandler handler;
        Mock<IDocumentSession> documentSession;

        [SetUp]
        public void SetupTests()
        {
            documentSession = new Mock<IDocumentSession>();
            handler = new CreateProjectCommentCommandHandler(documentSession.Object);
        }

        [Test]
        public void Handle_StoresNewProject_WithCorrectDetails()
        {
            CreateProjectCommentCommand command = new CreateProjectCommentCommand()
            {
                CommentType = ProjectCommentType.Comment,
                Description = "Desc",
                MonoVersion = "2.6",
                ProjectVersion = "2.4",
                ProjectId = "xUnit",
                CurrentUserId = "232"
            };

            handler.Handle(command);

            documentSession.Verify(x => x.Store(
                It.Is<ProjectComment>(y =>
                    y.Description == command.Description &&
                    y.MonoVersion == command.MonoVersion &&
                    y.ProjectVersion == command.ProjectVersion &&
                    y.UserId  == command.CurrentUserId &&
                    y.ProjectId == command.ProjectId)), Times.Once());
        }
    }
}

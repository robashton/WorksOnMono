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
    public class CreateCommentVoteCommandHandlerTests
    {
        CreateCommentVoteCommandHandler handler;
        Mock<IDocumentSession> documentSession;

        [SetUp]
        public void SetupTests()
        {
            documentSession = new Mock<IDocumentSession>();
            handler = new CreateCommentVoteCommandHandler(documentSession.Object);
        }

        [Test]
        public void Handle_StoresNewProject_WithCorrectDetails()
        {
            CreateCommentVoteCommand command = new CreateCommentVoteCommand()
            {
                CommentId = "1",
                ProjectId = "2",
                CurrentUserId = "3"
            };

            handler.Handle(command);

            documentSession.Verify(x => x.Store(
                It.Is<ProjectCommentVote>(y =>
                    y.ProjectCommentId == command.CommentId &&
                    y.ProjectId == command.ProjectId &&
                    y.UserId == y.UserId)), Times.Once());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wom.Controllers;
using Wom.Core;
using Moq;
using Wom.ViewModels;
using System.Web.Mvc;
using Wom.Commands;

namespace WorksOnMono.Tests.Controllers
{
    [TestFixture]
    public class ProjectsControllerTests
    {
        ProjectsController controller;
        Mock<ICommandInvoker> commandInvoker;
        Mock<IViewRepository> viewRepository;

        [SetUp]
        public void SetupTest()
        {
            commandInvoker = new Mock<ICommandInvoker>();
            viewRepository = new Mock<IViewRepository>();
            controller = new ProjectsController(commandInvoker.Object, viewRepository.Object);
        }

        [Test]
        public void Get_Index_ReturnsViewFromFactory()
        {
            ProjectIndexInputModel inputModel = new ProjectIndexInputModel();
            ProjectIndexViewModel viewModel = new ProjectIndexViewModel();
            viewRepository.Setup(x => x.Load<ProjectIndexInputModel, ProjectIndexViewModel>(inputModel)).Returns(viewModel);

            ViewResult result = (ViewResult)controller.Index(inputModel);
            Assert.AreEqual(viewModel, result.ViewData.Model);
        }

        [Test]
        public void Get_Item_ReturnsViewFromFactory()
        {
            ProjectItemInputModel inputModel = new ProjectItemInputModel();
            ProjectItemViewModel viewModel = new ProjectItemViewModel();
            viewRepository.Setup(x=>x.Load<ProjectItemInputModel,ProjectItemViewModel>(inputModel)).Returns(viewModel);

            ViewResult result = (ViewResult)controller.Item(inputModel);
            Assert.AreEqual(viewModel, result.ViewData.Model);
        }
        
        [Test]
        public void Post_WhenValid_CreateProject_CommandIsSent()
        {
           CreateProjectCommand command = new CreateProjectCommand();
           controller._CreateProject(command);
           commandInvoker.Verify(x => x.Execute(command), Times.Once());           
        }

        [Test]
        public void Post_WhenInvalid_CreateProject_CommandIsNotSent()
        {
            CreateProjectCommand command = new CreateProjectCommand();
            controller.ModelState.AddModelError("1", "1");
            controller._CreateProject(command);
            commandInvoker.Verify(x => x.Execute(command), Times.Never());
        }

        [Test]
        public void Post_WhenValid_CreateComment_CommandIsSent()
        {
            CreateProjectCommentCommand command = new CreateProjectCommentCommand();
            controller._CreateComment(command);
            commandInvoker.Verify(x => x.Execute(command), Times.Once());      
        }

        [Test]
        public void Post_WhenInvalid_CreateComment_CommandIsNotSent()
        {
            CreateProjectCommentCommand command = new CreateProjectCommentCommand();
            controller.ModelState.AddModelError("1", "1");
            controller._CreateComment(command);
            commandInvoker.Verify(x => x.Execute(command), Times.Never());     
        }

        [Test]
        public void Post_WhenValidVoteOnComment_CommandIsSent()
        {
            CreateCommentVoteCommand command = new CreateCommentVoteCommand();
            controller._VoteOnComment(command);
            commandInvoker.Verify(x => x.Execute(command), Times.Once());    
        }
        
        [Test]
        public void Post_WhenInvalidVoteOnComment_CommandIsNotSent()
        {
            CreateCommentVoteCommand command = new CreateCommentVoteCommand();
            controller.ModelState.AddModelError("1", "1");
            controller._VoteOnComment(command);
            commandInvoker.Verify(x => x.Execute(command), Times.Never());   
        }

        [Test]
        public void Post_GetComments_ReturnsViewFromFactory()
        {
            ProjectCommentsInputModel inputModel = new ProjectCommentsInputModel();
            ProjectCommentsViewModel viewModel = new ProjectCommentsViewModel();
            viewRepository.Setup(x => x.Load<ProjectCommentsInputModel, ProjectCommentsViewModel>(inputModel)).Returns(viewModel);

            JsonResult result = (JsonResult)controller._GetComments(inputModel);
            Assert.AreEqual(viewModel, result.Data);
        }

    }
}

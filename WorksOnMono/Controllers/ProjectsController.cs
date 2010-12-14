using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wom.Core;
using Wom.Commands;
using Wom.ViewModels;

namespace Wom.Controllers
{
    public class ProjectsController : Controller
    {
        private ICommandInvoker commandInvoker;
        private IViewRepository viewRepository;

        public ProjectsController(ICommandInvoker commandInvoker, IViewRepository viewRepository)
        {
            this.commandInvoker = commandInvoker;
            this.viewRepository = viewRepository;
        }

        [HttpGet]
        public ActionResult Item(ProjectItemInputModel input)
        {
            var view = viewRepository.Load<ProjectItemInputModel, ProjectItemViewModel>(input);
            return View(view);
        }

        [HttpGet]
        public ActionResult Index(ProjectIndexInputModel input)
        {
            var view = viewRepository.Load<ProjectIndexInputModel, ProjectIndexViewModel>(input);
            return View(view);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult _CreateProject(CreateProjectCommand command)
        {
            return MarshalCommand(command);
        }

        [Authorize]
        [HttpPost]
        public ActionResult _CreateComment(CreateProjectCommentCommand command)
        {
            return MarshalCommand(command);
        }

        [Authorize]
        [HttpPost]
        public ActionResult _VoteOnComment(CreateCommentVoteCommand command)
        {
            return MarshalCommand(command);
        }
        
        [HttpGet]
        public ActionResult _GetComments(ProjectCommentsInputModel input)
        {
            var view = viewRepository.Load<ProjectCommentsInputModel, ProjectCommentsViewModel>(input);
            return Json(view);
        }

        private ActionResult MarshalCommand<T>(T command)
        {
            if (ModelState.IsValid)
            {
                commandInvoker.Execute(command);
                return Json(new CommandResult() { Success = true });
            }
            return Json(new CommandResult() { Success = false });
        }
    }
}

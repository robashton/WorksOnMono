using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Core;
using Raven.Client;
using Wom.Documents;

namespace Wom.ViewModels
{
    public class ProjectItemViewModelFactory : IViewFactory<ProjectItemInputModel, ProjectItemViewModel>
    {
        IDocumentSession documentSession;

        public ProjectItemViewModelFactory(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public ProjectItemViewModel Load(ProjectItemInputModel input)
        {
            Project project = documentSession.Load<Project>(input.ProjectId);
            return new ProjectItemViewModel()
            {
                Id = project.Id,
                Summary = project.Summary,
                Title = project.Title
            };
        }
    }
}
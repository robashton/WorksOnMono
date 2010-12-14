using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Core;
using Raven.Client;

namespace Wom.ViewModels
{
    public class ProjectCommentsViewModelFactory : IViewFactory<ProjectCommentsInputModel, ProjectCommentsViewModel>
    {
        IDocumentSession documentSession;

        public ProjectCommentsViewModelFactory(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public ProjectCommentsViewModel Load(ProjectCommentsInputModel input)
        {
            throw new NotImplementedException();
        }
    }
}
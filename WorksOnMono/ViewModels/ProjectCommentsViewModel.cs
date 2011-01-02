using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.ViewModels
{
    public class ProjectCommentsViewModel
    {
        public string ProjectId { get; set; }
        public ProjectCommentsViewModelItem[] Items { get; set; }
    }
}
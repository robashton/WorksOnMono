using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Documents;

namespace Wom.ViewModels
{
    public class ProjectCommentsInputModel
    {
        public string ProjectId { get; set; }
        public ProjectCommentType CommentType { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
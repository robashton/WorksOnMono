using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.ViewModels
{
    public class ProjectCommentsViewModelItem
    {
        public string ProjectId { get; set; }
        public string ProjectCommentId { get; set; }
        public int Score { get; set; }
        public string MonoVersion { get; set; }
        public string ProjectVersion { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string CommentDescription { get; set; }
    }
}
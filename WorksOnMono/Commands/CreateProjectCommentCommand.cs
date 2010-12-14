using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Documents;

namespace Wom.Commands
{
    public class CreateProjectCommentCommand
    {
        public string ProjectId { get; set; }
        public string CurrentUserId { get; set; }
        public string Description { get; set; }
        public string ProjectVersion { get; set; }
        public string MonoVersion { get; set; }
        public ProjectCommentType CommentType { get; set; }
    }
}
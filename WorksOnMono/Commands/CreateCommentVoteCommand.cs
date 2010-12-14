using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.Commands
{
    public class CreateCommentVoteCommand
    {
        public string ProjectId { get; set; }
        public string CommentId { get; set; }
        public string CurrentUserId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.Documents
{
    public class ProjectComment
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string ProjectVersion { get; set; }
        public string MonoVersion { get; set; }
    }
}
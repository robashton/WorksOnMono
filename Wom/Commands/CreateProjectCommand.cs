using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.Commands
{
    public class CreateProjectCommand
    {
        public string Title { get; set; }
        public string ProjectUrl { get; set; }
        public string Summary { get; set; }
        public string CurrentUserId { get; set; }
    }
}
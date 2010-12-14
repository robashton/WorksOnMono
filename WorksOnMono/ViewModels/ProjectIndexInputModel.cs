using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.ViewModels
{
    public class ProjectIndexInputModel
    {
        public string SearchText { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.Commands
{
    public class EnsureUserExistsCommand
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public EnsureUserExistsCommand(string id, string displayName)
        {
            this.Id = id;
            this.DisplayName = displayName;
        }
    }
}
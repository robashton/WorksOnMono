using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wom.Behaviours
{
    public class ServiceCommandOutput
    {
        public bool Success { get; set; }
        public ServiceCommandOutputMessage[] Messages { get; set; }
    }

    public class ServiceCommandOutputMessage
    {
        public string Key { get; set; }
        public string Text { get; set; }
    }
}
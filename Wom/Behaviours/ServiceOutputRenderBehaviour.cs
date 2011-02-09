using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace Wom.Behaviours
{
    public class ServiceOutputRenderBehaviour<T> : IActionBehavior where T : class
    {
        private readonly IFubuRequest request;
        private readonly IJsonWriter writer;

        public IActionBehavior InsideBehavior { get; set; }

        public ServiceOutputRenderBehaviour(IJsonWriter writer, IFubuRequest request)
        {
            this.writer = writer;
            this.request = request;
        }

        public void Invoke()
        {
            if(InsideBehavior != null)
            {
                InsideBehavior.Invoke();
            }
            var output = request.Get<T>();
            writer.Write(output); 
        }

        public void InvokePartial()
            {
                this.Invoke();
            }
    }
}
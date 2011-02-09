using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;

namespace Wom.Behaviours
{
    public class ValidationAwareActionInvoker<TController, TInput> : OneInZeroOutActionInvoker<TController, TInput> where TInput : class
    {
        private readonly IFubuRequest request;

        public ValidationAwareActionInvoker(IFubuRequest request, TController controller, Action<TController, TInput> action) : base(request, controller, action)
        {
            this.request = request;
        }

        protected override FubuMVC.Core.DoNext performInvoke()
        {
            var result = request.Get<ServiceCommandOutput>();
            if (result.Success)
            {
                base.performInvoke();
            }
            return FubuMVC.Core.DoNext.Continue;
        }
    }
}
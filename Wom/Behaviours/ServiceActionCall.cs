using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;

namespace Wom.Behaviours
{
    public class ServiceActionCall : ActionCall
    {
        private readonly ActionCall inner;

        public ServiceActionCall(ActionCall inner) : base(inner.HandlerType, inner.Method)
        {
            this.inner = inner;
        }

        protected override ObjectDef buildObjectDef()
        {
            var originalDef = base.buildObjectDef();
            originalDef.Type = typeof (ValidationAwareActionInvoker<,>)
                .MakeGenericType(inner.HandlerType,
                inner.Method.GetParameters().First().ParameterType);

            return originalDef;
        }

        public override BehaviorCategory Category
        {
            get { return BehaviorCategory.Call; }
        }
    }
}
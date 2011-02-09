using System.Linq;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using Wom.Behaviours;

namespace Wom
{
    public class CommandHandlerUrlPolicy : IUrlPolicy
    {
        public bool Matches(ActionCall call, IConfigurationObserver log)
        {
            return call.Method.Name == "Handle";
        }

        public IRouteDefinition Build(ActionCall call)
        {
            call = WrapActionCall(call);

            var route = call.ToRouteDefinition();
            route.Append("Services");
            route.Append("Commands");
            route.Append(call.HandlerType.Name.Replace("CommandHandler", ""));

            return route;
        }

        private ServiceActionCall WrapActionCall(ActionCall call)
        {
            var outerCall = new ServiceActionCall(call);
            call.ReplaceWith(outerCall);

            var validationType = typeof(ServiceValidationBehaviour<>).MakeGenericType(outerCall.InputType());
            outerCall.WrapWith(validationType);

            var jsonBehaviourType = typeof(ServiceOutputRenderBehaviour<>).MakeGenericType(typeof(ServiceCommandOutput));
            var outputNode = new OutputNode(jsonBehaviourType);
            outerCall.AddToEnd(outputNode);
            return outerCall;
        }
    }
}
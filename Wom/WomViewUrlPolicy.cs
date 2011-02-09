using System.Linq;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;

namespace Wom
{
    public class WomViewUrlPolicy : IUrlPolicy
    {
        public bool Matches(ActionCall call, IConfigurationObserver log)
        {
            // This policy is global
            return call.HandlerType.Namespace.StartsWith("Wom.Views");
        }

        public IRouteDefinition Build(ActionCall call)
        {
            var ns = call.HandlerType.Namespace.Split('.').Last();
            var route = call.ToRouteDefinition();

            route.Append(ns);
            route.Append(call.HandlerType.Name);
           
            return route;
        }
    }

}
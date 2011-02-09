using System.Linq;
using FubuMVC.Core.Registration.Nodes;
using Spark.Web.FubuMVC.Registration;

namespace Wom
{
	public class WomSparkPolicy : ISparkPolicy
	{
		public bool Matches(ActionCall call)
		{
		    return call.HandlerType.Namespace.StartsWith("Wom.Views");
		}

		public string BuildViewLocator(ActionCall call)
		{
		    return call.HandlerType.Namespace.Split('.').Last();
		}

		public string BuildViewName(ActionCall call)
		{
            return call.HandlerType.Name;
		}
	}
}
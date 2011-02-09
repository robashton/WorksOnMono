using System.Web;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using System.Web.Routing;

namespace Wom
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Startup();
        }
    }
}
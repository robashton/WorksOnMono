using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wom.Core;
using FluentValidation.Mvc;
using Spark;
using Spark.Web.Mvc;
using FluentValidation;
using StructureMap;

namespace Wom
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            RegisterViewEngine();

            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(new StructureMapValidatorFactory()));
            ControllerBuilder.Current.SetControllerFactory(new Wom.Core.StructureMapControllerFactory());
            ModelBinders.Binders.DefaultBinder = new GenericBinderResolver();
            Bootstrapper.Startup();
        }

        private static void RegisterViewEngine()
        {
            ISparkSettings settings = new SparkSettings()
                .SetAutomaticEncoding(true);
            SparkEngineStarter.RegisterViewEngine(settings);
        }

        public class StructureMapValidatorFactory : ValidatorFactoryBase
        {
            public override IValidator CreateInstance(Type validatorType)
            {
                return ObjectFactory.TryGetInstance(validatorType) as IValidator;
            }
        }
    }
}
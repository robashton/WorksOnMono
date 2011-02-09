using System;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Nodes;
using Spark.Web.FubuMVC;
using Wom.Behaviours;
using Wom.Views.Home;

namespace Wom
{
    public class WomRegistry : FubuRegistry
    {
        public static string[] Verbs =
            {
                "Get",
                "Post"
            };

        public WomRegistry()
        {
            IncludeDiagnostics(true);

            Applies
                .ToThisAssembly();

            Actions
                .IncludeTypesNamed(x => x.EndsWith("CommandHandler"));

            Output
                .ToJson
                .WhenTheOutputModelIs<ServiceCommandOutput>();

            Routes
                .UrlPolicy<CommandHandlerUrlPolicy>();
            
            this.Spark(spark => spark
                                    .Policies
                                    .Add<WomSparkPolicy>());
           
            HomeIs<Index>(c => c.Get());
        }
    }
}
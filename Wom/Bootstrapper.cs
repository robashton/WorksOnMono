using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using FluentValidation;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using Raven.Client;
using StructureMap;
using Raven.Client.Document;
using Raven.Database;
using Raven.Client.Indexes;
using Raven.Client.Client;
using Raven.Database.Config;
using StructureMap.Configuration.DSL;
using Wom.Core;
using Wom.Core.Conventions;
using Wom.Indexes;

namespace Wom
{
    public static class Bootstrapper
    {
        public static void Startup()
        {
            var documentStore = new EmbeddableDocumentStore
            {
                Configuration = new RavenConfiguration
                {
                    DataDirectory = "App_Data\\RavenDB",
                }
            };
            documentStore.Initialize();
            
            IndexCreation.CreateIndexes(typeof(MonoVersionIndex).Assembly, documentStore);

            FubuApplication
             .For<WomRegistry>()
             .StructureMapObjectFactory(x=> x.AddRegistry(new CoreRegistry(documentStore)))
             .Bootstrap(RouteTable.Routes);

        }
    }

    public class CoreRegistry : Registry
    {
        private readonly IDocumentStore documentStore;

        public CoreRegistry(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;

            Scan(s =>
            {
                s.AssembliesFromApplicationBaseDirectory(x => x.FullName.StartsWith("Wom"));
                s.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                s.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                s.With(new RegisterFirstInstanceOfInterface());
            });

            For<IDocumentStore>().Use(documentStore);
            For<IDocumentSession>()
                .HybridHttpOrThreadLocalScoped()
                .Use(x =>
                {
                    var store = x.GetInstance<IDocumentStore>();
                    return store.OpenSession();
                });
        }
    }
}

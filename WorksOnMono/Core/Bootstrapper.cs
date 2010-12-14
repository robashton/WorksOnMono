using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Raven.Client.Document;
using Raven.Database;
using Raven.Client.Indexes;
using Raven.Client.Client;
using Raven.Database.Config;
using WorksOnMono.Indexes;

namespace Wom.Core
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

            ObjectFactory.Initialize(config =>
            {
                config.AddRegistry(new CoreRegistry(documentStore));
            });

            IndexCreation.CreateIndexes(typeof(MonoVersionIndex).Assembly, documentStore);
        }
    }
}

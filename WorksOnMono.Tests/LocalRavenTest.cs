using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Client;
using Raven.Database.Config;
using WorksOnMono.Indexes;
using Raven.Client.Indexes;
using NUnit.Framework;
using System.Threading;

namespace WorksOnMono.Tests
{
    public class LocalRavenTest
    {
        private EmbeddableDocumentStore store;
        public EmbeddableDocumentStore Store { get { return store; } }

        [SetUp]
        public void CreateStore()
        {
            store = new EmbeddableDocumentStore
            {
                Configuration = new RavenConfiguration
                {
                    RunInMemory = true
                }
            };
            store.Initialize();
            IndexCreation.CreateIndexes(typeof(MonoVersionIndex).Assembly, store);
        }

        [TearDown]
        public void DestroyStore()
        {
            store.Dispose();
        }

        public void WaitForIndexing()
        {
            while (store.DocumentDatabase.Statistics.StaleIndexes.Length > 0)
            {
                Thread.Sleep(100);
            }
        }
    }

}

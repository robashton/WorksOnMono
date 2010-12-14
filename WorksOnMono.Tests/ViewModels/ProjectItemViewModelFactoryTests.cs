using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wom.Documents;
using Wom.ViewModels;

namespace WorksOnMono.Tests.ViewModels
{
    [TestFixture]
    public class ProjectItemViewModelFactoryTests : LocalRavenTest
    {
        [SetUp]
        public void SetupTest()
        {
            using (var session = this.Store.OpenSession())
            {
                session.Store(new Project()
                {
                    Id = "test",
                    ProjectUrl = "blah.com",
                    Summary = "Hello",
                    Title = "World"
                });
                session.SaveChanges();
            }
            WaitForIndexing();
        }

        [Test]
        public void Load_ReturnsExpectedData()
        {
            using (var session = this.Store.OpenSession())
            {
                ProjectItemViewModelFactory factory = new ProjectItemViewModelFactory(session);
                var result = factory.Load(new ProjectItemInputModel() { ProjectId = "test" });

                Assert.AreEqual("test", result.Id);
                Assert.AreEqual("Hello", result.Summary);
                Assert.AreEqual("World", result.Title);
            }
        }
    }
}

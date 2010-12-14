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
    public class ProjectIndexViewModelFactoryTests : LocalRavenTest
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
                session.Store(new Project()
                {
                    Id = "test2",
                    ProjectUrl = "blah.com",
                    Summary = "Raven",
                    Title = "Whatever"
                });
                session.Store(new Project()
                {
                    Id = "test3",
                    ProjectUrl = "blah.com",
                    Summary = "Hello",
                    Title = "World"
                });
                session.Store(new Project()
                {
                    Id = "test4",
                    ProjectUrl = "blah.com",
                    Summary = "Hello",
                    Title = "World"
                });
                session.Store(new Project()
                {
                    Id = "test5",
                    ProjectUrl = "blah.com",
                    Summary = "Hello",
                    Title = "World"
                });
                session.SaveChanges();
            }
        }

        [Test]
        public void Load_WithoutSearchTerm_ReturnsAll()
        {
            using (var session = this.Store.OpenSession())
            {
                ProjectIndexViewModelFactory factory = new ProjectIndexViewModelFactory(session);
                var result = factory.Load(new ProjectIndexInputModel() { Page = 0, PageSize = 100 });

                Assert.AreEqual(5, result.Items.Length);
            }
        }

        [Test]
        public void Load_SetsPropertiesInOutput()
        {
            using (var session = this.Store.OpenSession())
            {
                ProjectIndexViewModelFactory factory = new ProjectIndexViewModelFactory(session);
                var result = factory.Load(new ProjectIndexInputModel() { Page = 2, PageSize = 1, SearchText = "Blah"});

                Assert.AreEqual(2, result.Page);
                Assert.AreEqual(1, result.PageSize);
                Assert.AreEqual("Blah", result.SearchText);
            }
        }

        [Test]
        public void Load_ReturnsExpectedData()
        {
            using (var session = this.Store.OpenSession())
            {
                ProjectIndexViewModelFactory factory = new ProjectIndexViewModelFactory(session);
                var result = factory.Load(new ProjectIndexInputModel() { Page = 0, PageSize = 1, SearchText = "Whatever" });
                Assert.AreEqual(1, result.Items.Length);

                var item = result.Items[0];

                Assert.AreEqual("test2", item.Id);
                Assert.AreEqual("Raven", item.Summary);
                Assert.AreEqual("Whatever", item.ProjectTitle);
            }
        }
    }
}

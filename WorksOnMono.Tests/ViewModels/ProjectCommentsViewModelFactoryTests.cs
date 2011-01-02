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
    public class ProjectCommentsViewModelFactoryTests : LocalRavenTest
    {
        [SetUp]
        public void SetupTest()
        {
            using (var session = Store.OpenSession())
            {
                session.Store(new ProjectComment()
                {
                    Id = "comment1",
                    Description = "desc",
                    MonoVersion = "2",
                    ProjectVersion = "3",
                    ProjectId = "project1",
                    UserId = "user1"
                });

                session.Store(new ProjectComment()
                {
                    Id = "comment2",
                    Description = "desc",
                    MonoVersion = "2",
                    ProjectVersion = "3",
                    ProjectId = "project1",
                    UserId = "user1"
                });

                session.Store(new User()
                {
                    Id = "user1",
                    Username = "Rob"
                });
                
                session.Store(new Project(){
                    Id = "project1"                     
                });
               
                session.SaveChanges();
            }
            WaitForIndexing();
        }

        [Test]
        public void Query_BringsBackOtherDocumentProperties()
        {
            using (var session = Store.OpenSession())
            {
                ProjectCommentsViewModelFactory factory = new ProjectCommentsViewModelFactory(session);
                factory.Load(new ProjectCommentsInputModel()
                {

                });
            }
        }
    }
}

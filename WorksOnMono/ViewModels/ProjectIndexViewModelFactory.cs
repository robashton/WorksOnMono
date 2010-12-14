using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Core;
using Raven.Client;
using Wom.Documents;

namespace Wom.ViewModels
{
    public class ProjectIndexViewModelFactory : IViewFactory<ProjectIndexInputModel, ProjectIndexViewModel>
    {
        IDocumentSession documentSession;

        public ProjectIndexViewModelFactory(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public ProjectIndexViewModel Load(ProjectIndexInputModel input)
        {
            var query = documentSession.Query<Project>()
                            .AsQueryable();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                query = query.Where(x =>
                    x.Title.StartsWith(input.SearchText));
            }

            var results = query.Skip(input.PageSize * input.Page).Take(input.PageSize).ToArray();
            return new ProjectIndexViewModel()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchText = input.SearchText,
                Items = results.Select(x => new ProjectIndexViewModelItem()
                {
                    Id = x.Id,
                    ProjectTitle = x.Title,
                    Summary = x.Summary
                }).ToArray()
            };
        }
    }
}
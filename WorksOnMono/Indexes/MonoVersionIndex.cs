using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;
using Wom.Documents;

namespace WorksOnMono.Indexes
{
    public class MonoVersion { public string Version { get; set; } public int Count { get; set; } }
    public class MonoVersionIndex : AbstractIndexCreationTask<ProjectComment, MonoVersion>
    {
        public MonoVersionIndex()
        {
            Map = docs => from doc in docs
                          select new
                          {
                             Version = doc.MonoVersion,
                             Count = 1
                          };
            Reduce = results => from result in results
                                group result by result.Version into g
                                select new
                                {
                                    Version = g.Key,
                                    Count = g.Sum(x => x.Count)
                                };
        }
    }
}
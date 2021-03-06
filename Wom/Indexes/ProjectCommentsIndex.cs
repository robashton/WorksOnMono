﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wom.Documents;
using Raven.Client.Indexes;

namespace Wom.Indexes
{
    public class ProjectCommentsIndex : AbstractIndexCreationTask<ProjectCommentVote, PopularByProjectOutput>
    {
        public ProjectCommentsIndex()
        {
            Map = docs => from doc in docs
                          select new
                          {
                              ProjectCommentId = doc.ProjectCommentId,
                              ProjectId = doc.ProjectId,
                              Score = doc.Weight
                          };

            Reduce = mapped => from map in mapped
                               group map by new
                               {
                                   map.ProjectCommentId,
                                   map.ProjectId
                               } 
                               into g
                               select new
                               {
                                   ProjectCommentId = g.Key.ProjectCommentId,
                                   ProjectId = g.Key.ProjectId,
                                   Score = g.Sum(x => x.Score)
                               };

            TransformResults = (database, results) =>
                                from result in results
                                let comment = database.Load<ProjectComment>(result.ProjectCommentId)
                                let user = database.Load<User>(comment.UserId)
                                select new
                                {
                                    ProjectCommentId = result.ProjectCommentId,
                                    Score = result.Score,
                                    MonoVersion = comment.MonoVersion,
                                    ProjectVersion = comment.ProjectVersion,
                                    UserId = comment.UserId,
                                    Username = user.Username,
                                    CommentDescription = comment.Description
                                };
        }
    }

    public class PopularByProjectOutput
    {
        public string ProjectCommentId { get; set; }
        public object ProjectId { get; set; }
        public int Score { get; set; }
        public string MonoVersion { get; set; }
        public string ProjectVersion { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
    }
}
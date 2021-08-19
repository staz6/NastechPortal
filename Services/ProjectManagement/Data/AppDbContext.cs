using System;
using Microsoft.EntityFrameworkCore;
using Polly;
using ProjectManagement.Entites;

namespace ProjectManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMemebers { get; set; }
        public DbSet<ProjectPhase> ProjectPhases { get; set; }
        public DbSet<ProjectSubFolder> ProjectSubFolders { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectSubTask> ProjectSubTasks { get; set; }
        public DbSet<ProjectTaskAsignee> ProjectTaskAsingees { get; set; }
        public DbSet<ProjectTaskComment> ProjectTaskComments{get;set;}
        public DbSet<ProjectTaskCommentReply> ProjectTaskCommentReplies { get; set; }
        public DbSet<ProjectTaskLog> ProjectTaskLogs { get; set; }

  

        public void MigrateDb()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.EnsureCreated());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeVMTut.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("EmpDBContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobTag> JobTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobPost>()
                        .HasMany<JobTag>(t => t.JobTags)
                        .WithMany(p => p.JobPost)
                        .Map(PostTags =>
                                    {
                                        PostTags.MapLeftKey("JobPostRefID");
                                        PostTags.MapRightKey("JobTagRefID");
                                        PostTags.ToTable("PostTags");
                                    });
        }
    }
}
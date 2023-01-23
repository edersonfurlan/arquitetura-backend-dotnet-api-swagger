﻿using curso.api.Business.Entities;
using curso.api.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace curso.api.Infrastructure.Data
{
    public class CourseDBContext : DbContext
    {
        public CourseDBContext(DbContextOptions<CourseDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Course> Course { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class QuizAppContext : DbContext
    {
        public QuizAppContext(DbContextOptions<QuizAppContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

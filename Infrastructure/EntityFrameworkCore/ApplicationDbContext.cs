using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkCore
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(category => category.HasMany(x => x.Questions).WithOne(x => x.Category));
            modelBuilder.Entity<Question>(question =>
            {
                question.HasMany(x => x.Answers).WithOne(x => x.Question);
                question.Ignore(x => x.CorrectAnswers);
                question.Ignore(x => x.IncorrectAnswers);
            });
        }

        public DbSet<Category> Categories { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJsTest.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Post> Posts { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostRubric>().HasKey(k => new { k.PostId, k.RubricId });

            modelBuilder.Entity<PostRubric>().HasOne(k => k.Post).WithMany(k => k.PostRubrics).HasForeignKey(k => k.PostId);

            modelBuilder.Entity<PostRubric>().HasOne(k => k.Rubric).WithMany(k => k.PostRubrics).HasForeignKey(k => k.RubricId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=183125;Database=vuejsef");
    }
}

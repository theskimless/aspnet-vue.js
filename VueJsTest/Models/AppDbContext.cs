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
        public DbSet<PostRubric> PostRubrics { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=183125;Database=vuejsef");
    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Wordle.Data.Entities;
using Wordle.Data.SeedData;

namespace Wordle.Data
{
    public class WordleDbContext : DbContext
    {
        public WordleDbContext(DbContextOptions<WordleDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasData(new WordData().Words);
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }

    }
}

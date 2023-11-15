using Microsoft.EntityFrameworkCore;
using Domain.Models;


namespace Infractructure.Data
{
    public class DataBaseContext:DbContext
    {

        public DbSet<Actor> Actor { get; set; }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Billboard> Billboard { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Movie>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Billboard>().Property(p => p.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.JWT;

namespace Infractructure.Data
{
    public class DataBaseContext:DbContext
    {

        public DbSet<Actor> Actor { get; set; }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Billboard> Billboard { get; set; }

        public DbSet<Auth> Auths { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Movie>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Billboard>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Auth>().Property(p => p.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}

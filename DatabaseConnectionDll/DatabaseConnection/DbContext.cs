using DatabaseConnection.entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
        public class GenericContext<T> : DbContext where T : Entity
        {
            public DbSet<T> Entity { get; set; }

            public GenericContext()
            {
                Database.EnsureCreated();
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=traintickets;");
                base.OnConfiguring(optionsBuilder);
            }
        }
}
using CodeChallenge.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeChallenge.Data.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasIndex(e => new { e.ClientGuid, e.Code }).IsUnique();
            });
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.DocNumber).IsUnique();
                
            });
        }
    }
}

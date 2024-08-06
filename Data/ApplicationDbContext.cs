using hendi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hendi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>()
                .HasIndex(g => g.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> students { get; set; }

        public DbSet<Guest> Guests { get; set; }
    }
}

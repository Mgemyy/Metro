using MetroApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MetroApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Subscription>()
                .HasOne(s => s.StartStation)
                .WithMany(st => st.StartSubscriptions)
                .HasForeignKey(s => s.StartStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subscription>()
                .HasOne(s => s.EndStation)
                .WithMany(st => st.EndSubscriptions)
                .HasForeignKey(s => s.EndStationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Subscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Subscription>()
                .HasOne(s => s.ReviewedByAdmin)
                .WithMany()
                .HasForeignKey(s => s.ReviewedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Stations
            builder.Entity<Station>().HasData(
                new Station { Id = 1, Name = "Helwan", LineNumber = 1 },
                new Station { Id = 2, Name = "Maadi", LineNumber = 1 },
                new Station { Id = 3, Name = "Sadat", LineNumber = 1 },
                new Station { Id = 4, Name = "Shohadaa", LineNumber = 1 },
                new Station { Id = 5, Name = "New El-Marg", LineNumber = 1 },
                new Station { Id = 6, Name = "Shubra El-Kheima", LineNumber = 2 },
                new Station { Id = 7, Name = "Cairo University", LineNumber = 2 },
                new Station { Id = 8, Name = "Giza", LineNumber = 2 },
                new Station { Id = 9, Name = "El-Mounib", LineNumber = 2 },
                new Station { Id = 10, Name = "Adly Mansour", LineNumber = 3 },
                new Station { Id = 11, Name = "Abbassia", LineNumber = 3 },
                new Station { Id = 12, Name = "Attaba", LineNumber = 3 },
                new Station { Id = 13, Name = "Kit Kat", LineNumber = 3 }
            );
        }
    }
}

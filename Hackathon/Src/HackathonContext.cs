using Hackathon.Entities;
using Microsoft.EntityFrameworkCore;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon;

public class HackathonContext : DbContext
{
    public DbSet<HackathonEntity> Hackathons { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<TeamEntity> Teams { get; set; }
    public DbSet<WishlistEntity> Wishlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamEntity>()
            .HasOne(t => t.TeamLead)
            .WithMany(e => e.Teams)
            .HasForeignKey(t => t.TeamLeadId);

        modelBuilder.Entity<TeamEntity>()
            .HasOne(t => t.Junior)
            .WithMany(e => e.Teams)
            .HasForeignKey(t => t.JuniorId);

        modelBuilder.Entity<WishlistEntity>()
            .HasOne(w => w.Employee)
            .WithMany(e => e.Wishlists)
            .HasForeignKey(w => w.EmployeeId);

        modelBuilder.Entity<WishlistEntity>()
            .HasOne(w => w.PreferredEmployee)
            .WithMany()
            .HasForeignKey(w => w.PreferredEmployeeId);
    }
}

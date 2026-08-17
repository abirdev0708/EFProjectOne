// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)           // each User has one Role
            .WithMany(r => r.Users)        // each Role has many Users
            .HasForeignKey(u => u.RoleId)  // the FK column is RoleId
            .OnDelete(DeleteBehavior.Restrict);   // prevent deleting a Role that still has Users

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "Admin", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) },
            new Role { Id = 2, RoleName = "User", IsActive = true, CreatedAt = new DateTime(2026, 1, 1) }
        );

        base.OnModelCreating(modelBuilder);
    }
}
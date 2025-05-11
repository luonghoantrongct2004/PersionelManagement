using BE.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BE.DAL.DBContext;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        modelBuilder.Entity<Transaction>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId);
        modelBuilder.Entity<Budget>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(b => b.CategoryId);

        base.OnModelCreating(modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }
    public DbSet<Users>Users { get; set; }
    public DbSet<TaskList>TaskList { get; set; }
    public DbSet<TaskItem>TaskItem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e=>e.UserId);
            entity.HasIndex(e=>e.Email).IsUnique();
            entity.Property(e=>e.Email).IsRequired();
        });

        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e=>e.Users)
                .WithMany(u=>u.TaskList)
                .HasForeignKey(e=>e.UserId);
        });

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e=>e.TaskList)
                .WithMany(tl=>tl.ListTaskItem)
                .HasForeignKey(e=>e.TaskListId);
        });

    }
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Restaurant
        modelBuilder.Entity<Restaurant>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Restaurant>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Menus)
            .WithOne(m => m.Restaurant)
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Orders)
            .WithOne(o => o.Restaurant)
            .HasForeignKey(o => o.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Menu
        modelBuilder.Entity<Menu>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Menu>()
            .Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Menu>()
            .HasMany(m => m.OrderDetails)
            .WithOne(od => od.MenuItem)
            .HasForeignKey(od => od.MenuItemId)
            .OnDelete(DeleteBehavior.Cascade);

        // User
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Courier)
            .WithOne(c => c.User)
            .HasForeignKey<Courier>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order 
        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderDetail 
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => od.Id);

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.Quantity)
            .IsRequired();

        // Courier 
        modelBuilder.Entity<Courier>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Courier>()
            .HasOne(c => c.User)
            .WithOne(u => u.Courier)
            .HasForeignKey<Courier>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

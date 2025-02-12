using Microsoft.EntityFrameworkCore;
using MyCity.DataAccess.Models;

namespace MyCity.DataAccess;

public class ApplicationContext : DbContext
{
    public DbSet<Location> Locations { get; set; }
    
    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyCity;Username=postgres;Password=123");
    }
}
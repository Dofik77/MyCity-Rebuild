using Microsoft.EntityFrameworkCore;
using MyCity.DataAccess.Models;

namespace MyCity.DataAccess;

public class ApplicationContext : DbContext
{
    public DbSet<Location> Locations { get; set; }
    
    public ApplicationContext()
    {

    }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }
}
using Microsoft.EntityFrameworkCore;

namespace Models.DataModels;

public class DataContext : DbContext
{


    public DataContext(DbContextOptions options)
        : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}

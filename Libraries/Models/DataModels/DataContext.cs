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

        modelBuilder.Entity<OrderRecord>(action =>
        {
            action.HasOne(x => x.order).WithMany(x => x.orderRecords).HasForeignKey(x => x.OrderId);
        });


        var baseDataModelType = typeof(BaseDataModel);
        var types = baseDataModelType.Assembly.GetExportedTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && t != baseDataModelType && baseDataModelType.IsAssignableFrom(t));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }
}

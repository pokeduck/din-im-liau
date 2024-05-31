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

        modelBuilder.Entity<DrinkToppingRelation>(action =>
        {
            action.HasKey(x => new { x.DrinkId, x.ToppingId });
        });
        modelBuilder.Entity<DrinkIceRelation>(action =>
        {
            action.HasKey(x => new { x.DrinkId, x.IceId });
        });
        modelBuilder.Entity<DrinkSuggerRelation>(action =>
        {
            action.HasKey(x => new { x.DrinkId, x.SuggerId });
        });

        modelBuilder.Entity<Account>(action =>
        {
            action.HasMany(x => x.Orders).WithOne(x => x.Admin).HasForeignKey(x => x.AdminId).OnDelete(DeleteBehavior.NoAction);
            action.HasMany(x => x.OrderRecords).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Account>(action =>
        {

        });


        // modelBuilder.Entity<OrderRecord>(action =>
        // {
        //     action.HasOne(x => x.order).WithMany(x => x.orderRecords).HasForeignKey(x => x.OrderId);
        // });


        var baseDataModelType = typeof(BaseDataModel);
        var types = baseDataModelType.Assembly.GetExportedTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && t != baseDataModelType && baseDataModelType.IsAssignableFrom(t));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }
}

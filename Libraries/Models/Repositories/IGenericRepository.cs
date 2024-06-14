

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models.DataModels;

namespace Models.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseDataModel
{
    public DatabaseFacade Database { get; }
    public DbSet<TEntity> DBSetTable { get; }
    public IQueryable<TEntity> Table { get; }


}

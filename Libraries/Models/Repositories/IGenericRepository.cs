

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Models.DataModels;

namespace Models.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseDataModel
{
    public DatabaseFacade Database { get; }
    public DbSet<TEntity> DBSetTable { get; }
    public IQueryable<TEntity> NoTrackingTable { get; }


    public Task Create(TEntity entity, bool saveImediately = true);
    public Task<TEntity?> ReadFirst(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? order = null,
            bool asNoTracking = true
        );

    public Task<TEntity?> ReadFirstById(
            int id,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? order = null,
            bool asNoTracking = true
            );

    public Task Update(TEntity entity, bool saveImediately = true);

    public Task Delete(TEntity entity, bool saveImediately = true);
}

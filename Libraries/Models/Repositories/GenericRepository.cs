

using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Models.DataModels;

namespace Models.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseDataModel
{

    private readonly DataContext _dataContext;
    private readonly HttpContext _httpContext;
    private readonly ClaimsPrincipal _userClaims;

    public GenericRepository(DataContext dataContext,
      IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _httpContext = httpContextAccessor.HttpContext!;
        _userClaims = _httpContext.User;
    }
    public DatabaseFacade Database => _dataContext.Database;

    public DbSet<TEntity> DBSetTable => _dataContext.Set<TEntity>();
    public IQueryable<TEntity> NoTrackingTable => _dataContext.Set<TEntity>().AsNoTracking();

    private IQueryable<TEntity> Query(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? order = null,
        bool asNoTracking = true
        )
    {
        var query = asNoTracking ? NoTrackingTable : DBSetTable;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (order != null)
        {
            query = order(query);
        }


        return query;
    }


    public async Task Create(TEntity entity, bool saveImediately = true)
    {
        var time = DateTime.Now.ToTimestamp();
        if (entity is ICreateEntity createEntity)
        {
            if (createEntity.CreateTime == default)
                createEntity.CreateTime = time;
        }
        if (entity is IUpdateEntity updateEntity)
        {
            if (updateEntity.UpdateTime == default)
                updateEntity.UpdateTime = time;
        }

        _dataContext.Add(entity);

        if (saveImediately)
        {
            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task<TEntity?> ReadFirst(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? order = null,
        bool asNoTracking = true
    )
    {
        return await Query(predicate, include, order, asNoTracking).FirstOrDefaultAsync();
    }

    public async Task<TEntity?> ReadFirstById(
        int id,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? order = null,
        bool asNoTracking = true
        )
    {
        return await Query(predicate, include, order, asNoTracking).FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task Update(TEntity entity, bool saveImediately = true)
    {
        if (entity is IUpdateEntity updateEntity)
        {
            updateEntity.UpdateTime = DateTime.Now.ToTimestamp();
        }
        _dataContext.Update(entity);

        if (saveImediately)
        {
            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task Delete(TEntity entity, bool saveImediately = true)
    {
        _dataContext.Remove(entity);

        if (saveImediately)
            await _dataContext.SaveChangesAsync();
    }

}

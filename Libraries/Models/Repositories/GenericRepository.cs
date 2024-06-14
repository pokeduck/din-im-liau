

using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Models.DataModels;

namespace Models.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseDataModel
{

    private readonly DataContext _dataContext;
    private readonly HttpContext _httpContext;
    private readonly ClaimsPrincipal _userClaims;
    private readonly ServiceProvider _serviceProvider;

    public GenericRepository(DataContext dataContext, ServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _serviceProvider = serviceProvider;
        _httpContext = httpContextAccessor.HttpContext!;
        _userClaims = _httpContext.User;
    }
    public DatabaseFacade Database => _dataContext.Database;

    public DbSet<TEntity> DBSetTable => _dataContext.Set<TEntity>();
    public IQueryable<TEntity> Table => _dataContext.Set<TEntity>().AsNoTracking();
}

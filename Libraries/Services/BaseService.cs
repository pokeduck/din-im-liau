

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models.DataModels;
using Models.Repositories;

namespace Services;

public class BaseService<T> where T : BaseDataModel
{
    public HttpContext _httpContext;
    public IGenericRepository<T>? _repository;

    public BaseService(IHttpContextAccessor contextAccessor)
    {
        _httpContext = contextAccessor.HttpContext!;
        _repository = _httpContext.RequestServices.GetService<IGenericRepository<T>>();
    }
}



using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models.DataModels;
using Models.Repositories;
using Services.Extensions;

namespace Services;

public class BaseService<T> where T : BaseDataModel
{
    protected readonly HttpContext HttpContext;
    protected readonly IGenericRepository<T> Repository;

    protected readonly IMapper Mapper;

    protected Account Account => HttpContext.GetAccount();

    public BaseService(IHttpContextAccessor contextAccessor)
    {
        HttpContext = contextAccessor.HttpContext!;
        Repository = HttpContext.RequestServices.GetService<IGenericRepository<T>>()!;
        Mapper = HttpContext.RequestServices.GetService<IMapper>()!;
    }
}

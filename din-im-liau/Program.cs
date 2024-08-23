using Services.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using din_im_liau.Events;
using Amazon.S3;
using Models.Repositories;
using Services;
using Scrutor;
using Microsoft.Extensions.FileProviders;
using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using din_im_liau.Middlewares;
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var mvcBuilder = builder.Services.AddRazorPages();


    if (builder.Environment.IsDevelopment())
    {
        mvcBuilder.AddRazorRuntimeCompilation();
    }


    builder.Services.AddServices();



    builder.Services.AddRouting(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
    {
        opt.Cookie.Name = "dim_in_liau_oauth_token";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        opt.LoginPath = "/user/login";
        opt.LogoutPath = "/";
        opt.AccessDeniedPath = "/error/401";
        opt.EventsType = typeof(CustomCookieAuthenticationEvents);
    });
    builder.Services.AddScoped<CustomCookieAuthenticationEvents>();



    builder.Services.AddHttpClient();

    builder.Services.AddHsts(options =>
    {
        options.Preload = true;
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromSeconds(30);
    });

    builder.Services.AddDbContext<DataContext>(options =>
    {
        var dbString = builder.Configuration.GetConnectionString("MySql");
        options.UseMySql(dbString, ServerVersion.Create(new Version(8, 0, 0), ServerType.MariaDb), mySqlOption =>
        {
            mySqlOption.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
        });
    });

    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

    builder.Services.RegisterBaseServices();


    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



    //把Validation 自動載入
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<AuthRegisterValidator>();

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ErrorMessageFilter));
    })
    .ConfigureApiBehaviorOptions(options =>
    {

        options.SuppressModelStateInvalidFilter = true;
        //重新格式化錯誤訊息
        // options.InvalidModelStateResponseFactory = factory =>
        // {
        //     var errors = factory.ModelState.Values
        //     .Where(x => x.Errors.Count > 0)
        //     .SelectMany(x => x.Errors)
        //     .Select(x => x.ErrorMessage);
        //     var errorsString = string.Join('\n', errors);

        //     return new BadRequestObjectResult(new { ErrorCode = 0, Message = errorsString });
        // };
    }).AddJsonOptions(options =>
    {
        //將 json 輸出轉換成 snake case
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
    });




    //版本控制v1,v2,v3...
    builder.Services.AddApiVersioning(
        opt =>
        {
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
        }
    );

    // builder.Services.AddVersionedApiExplorer(options =>
    // {
    //     // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    //     // note: the specified format code will format the version as "'v'major[.minor][-status]"
    //     options.GroupNameFormat = "'v'VVV";

    //     // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    //     // can also be used to control the format of the API version in route templates
    //     options.SubstituteApiVersionInUrl = true;
    // });

    // builder.Services.AddHttpsRedirection(options =>
    // {
    //     options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    //     options.HttpsPort = 7017;
    // });

    var app = builder.Build();




    // using var scope = app.Services.CreateScope();
    // var service = scope.ServiceProvider;
    // var repo = service.GetRequiredService<IGenericRepository<Account>>();
    // Console.WriteLine(repo);
    // using var scope = app.Services.CreateScope();
    // var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    // db.Database.Migrate();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/error-development");

    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHsts();

    }

    app.UseMiddleware<GlobalExceptionMiddleware>();

    app.UseHttpsRedirection();
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();


    app.MapRazorPages();
    app.MapControllers();
    app.Run();

    return 0;

}
catch (Exception ex)
{
    Console.WriteLine(ex + "Host terminated unexpectedly.");
    return 1;
}
finally
{
    Console.WriteLine("Finally");
}


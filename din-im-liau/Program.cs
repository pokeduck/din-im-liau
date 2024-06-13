using Services.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using din_im_liau.Events;
using Amazon.S3;
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();

    builder.Services.AddServices();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

    // builder.Services.AddHttpsRedirection(options =>
    // {
    //     options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    //     options.HttpsPort = 7017;
    // });

    var app = builder.Build();

    // using var scope = app.Services.CreateScope();
    // var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    // db.Database.Migrate();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

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


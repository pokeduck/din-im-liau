using Services.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Models.DataModels;
using Microsoft.EntityFrameworkCore;
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();

    builder.Services.AddServices();

    builder.Services.AddRouting(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

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
        options.UseMySql(dbString, new MySqlServerVersion(new Version(8, 8, 8)));

    });

    // builder.Services.AddHttpsRedirection(options =>
    // {
    //     options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    //     options.HttpsPort = 7017;
    // });

    var app = builder.Build();

    //using var scope = app.Services.CreateScope();
    //var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    //db.Database.Migrate();

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


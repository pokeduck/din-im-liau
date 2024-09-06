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
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using din_im_liau.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning.ApiExplorer;
using Models.Settings;
using System.ComponentModel;

try
{
    var builder = WebApplication.CreateBuilder(args);


    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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


    builder.Services.AddOptions<JwtSetting>()
    .Configure<IConfiguration>((settings, config) => config.GetSection("JwtSettings").Bind(settings));


    builder.Services.AddAuthentication(config =>
    {
        config.DefaultScheme = "smart";
        config.DefaultChallengeScheme = "smart";
    })
    .AddPolicyScheme("smart", "Bearer or Jwt", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            //var bearerAuth = context.Request.Headers["Authorization"].FirstOrDefault()?.StartsWith("Bearer ") ?? false;
            // You could also check for the actual path here if that's your requirement:
            // eg: if (context.HttpContext.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCulture))
            if (context.Request.Headers["Accept"].ToString() == "application/json")
            {
                return JwtBearerDefaults.AuthenticationScheme;
            }
            else
            {
                return CookieAuthenticationDefaults.AuthenticationScheme;
            }
        };
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.Cookie.Name = "dim_in_liau_oauth_token";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        opt.LoginPath = "/user/login";
        opt.LogoutPath = "/";
        opt.AccessDeniedPath = "/error/401";
        opt.EventsType = typeof(CustomCookieAuthenticationEvents);
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

            // 一般我們都會驗證 Issuer
            ValidateIssuer = false,
            ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),

            // 通常不太需要驗證 Audience
            ValidateAudience = false,
            //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

            // 一般我們都會驗證 Token 的有效期間
            ValidateLifetime = true,

            // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
            ValidateIssuerSigningKey = false,

            // "1234567890123456" 應該從 IConfiguration 取得
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:Key")!))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                // Call this to skip the default logic and avoid using the default response
                context.HandleResponse();
                var message = "";
                var bearerToken = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(bearerToken))
                {
                    message = "You need a Bearer token.";
                }
                else if (bearerToken.Contains("Bearer"))
                {
                    message = "The token is invalid.";
                }
                else
                {
                    message = "You are not authorized!";
                }
                var statusCode = 401;

                var errorCode = 401;
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var response = JsonSerializer.Serialize(new { ErrorCode = errorCode, ErrorMessage = message }, jsonSerializerOptions);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(response);
            },
            OnAuthenticationFailed = context =>
            {
                context.Fail("You are no authorized!!!!");
                return Task.CompletedTask;
            },
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder(
            CookieAuthenticationDefaults.AuthenticationScheme,
            JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
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
        options.Filters.Add(typeof(BearerJwtFilter));
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
    ).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // builder.Services.AddVersionedApiExplorer(options =>
    // {
    //     // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    //     // note: the specified format code will format the version as "'v'major[.minor][-status]"
    //     options.GroupNameFormat = "'v'VVV";

    //     // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    //     // can also be used to control the format of the API version in route templates
    //     options.SubstituteApiVersionInUrl = true;
    // });
    //services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureApiVersionSwaggerGenOptions>();
    builder.Services.AddEndpointsApiExplorer();
    // builder.Services.AddHttpsRedirection(options =>
    // {
    //     options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    //     options.HttpsPort = 7017;
    // });

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Din In Liau",
            Description = "API Doc",
            TermsOfService = new Uri("https://google.com.tw"),
            Contact = new OpenApiContact
            {
                Name = "BBB",
                Email = "BBB@dindin.tw",
                Url = new Uri("https://githib.com")
            }
        });



        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Desc."
        });

        options.UseInlineDefinitionsForEnums();
        options.EnableAnnotations();

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });


        // 讀取每一隻API註解的說明內容
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        options.OrderActionsBy(api => api.RelativePath);
        options.SchemaFilter<SwaggerOrderSchemaFilter>();
    });

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
        app.UseSwagger();
        app.UseSwaggerUI();
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

    // app.UseEndpoints(endpoints =>
    //     endpoints.Map("/", context => Task.Run(() => context.Response.Redirect("/swagger/index.html")))
    // );

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


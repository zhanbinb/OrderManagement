using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;
using OrderManagement.Api.Data;
using OrderManagement.Api.Exceptions;
using OrderManagement.Api.HealthChecks;
using OrderManagement.Api.Services;
using OrderManagement.Api.Services.Orders;
using OrderManagement.Api.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
Console.WriteLine($"当前环境: {environment}");

// Serilog 日志配置
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File(
            path: "logs/order-.log",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30,
            fileSizeLimitBytes: 10 * 1024 * 1024,
            rollOnFileSizeLimit: true
        );
});

// Add services to the container.

builder.Services.AddControllers();

// builder.Services.AddTransient<IOperationService,OperationService>();
// builder.Services.AddScoped<IOperationService, OperationService>();
// builder.Services.AddSingleton<IOperationService, OperationService>();

builder.Services.AddTransient<ITransientOperationService, OperationService>();

builder.Services.AddScoped<IScopedOperationService, OperationService>();

builder.Services.AddSingleton<ISingletonOperationService, OperationService>();

builder.Services.AddScoped<IOrderService, OrderService>();

//注册订单配置
builder.Services.Configure<OrderSettings>(builder.Configuration.GetSection("OrderSettings"));

//注册EF Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//注册全局异常
builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

//注册JWT认证
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

//注册内存缓存
builder.Services.AddMemoryCache();

//注册redis
var redisConnection = builder.Configuration
                          .GetConnectionString("Redis")
                      ?? throw new InvalidOperationException("Redis connection string missing");

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnection;
    options.InstanceName = "OrderManagement:";
});

//注册限流
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("order-api", httpcontext =>
    {
        var ipAddress = httpcontext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: ipAddress, factory =>
            new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            });
    });

});

//注册Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>     
    {
        policy.WithOrigins( 
                "http://localhost:5173",
                "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//注册HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("api is running"),
        tags: ["live"])
    .AddCheck<PostgreSqlHealthCheck>("PostgreSQL", tags: ["ready"])
    .AddCheck<RedisHealthCheck>("Redis", tags: ["ready"]);
    


var jwtSettings = builder.Configuration
                      .GetSection("Jwt")
                      .Get<JwtSettings>()
                  ?? throw    new InvalidOperationException("JWT 配置缺失");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey =
                new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            //允许时钟偏差
            ClockSkew = TimeSpan.FromSeconds(30),

            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name
        };
    });

// 注册授权策略
builder.Services.AddAuthorization(options =>
{
    // 只有 Admin 角色可以访问
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

    // 必须拥有订单读取权限
    options.AddPolicy("OrderRead", policy =>
        policy.RequireClaim("permission", "order.read"));

    // 必须同时满足 Admin 角色和订单管理权限
    options.AddPolicy("OrderManage", policy =>
        policy.RequireRole("Admin")
            .RequireClaim("permission", "order.manage"));
});

// 3. 开启 DI 生命周期验证
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi(options =>
{
    // 1. 注册 JWT Bearer 安全认证方案
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes =
            new Dictionary<string, IOpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "请输入 JWT Access Token"
                }
            };

        return Task.CompletedTask;
    });

    // 2. 只给需要认证的接口添加 JWT 安全要求
    options.AddOperationTransformer((operation, context, cancellationToken) =>
    {
        var metadata = context.Description.ActionDescriptor.EndpointMetadata;

        // 没有 [Authorize] 的接口不需要 JWT
        var requiresAuthorization = metadata
            .OfType<IAuthorizeData>()
            .Any();

        // [AllowAnonymous] 接口不需要 JWT
        var allowsAnonymous = metadata
            .OfType<IAllowAnonymous>()
            .Any();

        if (requiresAuthorization && !allowsAnonymous)
        {
            operation.Security ??= [];

            operation.Security.Add(
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(
                        "Bearer",
                        context.Document)] = []
                });
        }

        return Task.CompletedTask;
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "OrderManagement API v1");
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("Frontend");

app.UseRateLimiter();

app.UseExceptionHandler();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();

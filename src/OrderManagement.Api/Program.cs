using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Data;
using OrderManagement.Api.Exceptions;
using OrderManagement.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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

//注册订单配置
builder.Services.Configure<OrderSettings>(builder.Configuration.GetSection("OrderSettings"));

//注册EF Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//注册全局异常
builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// 3. 开启 DI 生命周期验证
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();

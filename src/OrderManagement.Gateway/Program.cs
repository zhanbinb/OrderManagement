using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Discovery.Consul;
using Ocelot.Middleware;
using OrderManagement.Gateway;
using OrderManagement.Gateway.Services;

var builder = WebApplication.CreateBuilder(args);


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

//加载Ocelot路由配置
builder.Configuration.AddJsonFile("ocelot.json",optional: false, reloadOnChange: true);

//注册Ocelot 服务
builder.Services.AddOcelot(builder.Configuration).AddConsul().AddConsul<CustomConsulServiceBuilder>();



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

//配置Ocelot 路由
await app.UseOcelot();

app.Run();

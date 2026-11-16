using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Api.DTOs.Auth;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
   private readonly JwtSettings _jwtSettings;
   
   public AuthController(IOptions<JwtSettings> jwtSettings)
   {
       _jwtSettings = jwtSettings.Value;
   }

   [HttpPost("login")]
   public IActionResult Login(LoginRequest request)
   {
       if (request.Username != "admin" || request.Password != "123456")
       {
           return Unauthorized(new { message = "用户名或密码错误" });
       }

       var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

       //用户身份信息
       var claims = new[]
       {
           new Claim(ClaimTypes.NameIdentifier, "1"),
           new Claim(ClaimTypes.Name, request.Username),
           new Claim(ClaimTypes.Role, "Admin"),
           new Claim("permission", "order.read"),
           new Claim("permission", "order.manage")
       };
       //创建签名秘钥
       var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
       var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

       //创建JWT令牌
       var token = new JwtSecurityToken(
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience,
           claims: claims,
           expires: expiresAt,
           signingCredentials: credentials);
       //生成字符串
       var tokenString = new JwtSecurityTokenHandler()
           .WriteToken(token);

       return Ok((new LoginResponse
       {
           AccessToken = tokenString,
           TokenType = "Bearer",
           ExpiresAt = expiresAt
       }));
   }
}
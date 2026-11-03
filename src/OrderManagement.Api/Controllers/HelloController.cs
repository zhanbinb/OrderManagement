using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return  Ok(new
        {
            message = "Hello ASP.NET Core 10!"
        });
    }
}
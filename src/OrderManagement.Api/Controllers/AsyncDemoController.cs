using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.Controllers;

public class AsyncDemoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
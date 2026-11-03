using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly OrderSettings _settings;

    public SettingsController(IOptions<OrderSettings> settings)
    {
        _settings = settings.Value;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(new
        {
            SystemName = _settings.SystemName,
            MaxItemsPerOrder = _settings.MaxItemsPerOrder,
            EnableAutoCancel = _settings.EnableAutoCancel,
            AutoCancelMinutes = _settings.AutoCancelMinutes
        });
    }
}
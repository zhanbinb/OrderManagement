using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrderManagement.Api.Settings;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OptionsExperimentController : ControllerBase
{
    private readonly IOptions<OrderSettings> _options;
    private readonly IOptionsSnapshot<OrderSettings> _snapshot;
    private readonly IOptionsMonitor<OrderSettings> _monitor;

    public OptionsExperimentController(
        IOptions<OrderSettings> options,
        IOptionsSnapshot<OrderSettings> snapshot,
        IOptionsMonitor<OrderSettings> monitor)
    {
        _options = options;
        _snapshot = snapshot;
        _monitor = monitor;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // TODO：分别读取三个 Options 的 MaxItemsPerOrder

        return Ok(new
        {
            Options = _options.Value.MaxItemsPerOrder,
            Snapshot = _snapshot.Value.MaxItemsPerOrder,
            Monitor = _monitor.CurrentValue.MaxItemsPerOrder
        });
    }
}
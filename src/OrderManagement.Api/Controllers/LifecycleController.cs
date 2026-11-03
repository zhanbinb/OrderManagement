using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Services;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LifecycleController : ControllerBase
{

    private readonly ITransientOperationService _transient;
    private readonly IScopedOperationService _scoped;
    private readonly ISingletonOperationService _singleton;
    
    private readonly IServiceProvider _serviceProvider;
    
    //构造函数注入
    public LifecycleController(ITransientOperationService transient, IScopedOperationService scoped, ISingletonOperationService singleton, IServiceProvider serviceProvider)
    {
        _transient = transient;
        _scoped = scoped;
        _singleton = singleton;
        _serviceProvider = serviceProvider;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
       // var result = new
       // {
       //     Transient1 = _transient.GetOperationId(),
       //     Transient2 = _transient.GetOperationId(),
       //
       //     Scoped1 = _scoped.GetOperationId(),
       //     Scoped2 = _scoped.GetOperationId(),
       //
       //     Singleton1 = _singleton.GetOperationId(),
       //     Singleton2 = _singleton.GetOperationId()
       // };
       // return Ok(result);
       var transient1 = _serviceProvider
           .GetRequiredService<ITransientOperationService>();

       var transient2 = _serviceProvider
           .GetRequiredService<ITransientOperationService>();

       var scoped1 = _serviceProvider
           .GetRequiredService<IScopedOperationService>();

       var scoped2 = _serviceProvider
           .GetRequiredService<IScopedOperationService>();

       var singleton1 = _serviceProvider
           .GetRequiredService<ISingletonOperationService>();

       var singleton2 = _serviceProvider
           .GetRequiredService<ISingletonOperationService>();

       return Ok(new
       {
           Transient1 = transient1.GetOperationId(),
           Transient2 = transient2.GetOperationId(),

           Scoped1 = scoped1.GetOperationId(),
           Scoped2 = scoped2.GetOperationId(),

           Singleton1 = singleton1.GetOperationId(),
           Singleton2 = singleton2.GetOperationId()
       });
    }
}
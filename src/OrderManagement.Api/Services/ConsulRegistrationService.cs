using Consul;

public class ConsulRegistrationService : IHostedService
{
    private readonly IConsulClient _consul;
    private readonly IConfiguration _configuration;

    public ConsulRegistrationService(
        IConsulClient consul,
        IConfiguration configuration)
    {
        _consul = consul;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var serviceId = _configuration["Consul:ServiceId"]!;
        var serviceName = _configuration["Consul:ServiceName"]!;
        var address = _configuration["Consul:ServiceAddress"]!;
        var port = int.Parse(_configuration["Consul:ServicePort"]!);
        var healthCheckAddress =
            _configuration["Consul:HealthCheckAddress"]!;

        var registration = new AgentServiceRegistration
        {
            ID = serviceId,
            Name = serviceName,
            Address = address,
            Port = port,

            Check = new AgentServiceCheck
            {
                HTTP = healthCheckAddress,
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromSeconds(5),
                DeregisterCriticalServiceAfter =
                    TimeSpan.FromMinutes(1)
            }
        };

        await _consul.Agent.ServiceRegister(
            registration,
            cancellationToken);

        Console.WriteLine($"Consul 注册成功：{serviceName}");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var serviceId = _configuration["Consul:ServiceId"]!;

        await _consul.Agent.ServiceDeregister(
            serviceId,
            cancellationToken);

        Console.WriteLine($"Consul 注销成功：{serviceId}");
    }
}
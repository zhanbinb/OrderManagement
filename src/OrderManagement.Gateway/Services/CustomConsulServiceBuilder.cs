using Consul;
using Ocelot.Discovery.Consul;
using Ocelot.Logging;

namespace OrderManagement.Gateway.Services;

public class CustomConsulServiceBuilder
    : DefaultConsulServiceBuilder
{
    public CustomConsulServiceBuilder(
        IHttpContextAccessor contextAccessor,
        IConsulClientFactory clientFactory,
        IOcelotLoggerFactory loggerFactory)
        : base(contextAccessor, clientFactory, loggerFactory)
    {
    }

    protected override string GetDownstreamHost(
        ServiceEntry entry,
        Node node)
    {
        return entry.Service.Address;
    }
}
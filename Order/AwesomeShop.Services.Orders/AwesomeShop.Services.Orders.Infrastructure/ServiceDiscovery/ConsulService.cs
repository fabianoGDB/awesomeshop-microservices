using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;

namespace AwesomeShop.Services.Orders.Infrastructure.ServiceDiscovery
{
    public class ConsulService : IServiceDiscoveryService
    {
        private readonly IConsulClient _consulClient;

        public ConsulService(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        public async Task<Uri> GetServiceUri(string serviceName, string requestUrl)
        {
            var allRegistredService = await _consulClient.Agent.Services();

            var registeredServices = allRegistredService.Response?
                                        .Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                                        .Select(s => s.Value)
                                        .ToList();

            var service = registeredServices.First();

            Console.WriteLine(service.Address);

            var uri = $"https://{service.Address}:{service.Port}/{requestUrl}";

            return new Uri(uri);
        }
    }
}
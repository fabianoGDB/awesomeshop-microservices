using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AwesomeShop.Services.Orders.Api.Extension
{
    public static class Extensions
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var resgistration = new AgentServiceRegistration
            {
                ID = "order-service",
                Name = "OrderServices",
                Address = "localhost",
                Port = 5001
            };

            consulClient.Agent.ServiceDeregister(resgistration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(resgistration).ConfigureAwait(true);

            lifeTime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(resgistration.ID).ConfigureAwait(true);
                Console.WriteLine("Service deregisted in Consul");
            });

            return app;

        }
    }
}
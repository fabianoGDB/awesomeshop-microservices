using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Application.Commands;
using AwesomeShop.Services.Orders.Application.Subscribers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Orders.Application.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AddOrder));

            return services;
        }

        public static IServiceCollection AddSubscribers(this IServiceCollection services)
        {
            services.AddHostedService<PaymentAcceptedSubscriber>();

            return services;
        }

        public static string ToDashCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }

            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('-');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }

                Console.WriteLine(sb.ToString());
            }



            return sb.ToString();
        }
    }
}
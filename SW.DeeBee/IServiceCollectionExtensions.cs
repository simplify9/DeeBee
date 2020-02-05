using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.DeeBee
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDeeBee(this IServiceCollection serviceCollection, Action<DeeBeeOptions> configure)
        {
            var deeBeeOptions = new DeeBeeOptions();
            configure.Invoke(deeBeeOptions);
            //serviceCollection.AddSingleton(deeBeeOptions);

            serviceCollection.AddScoped(serviceProvider => 
            {
                return new ConnectionHost(deeBeeOptions);
            });

            return serviceCollection;
        }
    }
}

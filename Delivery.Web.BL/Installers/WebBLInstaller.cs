
using Delivery.Common.BL.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Web.BL.Installers
{
    public class WebBLInstaller
    {
        public void Install(IServiceCollection serviceCollection, string apiBaseUrl)
        {
            serviceCollection.AddTransient<DishClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new DishClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<OrderClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new OrderClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<RestaurantClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new RestaurantClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<FilterClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new FilterClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<RevenueClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new RevenueClient(client, apiBaseUrl);
            });

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<WebBLInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }

        public HttpClient CreateApiHttpClient(IServiceProvider serviceProvider, string apiBaseUrl)
        {
            var client = new HttpClient() { BaseAddress = new Uri(apiBaseUrl) };
            client.BaseAddress = new Uri(apiBaseUrl);
            return client;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Delivery.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection)
            where TInstaller : IInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(serviceCollection);
        }
    }
}
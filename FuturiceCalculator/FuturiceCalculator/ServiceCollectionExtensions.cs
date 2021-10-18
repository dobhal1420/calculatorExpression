using FuturiceCalculator.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturiceCalculator
{

    /// <summary>
    /// Extensions methods for the <see cref="IServiceCollection"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add web application services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The configured service collection.</returns>
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services
                .AddTransient<IExpressionParserService,ExpressionParserService>();                

            return services;


        }
    }
}

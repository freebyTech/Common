using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using freebyTech.Common.Logging;
using freebyTech.Common.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using freebyTech.Common.Logging.FrameworkAgents;

namespace freebyTech.Common.ExtensionMethods
{
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// If using this ILogFrameworkAgent type then NLog dependencies must be added to your project.
        /// </summary>
        public static void AddNLogFrameworkAgent(this IServiceCollection services)
        {
            services.AddSingleton<ILogFrameworkAgent, NLogFrameworkAgent>();
        }

        /// <summary>
        /// If using this ILogFrameworkAgent type then Serilog dependencies must be added to your project.
        /// </summary>
        public static void AddSerilogFrameworkAgent(this IServiceCollection services)
        {
            services.AddSingleton<ILogFrameworkAgent, SerilogFrameworkAgent>();
        }

        /// <summary>
        /// Adds standard logging types to dependency injection, the specific ILogFrameworkAgent must be defined before this
        /// as that agent needs to be passed on to these logger types defined.
        /// </summary>
        public static void AddBasicLoggingServices(this IServiceCollection services, Assembly parentApplication, string applicationLogginId)
        {
            var serviceProvider = services.BuildServiceProvider();

            services.AddTransient<IBasicLogger, BasicLogger>((ctx) =>
            {
                return new BasicLogger(parentApplication, applicationLogginId, serviceProvider.GetService<ILogFrameworkAgent>());
            });

            services.AddTransient<IInstrumentationLogger, BasicInstrumentationLogger>((ctx) =>
            {
                return new BasicInstrumentationLogger(parentApplication, applicationLogginId, serviceProvider.GetService<ILogFrameworkAgent>());
            });

            services.AddTransient<IRunMetricsLogger, BasicRunMetricsLogger>((ctx) =>
            {
                return new BasicRunMetricsLogger(parentApplication, applicationLogginId, serviceProvider.GetService<ILogFrameworkAgent>());
            });

            services.AddTransient<IValidationLogger, BasicValidationLogger>((ctx) =>
            {
                return new BasicValidationLogger(parentApplication, applicationLogginId, serviceProvider.GetService<ILogFrameworkAgent>());
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using freebyTech.Common.Logging;
using freebyTech.Common.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using freebyTech.Common.Logging.FrameworkAgents.NLog;

namespace freebyTech.Common.ExtensionMethods
{
    public static class ServiceRegistrationExtensions
    {
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


        /// <summary>
        /// If using this ILogFrameworkAgent type then NLog must be used as a dependency.
        /// </summary>
        public static void AddNLogFrameworkAgent(this IServiceCollection services)
        {
            services.AddSingleton<ILogFrameworkAgent, NLogFrameworkAgent>();
        }
    }
}

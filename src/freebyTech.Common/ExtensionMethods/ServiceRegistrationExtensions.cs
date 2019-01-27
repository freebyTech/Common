﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using freebyTech.Common.Logging;
using freebyTech.Common.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace freebyTech.Common.ExtensionMethods
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddBasicLoggingServices(this IServiceCollection services, Assembly parentApplication, string applicationLogginId)
        {
            services.AddScoped<IBasicLogger, BasicLogger>((ctx) =>
            {
                return new BasicLogger(parentApplication, applicationLogginId);
            });

            services.AddScoped<IInstrumentationLogger, BasicInstrumentationLogger>((ctx) =>
            {
                return new BasicInstrumentationLogger(parentApplication, applicationLogginId);
            });

            services.AddScoped<IRunMetricsLogger, BasicRunMetricsLogger>((ctx) =>
            {
                return new BasicRunMetricsLogger(parentApplication, applicationLogginId);
            });

            services.AddScoped<IValidationLogger, BasicValidationLogger>((ctx) =>
            {
                return new BasicValidationLogger(parentApplication, applicationLogginId);
            });
        }
    }
}

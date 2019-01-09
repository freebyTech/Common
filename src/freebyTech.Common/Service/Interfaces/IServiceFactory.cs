using System;
using freebyTech.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace freebyTech.Common.Service.Interfaces
{
    public interface IServiceFactory
    {
        IServiceProvider ServiceProvider { get; }
        ILogger Logger { get;  }

        IService GetServiceSingleton();

        void ConfigureServices(IEnvironmentManager environmentManager, IExecutionEnvironment executionEnvironment, IServiceCollection services, string[] args);

        void ValidateAndExecuteService();
    }
}

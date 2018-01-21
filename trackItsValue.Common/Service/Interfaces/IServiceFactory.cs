using System;
using System.Collections.Generic;
using DasMulli.Win32.ServiceUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using trackItsValue.Common;
using trackItsValue.Common.Interfaces;

namespace trackItsValue.Service.Interfaces
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using DasMulli.Win32.ServiceUtils;
using freebyTech.Common.CommandLine;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Interfaces;
using freebyTech.Common.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace freebyTech.Common.Service.BaseClasses
{
    public abstract class ServiceFactoryBase : IServiceFactory
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public ILogger Logger { get; private set; }
        protected IConfiguration Configuration { get; private set; }

        public abstract IService GetServiceSingleton();

        public void ConfigureServices(IEnvironmentManager environmentManager, IExecutionEnvironment executionEnvironment, IServiceCollection services, string[] args)
        {
            var basicServicesProvider = services
                .AddLogging()
                .AddSingleton(environmentManager)
                .AddSingleton(executionEnvironment)
                .AddTransient<Win32ServiceManager>()
                .AddDefaultConfiguration(executionEnvironment, args, GetSwitchMappings())
                .BuildServiceProvider();

            Logger = basicServicesProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug, false).CreateLogger("ServiceStartup");
            Configuration = basicServicesProvider.GetService<IConfiguration>();

            OnConfigureServices(services, args);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected abstract void OnConfigureServices(IServiceCollection services, string[] args);

        public void ValidateAndExecuteService()
        {
            var iService = GetServiceSingleton();

            try
            {
                //File.Create("C:\\GotThere.FactoryBaseValidateAndExecute.txt");
                iService.Init(ServiceProvider);

                var status = iService.ValidateConfiguration();

                DisplayStartup(iService, status);

                switch (status)
                {
                    case BaseCommandArgumentSelected.DisplayHelp:
                        System.Environment.ExitCode = 1;
                        Thread.Sleep(500); // Allow NLog Console Logging.
                        break;

                    case BaseCommandArgumentSelected.ExecuteProgramAsConsole:
                        Console.Title = iService.ApplicationName;
                        iService.Start();

                        Console.WriteLine($"Running {iService.ServiceName} in Console Mode.");
                        Console.WriteLine("Hit any Key to Stop...");
                        Console.ReadKey();
                        Console.WriteLine("Key Registered, Stopping...");

                        iService.Stop();
                        Thread.Sleep(500); // Allow NLog Console Logging.
                        break;

                    case BaseCommandArgumentSelected.ExecuteProgramAsService:
                        //File.Create("C:\\GotThere.FactoryBaseValidateAndExecute.ExecuteProgramAsService.txt");

                        var serviceHost = new Win32ServiceHost(iService);
                        serviceHost.Run();
                        break;

                    case BaseCommandArgumentSelected.RegisterService:
                        var rebuiltArguments = System.Environment.GetCommandLineArgs()
                            .Where(arg =>
                                !arg.EndsWith(BaseCommandArguments.RegisterService, StringComparison.OrdinalIgnoreCase))
                            .Select(EscapeCommandLineArgument);
                        var host = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

                        if (!host.EndsWith("dotnet.exe", StringComparison.OrdinalIgnoreCase))
                        {
                            // For self-contained apps, skip the dll path
                            rebuiltArguments = rebuiltArguments.Skip(1);
                        }
                        
                        var fullServiceCommand = host + " " + string.Join(" ", rebuiltArguments);
                        
                        // Note that when the service is already registered and running, it will be reconfigured but not restarted
                        var serviceDefinition = new ServiceDefinitionBuilder(iService.ServiceName)
                            .WithDisplayName(iService.GetServiceDisplayName())
                            .WithDescription(iService.GetServiceDescription())
                            .WithBinaryPath(fullServiceCommand)
                            // TODO: Support more than LocalSystem in the future
                            .WithCredentials(Win32ServiceCredentials.LocalSystem)
                            // TODO: Support more than autostart in the future
                            .WithAutoStart(true)
                            .Build();

                        ServiceProvider.GetService<Win32ServiceManager>().CreateOrUpdateService(serviceDefinition, true);
                        
                        Console.WriteLine($@"Successfully registered and started service ""{iService.ServiceName}"" (""{iService.GetHelpDescription()}"")");
                        break;

                    case BaseCommandArgumentSelected.UninstallService:

                        ServiceProvider.GetService<Win32ServiceManager>().DeleteService(iService.ServiceName);

                        break;
                }
            }
            catch (Exception ex)
            {
                var errorTitle = $"{iService.ApplicationName} Startup Exception!";
                //File.WriteAllText(".\\ServiceFactoryBase.err", ex.Message + Environment.NewLine + ex.StackTrace);
                // Catastrophic failure of DI and configuration building, display exception and shut down.
                if (Logger != null)
                {
                    DisplayStartup(iService, BaseCommandArgumentSelected.DisplayHelp);
                    // TODO: Need a better logger than the base logger.
                    Logger.LogCritical(errorTitle, ex);
                    Logger.LogCritical(ex.Message);
                    Thread.Sleep(500); // Allow Console / Other Logging if its on a background thread.
                }
                else
                {
                    Console.WriteLine(errorTitle);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static string EscapeCommandLineArgument(string arg)
        {
            // http://stackoverflow.com/a/6040946/784387
            arg = Regex.Replace(arg, @"(\\*)" + "\"", @"$1$1\" + "\"");
            arg = "\"" + Regex.Replace(arg, @"(\\+)$", @"$1$1") + "\"";
            return arg;
        }

        private CommandCollection[] BasicCommandLineOptions { get; } = new[]
        {
            new CommandCollection()
            {
                CollectionName = "general-options",
                AvailableArguments = new []
                {
                    new CommandLineArgument() {
                        Command = "help", SwitchMapping = "-h", Description = "Display this help information.",
                    }
                }
            }
        };

        private Dictionary<string, string> _switchMappingsCache;
        public Dictionary<string, string> GetSwitchMappings()
        {
            if (_switchMappingsCache == null)
            {
                _switchMappingsCache = new Dictionary<string, string>();
                foreach (var commandCollection in GetServiceSingleton().GetCommandLineCollections())
                {
                    foreach (var arg in commandCollection.AvailableArguments)
                    {
                        if (!arg.SwitchMapping.IsNullOrEmpty()) _switchMappingsCache.Add($"-{arg.SwitchMapping}", arg.CommandValueName);
                    }
                }
                foreach (var commandCollection in BasicCommandLineOptions)
                {
                    foreach (var arg in commandCollection.AvailableArguments)
                    {
                        if (!arg.SwitchMapping.IsNullOrEmpty()) _switchMappingsCache.Add($"-{arg.SwitchMapping}", arg.CommandValueName);
                    }
                }
            }
            return _switchMappingsCache;
        }

        private void DisplayStartup(IService iService, BaseCommandArgumentSelected status)
        { 
            // Display help if asked to console.
            if (status == BaseCommandArgumentSelected.DisplayHelp)
            {
                var maxLineLength = Console.WindowWidth - 10;
                var executionOptions = iService.GetCommandLineCollections().CommandCollectionsAsExecutionOptions();
                foreach (var info in iService.GetHelpDescription().SplitAtLines(maxLineLength))
                {
                    Console.WriteLine(info);
                }
                Console.WriteLine();
                Console.WriteLine($"{iService.ApplicationName} ({iService.ApplicationVersion}) ");
                Console.WriteLine($"Usage: {iService.ApplicationName} [general-options]");
                Console.WriteLine($"Usage: {iService.ApplicationName} --{BaseCommandArguments.RegisterService}{executionOptions}");
                Console.WriteLine($"Usage: {iService.ApplicationName} --{BaseCommandArguments.UninstallService}{executionOptions}");
                Console.WriteLine($"Usage: {iService.ApplicationName} --{BaseCommandArguments.ConsoleMode}{executionOptions}");
                Console.WriteLine();
                foreach (var line in BasicCommandLineOptions.CommandCollectionsAsArgumentDescriptions(maxLineLength))
                {
                    Console.WriteLine(line);
                }
                foreach (var line in iService.GetCommandLineCollections().CommandCollectionsAsArgumentDescriptions(maxLineLength))
                {
                    Console.WriteLine(line);
                }
            }
            // Otherwise log execution to logger.
            //else
            //{
            //    //if (model.ParameterInfo.Count > 0)
            //    //{
            //    //    Logger.PushHeaderInfo($"{CommandLineModel.AppBinaryName} Execution Parameters");
            //    //    foreach (var info in model.ParameterInfo)
            //    //    {
            //    //        Logger.PushInfo(info);
            //    //    }
            //    //}
            //    if (Configuration["console"].IsTrue())
            //    {
            //        Logger.LogInformation($"{iService.ServiceName} Started on {DateTime.Now} in Console Mode.");
            //    }
            //    else
            //    {
            //        Logger.LogInformation($"{iService.ServiceName} Started on {DateTime.Now} in Service Mode.");
            //    }
            //}
        }
    }
}

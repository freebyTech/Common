﻿using System;
using System.IO;
using System.Reflection;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Interfaces;

namespace freebyTech.Common.Environment
{
  public class ExecutionEnvironment : IExecutionEnvironment
  {
    public const string Production = "Production";
    public const string Staging = "Staging";
    public const string Development = "Development";
    public const string Local = "Local";

    public ExecutionEnvironment(IEnvironmentManager environment, Assembly applicationAssembly) : this(environment)
    {
      ApplicationAssembly = applicationAssembly;
      ApplicationInfo = ApplicationAssembly?.GetName();
    }

    public ExecutionEnvironment(IEnvironmentManager environment)
    {
      var env = environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      if (env.IsNullOrEmpty())
        env = Production;

      EnvironmentName = env;

      ServiceRootPath = AppContext.BaseDirectory;
      StartupDirectory = Directory.GetCurrentDirectory();
    }

    public ExecutionEnvironment(string environmentName, string serviceRootPath, string startupDirectory)
    {
      EnvironmentName = environmentName;
      ServiceRootPath = serviceRootPath;
      StartupDirectory = startupDirectory;
    }

    public string EnvironmentName { get; }
    public string ServiceRootPath { get; }
    public string StartupDirectory { get; }
    public Assembly? ApplicationAssembly { get; }
    public AssemblyName? ApplicationInfo { get; private set; }
  }
}

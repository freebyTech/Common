﻿using System.Reflection;

namespace freebyTech.Common.Interfaces
{
    public interface IExecutionEnvironment
    {
        /// <summary>
        /// Gets or sets the name of the environment. This property is automatically set by the host to the value of the "ASPNETCORE_ENVIRONMENT" environment variable.
        /// </summary>
        string EnvironmentName { get; }

        /// <summary>
        /// The root path of the service. This is the location of the binaries
        /// </summary>
        string ServiceRootPath { get; }

        /// <summary>
        /// The startup directory of the application, sometimes different than ServiceRootPath
        /// </summary>
        string StartupDirectory { get; }

        /// <summary>
        /// The Application Assembly
        /// </summary>
        public Assembly? ApplicationAssembly { get; }

        /// <summary>
        /// The AssemblyName of the Application
        /// </summary>
        public AssemblyName? ApplicationInfo { get; }
    }
}

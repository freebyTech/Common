using freebyTech.Common.Environment;
using freebyTech.Common.Interfaces;

namespace freebyTech.Common.ExtensionMethods
{
  public static class EnvironmentExtensions
  {
    public static bool IsLocal(this IExecutionEnvironment executionEnvironment)
    {
      return (executionEnvironment.EnvironmentName.CompareNoCase(ExecutionEnvironment.Local));
    }
    public static bool IsDevelopment(this IExecutionEnvironment executionEnvironment)
    {
      return (executionEnvironment.EnvironmentName.CompareNoCase(ExecutionEnvironment.Development));
    }

    public static bool IsStaging(this IExecutionEnvironment executionEnvironment)
    {
      return (executionEnvironment.EnvironmentName.CompareNoCase(ExecutionEnvironment.Staging));
    }

    public static bool IsProduction(this IExecutionEnvironment executionEnvironment)
    {
      return (executionEnvironment.EnvironmentName.CompareNoCase(ExecutionEnvironment.Production));
    }

    public static bool IsEnvironemnt(this IExecutionEnvironment executionEnvironment, string environmentName)
    {
      return (executionEnvironment.EnvironmentName.CompareNoCase(environmentName));
    }

    public static string GetApiVersion(this IExecutionEnvironment executionEnvironment)
    {
      if (executionEnvironment.ApplicationInfo != null && executionEnvironment.ApplicationInfo.Version != null)
      {
        return $"v{executionEnvironment.ApplicationInfo.Version?.Major}.{executionEnvironment.ApplicationInfo.Version?.Minor}";
      }
      else
      {
        return "v0.1";
      }
    }

    public static string GetFullVersion(this IExecutionEnvironment executionEnvironment)
    {
      if (executionEnvironment.ApplicationInfo != null && executionEnvironment.ApplicationInfo.Version != null)
      {
        return executionEnvironment.ApplicationInfo.Version.ToString();
      }
      else
      {
        return "0.1";
      }
    }

    public static string GetAppName(this IExecutionEnvironment executionEnvironment)
    {
      if (executionEnvironment.ApplicationInfo != null && executionEnvironment.ApplicationInfo.Name != null)
      {
        return executionEnvironment.ApplicationInfo.Name;
      }
      else
      {
        return "Unknown";
      }
    }
  }
}

using freebyTech.Common.Environment;
using freebyTech.Common.Interfaces;

namespace freebyTech.Common.ExtensionMethods
{
    public static class EnvironmentExtensions
    {
        public static bool IsDevelopment(this IExecutionEnvironment executionEnvironment)
        {
            return(executionEnvironment.EnvironmentName.CompareNoCase(ExecutionEnvironment.Development));
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
    }
}

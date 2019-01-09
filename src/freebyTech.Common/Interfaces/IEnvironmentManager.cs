using System;
using System.Runtime.InteropServices;

namespace freebyTech.Common.Interfaces
{
    public interface IEnvironmentManager
    {
        string GetEnvironmentVariable(string variable);
        void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target);
        string GetMachineName();
        OSPlatform GetOSPlatform();
    }
}

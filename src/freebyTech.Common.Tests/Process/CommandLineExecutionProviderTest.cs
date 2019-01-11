using freebyTech.Common.Environment;
using freebyTech.Common.Process;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;

namespace freebyTech.Common.Tests.Process
{
    public class CommandLineExecutionProviderTest
    {
        OSPlatform OsPlatform { get; } = new EnvironmentManager().GetOSPlatform();

        public static string today = System.DateTime.Now.ToShortDateString();
        [Theory,
            MemberData(nameof(GeneralSystemCommandArguments))]
        public void ProcessGeneralCommandAsBatch(string[] commands, bool runAsBatch)
        {
            var commandLineExecution = new CommandLineExecutionProvider();
            var output = commandLineExecution.ProcessCommands(commands, runAsBatch);
            Assert.NotEmpty(output);
        }

        [Theory,
           MemberData(nameof(EnvironmentSystemCommandArguments))]
        public void ProcessEnvironmentCommandAsBatch(string[] commands, bool runAsBatch)
        {
            var commandLineExecution = new CommandLineExecutionProvider();
            var output = commandLineExecution.ProcessCommands(commands, runAsBatch);
            Assert.NotEmpty(output);
            Assert.Contains("JAVAHOME", output);
        }

        [Theory,
           MemberData(nameof(FileSystemCommandArguments))]
        public void ProcessFileSystemCommandAs(string[] commands, bool runAsBatch)
        {
            var commandLineExecution = new CommandLineExecutionProvider();
            var output = commandLineExecution.ProcessCommands(commands, runAsBatch);
            Assert.NotEmpty(output);
            Assert.Contains("eun555", output);
        }

        [Theory,
           InlineData(new string[] { "cmd /c dir /" }, false)]
        public void ProcessDirCommand(string[] commands, bool runAsBatch)
        {
            var commandLineExecution = new CommandLineExecutionProvider();
            var output = commandLineExecution.ProcessCommands(commands, runAsBatch);
            Assert.NotEmpty(output);
            
            if (OsPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                Assert.Contains("Program", output);
            }
            else
            {
                Assert.Contains("dev", output);
            }
        }

        [Theory,
        InlineData(new string[] { "cmd /c cd c:\\temp", "cmd /c mkdir c:\\temp\\blob", "cmd /c dir c:\\temp" }, false)]
        public void ProcessWindowsFileSystemCommands(string[] commands, bool runAsBatch)
        {
            if (OsPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                var timeoutMs = 30000;
                var commandLineExecution = new CommandLineExecutionProvider();
                var output = commandLineExecution.ProcessCommands(commands, runAsBatch, timeoutMs);
                Assert.NotEmpty(output);
                Assert.Contains("temp", output);
                Assert.Contains("blob", output);
            }
        }

        [Theory,
          InlineData(new string[] { "cd c:\\temp", "pause", "dir c:\\temp" }, true)]
        public void ProcessWindowsCommandAsBatchForTimeoutTest(string[] commands, bool runAsBatch)
        {
            if (OsPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                var commandLineExecution = new CommandLineExecutionProvider();
                var timeoutMs = 1000;
                Exception ex = Assert.Throws<TimeoutException>(() => commandLineExecution.ProcessCommands(commands, runAsBatch, timeoutMs));
                Assert.Equal($"Process did not finish in {timeoutMs} ms.", ex.Message);
            }
        }

        [Theory,
          InlineData(new string[] { "cd /", "read -n1 -r -p \"Press any key to continue...\" key", "ls" }, true)]
        public void ProcessLinuxCommandAsBatchForTimeoutTest(string[] commands, bool runAsBatch)
        {
            if (OsPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                var commandLineExecution = new CommandLineExecutionProvider();
                var timeoutMs = 1000;
                Exception ex = Assert.Throws<TimeoutException>(() => commandLineExecution.ProcessCommands(commands, runAsBatch, timeoutMs));
                Assert.Equal($"Process did not finish in {timeoutMs} ms.", ex.Message);
            }            
        }

        public static IEnumerable<object[]> GeneralSystemCommandArguments()
        {
            var osPlatform = new EnvironmentManager().GetOSPlatform();

            if (osPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                return new[]
                {
                    new object[] { new string[] { "dir c:\\"}, true },
                    new object[] { new string[] { "echo %HOME%", "set JAVAHOME=\"C:\\Temp\"", "echo %JAVAHOME%"}, true }
                };
            }
            else
            {
                return new[]
                {
                    new object[] { new string[] { "ls ./"}, true },
                    new object[] { new string[] { "echo $HOME", "export JAVAHOME=/temp", "echo $JAVAHOME"}, true }
                };
            }
        }

        public static IEnumerable<object[]> EnvironmentSystemCommandArguments()
        {
            var osPlatform = new EnvironmentManager().GetOSPlatform();

            if (osPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                return new[]
                {
                    new object[] { new string[] { "echo %HOME%", "set JAVAHOME=\"C:\\Temp\"", "echo %JAVAHOME%" }, true}
                };
            }
            else
            {
                return new[]
                {
                    new object[] { new string[] { "echo $HOME", "export JAVAHOME=/temp", "echo $JAVAHOME"}, true }
                };
            }
        }

        public static IEnumerable<object[]> FileSystemCommandArguments()
        {
            var osPlatform = new EnvironmentManager().GetOSPlatform();

            if (osPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                return new[]
                {
                    new object[] { new string[] { "mkdir c:\\temp_enu555", "echo eun555" }, true },
                    new object[] { new string[] { "mkdir c:\\temp5","cd c:\\temp5", "mkdir eun555", "dir" }, true }
                };
            }
            else
            {
                return new[]
                {
                    new object[] { new string[] { "md ./temp_eun555", "echo eun555" }, true },
                    new object[] { new string[] { "md ./temp1", "cd ./temp1", "mkdir ./eun555", "dir" }, true }
                };
            }
        }
    }
}

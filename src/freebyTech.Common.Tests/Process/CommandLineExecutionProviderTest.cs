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
            Assert.Contains("HOME", output);
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
           InlineData(new string[] { "cmd /c dir C:\\" }, false)]
        public void ProcessWindowsDirCommand(string[] commands, bool runAsBatch)
        {
            if (OsPlatform == System.Runtime.InteropServices.OSPlatform.Windows)
            {
                var commandLineExecution = new CommandLineExecutionProvider();
                var output = commandLineExecution.ProcessCommands(commands, runAsBatch);
                Assert.NotEmpty(output);
                Assert.Contains("Program", output);
            }
        }

        [Theory,
           InlineData(new string[] { "ls /" }, false)]
        public void ProcessLinuxLsCommand(string[] commands, bool runAsBatch)
        {
            if (OsPlatform == System.Runtime.InteropServices.OSPlatform.Linux)
            {
                var commandLineExecution = new CommandLineExecutionProvider();
                var output = commandLineExecution.ProcessCommands(commands, runAsBatch);
                Assert.NotEmpty(output);
                Assert.Contains("etc", output);
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
                    new object[] { new string[] { "ls ./"}, false },
                    new object[] { new string[] { "echo $HOME", "ls /"}, false }
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
                    new object[] { new string[] { "echo $HOME", "echo $USER" }, false }
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
                    new object[] { new string[] { "md c:\\temp_enu555", "dir c:\\" }, true },
                    new object[] { new string[] { "md c:\\temp5","cd c:\\temp5", "md eun555", "dir" }, true }
                };
            }
            else
            {
                return new[]
                {
                    //TODO: Need to investigate why running as a batch in linux is failing with
                    // System.ComponentModel.Win32Exception : Permission denied
                    // and why export and cd command themsevles are failing as well.
                    new object[] { new string[] { "mkdir /tmp/temp_eun555", "ls /tmp", "rm -rf /tmp/temp_eun555" }, false },
                    new object[] { new string[] { "mkdir /tmp/temp1", "mkdir /tmp/temp1/eun555", "ls /tmp/temp1", "rm -rf /tmp/temp1" }, false }
                };
            }
        }
    }
}

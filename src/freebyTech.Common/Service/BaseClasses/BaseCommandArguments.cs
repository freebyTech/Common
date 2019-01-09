﻿namespace freebyTech.Common.Service.BaseClasses
{
    public enum BaseCommandArgumentSelected
    {
        DisplayHelp,
        ExecuteProgramAsService,
        ExecuteProgramAsConsole,
        RegisterService,
        UninstallService
    }

    public class BaseCommandArguments
    {
        public const string Help = "help";
        public const string RegisterService = "register-service";
        public const string UninstallService = "uninstall-service";
        public const string ConsoleMode = "console";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace trackItsValue.Common.CommandLine
{
    public class CommandCollection
    {
        public string CollectionName { get; set; }
        public CommandLineArgument[] AvailableArguments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freebyUtil.Common.ExtensionMethods
{
  public static class CharExtensions
  {
    public static string MakeLine(this char character, int lineLength) => new string(character, lineLength);
  }
}

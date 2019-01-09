using System.Linq;
using System.Reflection;

namespace freebyTech.Common.ExtensionMethods
{
  public static class MethodInfoExtensions
  {
    public static string MethodSignature(this MethodInfo mi)
    {
      var param = mi.GetParameters()
                      .Select(p => $"{p.ParameterType.Name} {p.Name}")
                      .ToArray();

      var dtName = "?";
      if (mi.DeclaringType != null)
      {
        dtName = mi.DeclaringType.Name;
      }
      var signature = $"{mi.ReturnType.Name} {dtName}.{mi.Name}({string.Join(",", param)})";

      return signature;
    }

    public static string MethodSignature(this MethodBase mb)
    {
      var param = mb.GetParameters()
                      .Select(p => $"{p.ParameterType.Name} {p.Name}")
                      .ToArray();

      var dtName = "?";
      if (mb.DeclaringType != null)
      {
        dtName = mb.DeclaringType.Name;
      }
      var signature = $"{dtName}.{mb.Name}({string.Join(",", param)})";

      return signature;
    }

  }
}

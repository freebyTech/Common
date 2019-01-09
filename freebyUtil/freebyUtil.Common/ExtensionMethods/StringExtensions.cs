using freebyUtil.Common.Interfaces;
using System;
using System.IO;
using System.Text;

namespace freebyUtil.Common.ExtensionMethods
{
  public static class StringExtensions
  {
    /// <summary>
    /// More succinct usage of IsNullOrEmpty()
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string value)
    {
      return (string.IsNullOrEmpty(value));
    }

    /// <summary>
    /// Appends a name / value pair to a URL properly encoded.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public static void AppendUriEncoded(this StringBuilder sb, string name, string value)
    {
      if (sb.Length > 0)
      {
        sb.Append("&");
      }
      sb.Append(Uri.EscapeDataString(name));
      sb.Append("=");
      sb.Append(Uri.EscapeDataString(value));
    }

    /// <summary>
    /// Combines two paths together in the proper way.
    /// </summary>
    /// <param name="path1"></param>
    /// <param name="path2"></param>
    /// <returns></returns>
    public static string PathCombine(this string path1, string path2)
    {
      return Path.Combine(path1, path2);
    }

    public static bool CompareNoCase(this string stringA, string stringB)
    {
      if (string.Compare(stringA, stringB, StringComparison.OrdinalIgnoreCase) == 0)
      {
        return (true);
      }
      return (false);
    }

    public static bool ContainsNoCase(this string stringA, string stringB)
    {
      return (stringA.IndexOf(stringB, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public static bool CompareWithCase(this string stringA, string stringB)
    {
      if (string.Compare(stringA, stringB, StringComparison.Ordinal) == 0)
      {
        return (true);
      }
      return (false);
    }

    public static string SafeToUpper(this string dbValue)
    {
      if (dbValue.IsNullOrEmpty()) return "";

      return (dbValue.ToUpper());
    }


    public static string SafeTrim(this string dbValue)
    {
      if (dbValue.IsNullOrEmpty()) return "";

      return (dbValue.Trim());
    }

    public static string SafeTrimStart(this string dbValue)
    {
      if (dbValue.IsNullOrEmpty()) return "";

      return (dbValue.TrimStart());
    }

    public static string SafeTrimEnd(this string dbValue)
    {
      if (dbValue.IsNullOrEmpty()) return "";

      return (dbValue.TrimEnd());
    }

    public static string SafeTrimUpper(this string dbValue)
    {
      if (dbValue.IsNullOrEmpty()) return "";

      return (dbValue.Trim().ToUpper());
    }

    public static string VerifySize(this string dbValue, int maxLength = 0)
    {
      if (dbValue.IsNullOrEmpty()) return "";

      var ret = dbValue.Trim();
      if ((maxLength > 0) && (ret.Length >= maxLength))
      {
        ret = ret.Substring(0, maxLength);
      }

      return (ret);
    }


    public static void ThrowIfEmpty(this string value, string name)
    {
      if (value.SafeTrim().IsNullOrEmpty())
      {
        throw new ArgumentException(string.Format("{0} was not passed with a proper value.", name));
      }
    }


    public static T DeserializeXML<T>(this string value) where T : class
    {
      var encoding = new UTF8Encoding();

      using (var stream = new MemoryStream(encoding.GetBytes(value)))
      {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        return serializer.Deserialize(stream) as T;
      }

    }

    public static string Left(this string str, int length)
    {
      int strLen = str.Length;
      if (length > strLen)
      {
        return str;
      }

      if (str != string.Empty)
      {
        string result = str.Substring(0, length);
        return result;
      }

      return str;
    }

    public static string Right(this string str, int length)
    {
      int strLen = str.Length;
      if (length > strLen)
      {
        return str;
      }

      if (str != string.Empty)
      {
        string result = str.Substring(strLen - length, length);
        return result;
      }

      return str;
    }

    public static bool IsDouble(this string anyString)
    {
      double numericValue = 0;
      return IsDouble(anyString, out numericValue);
    }

    public static bool IsDouble(this string anyString, out double numericValue)
    {
      numericValue = 0;
      if (anyString.IsNullOrEmpty())
      {
        return false;
      }
      var cultureInfo = new System.Globalization.CultureInfo("en-US", true);
      return Double.TryParse(anyString, System.Globalization.NumberStyles.Any, cultureInfo.NumberFormat, out numericValue);
    }

    public static bool IsInt(this string anyString)
    {
      int numericValue = 0;
      return IsInt(anyString, out numericValue);
    }

    public static bool IsInt(this string anyString, out int numericValue)
    {
      numericValue = 0;
      if (anyString.IsNullOrEmpty())
      {
        return false;
      }
      var cultureInfo = new System.Globalization.CultureInfo("en-US", true);
      return int.TryParse(anyString, System.Globalization.NumberStyles.Any, cultureInfo.NumberFormat, out numericValue);
    }

    public static string ReplaceEnvValue(this string sourceString, string[] envVariables, IEnvironmentManager environmentManager)
    {
      if (sourceString.IsNullOrEmpty()) return string.Empty;
      foreach (var envVariable in envVariables)
      {
        var envValue = environmentManager.GetEnvironmentVariable(envVariable);
        sourceString = sourceString.Replace(string.Format("@{0}@", envVariable), envValue);
      }
      return sourceString;
    }

    public static string ReplaceBuildPropertyValue(this string sourceString, string buildProperty, string buildValue)
    {
      if (sourceString.IsNullOrEmpty()) return string.Empty;
      return sourceString.Replace(string.Format("@{0}@", buildProperty), buildValue);
    }

    public static string SanitizeInputs(this string str, int maxLength)
    {
      if (str == null)
      {
        str = string.Empty;
      }
      else
      {
        // Maximum Length gotcha
        if (str.Length > maxLength)
          str = str.Substring(0, maxLength - 3) + "...";
      }

      return (str);
    }
  }
}

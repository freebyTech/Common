using freebyTech.Common.Interfaces;
using freebyTech.Common.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace freebyTech.Common.ExtensionMethods
{
  public static class StringExtensions
  {
    /// <summary>
    /// A safe version of the Length property.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static int SafeLength(this string value)
    {
      if(value.IsNullOrEmpty()) return 0;
      return (value.Length);
    }

    /// <summary>
    /// More succinct usage of string.IsNullOrEmpty()
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static bool IsNullOrEmpty(this string value)
    {
      return (string.IsNullOrEmpty(value));
    }

    [DebuggerStepThrough]
    public static bool HasValue(this string value)
    {
      return !string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Ensures that a string only contains numeric values
    /// </summary>
    /// <returns>Input string with only numeric values, empty string if input is null or empty</returns>
    [DebuggerStepThrough]
    public static string EnsureNumericOnly(this string value)
    {
      if (string.IsNullOrEmpty(value)) return string.Empty;

      return new String(value.Where(c => Char.IsDigit(c)).ToArray());
    }

    [DebuggerStepThrough]
    public static bool IsAlpha(this string value)
    {
      return RegularExpressions.IsAlpha.IsMatch(value);
    }

    [DebuggerStepThrough]
    public static bool IsAlphaNumeric(this string value)
    {
      return RegularExpressions.IsAlphaNumeric.IsMatch(value);
    }

    /// <summary>
    /// Appends a name / value pair to a URL properly encoded.
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    [DebuggerStepThrough]
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
    [DebuggerStepThrough]
    public static string PathCombine(this string path1, string path2)
    {
      return Path.Combine(path1, path2);
    }

    /// <summary>
    /// Compares two strings without regard to case.
    /// </summary>
    /// <param name="stringA">The first string to compare.</param>
    /// <param name="stringB">The second string to compare.</param>
    /// <returns>True if they are equal without regard to case, otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool CompareNoCase(this string stringA, string stringB)
    {
      if (string.Compare(stringA, stringB, StringComparison.OrdinalIgnoreCase) == 0)
      {
        return (true);
      }
      return (false);
    }

    /// <summary>
    /// Determines whether or not <paramref name="stringA"/> is contained within <paramref name="stringB"/> in a caseless manner.    
    /// </summary>
    /// <param name="stringA">String to search for.</param>
    /// <param name="stringB">String to search inside of.</param>
    /// <returns>True if <paramref name="stringA"/> is inside of <paramref name="stringB"/> without regard to case.</returns>
    [DebuggerStepThrough]
    public static bool ContainsNoCase(this string stringA, string stringB)
    {
      return (stringA.IndexOf(stringB, StringComparison.OrdinalIgnoreCase) >= 0);
    }


    /// <summary>
    /// Compares two strings without regard to case.
    /// </summary>
    /// <param name="stringA">The first string to compare.</param>
    /// <param name="stringB">The second string to compare.</param>
    /// <returns>True if they are equal with regard to case, otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool CompareWithCase(this string stringA, string stringB)
    {
      if (string.Compare(stringA, stringB, StringComparison.Ordinal) == 0)
      {
        return (true);
      }
      return (false);
    }

    /// <summary>
    /// A safe ToUpper method, as of C# version 6 and beyond this can be done with a /"Safe Navigation/" operator
    /// like <code>value?.ToUpper</code> but this method also will turn a null into an empty string and has remained for
    /// backwards compatibility with this behavior.
    /// </summary>
    /// <returns></returns> 
    [DebuggerStepThrough]
    public static string SafeToUpper(this string value)
    {
      if (value.IsNullOrEmpty()) return "";

      return (value.ToUpper());
    }


    /// <summary>
    /// A safe Trim method, as of C# version 6 and beyond this can be done with a /"Safe Navigation/" operator
    /// like <code>value?.Trim</code> but this method also will turn a null into an empty string and has remained for
    /// backwards compatibility with this behavior.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static string SafeTrim(this string value)
    {
      if (value.IsNullOrEmpty()) return "";

      return (value.Trim());
    }

    /// <summary>
    /// A safe TrimStart method, as of C# version 6 and beyond this can be done with a /"Safe Navigation/" operator
    /// like <code>value?.TrimStart</code> but this method also will turn a null into an empty string and has remained for
    /// backwards compatibility with this behavior.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static string SafeTrimStart(this string value)
    {
      if (value.IsNullOrEmpty()) return "";

      return (value.TrimStart());
    }

    /// <summary>
    /// A safe TrimEnd method, as of C# version 6 and beyond this can be done with a /"Safe Navigation/" operator
    /// like <code>value?.TrimEnd</code> but this method also will turn a null into an empty string and has remained for
    /// backwards compatibility with this behavior.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static string SafeTrimEnd(this string value)
    {
      if (value.IsNullOrEmpty()) return "";

      return (value.TrimEnd());
    }

    /// <summary>
    /// A combined safe Trim and ToUpper method.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static string SafeTrimUpper(this string value)
    {
      if (value.IsNullOrEmpty()) return "";

      return (value.Trim().ToUpper());
    }

    private static bool IsWebUrlInternal(this string value, bool schemeIsOptional)
    {
      if (string.IsNullOrEmpty(value))
        return false;

      value = value.Trim().ToLowerInvariant();

      if (schemeIsOptional && value.StartsWith("//"))
      {
        value = "http:" + value;
      }

      return Uri.IsWellFormedUriString(value, UriKind.Absolute) &&
        (value.StartsWith("http://") || value.StartsWith("https://") || value.StartsWith("ftp://"));
    }

    [DebuggerStepThrough]
    public static bool IsWebUrl(this string value)
    {
        return value.IsWebUrlInternal(false);
    }

    [DebuggerStepThrough]
    public static bool IsWebUrl(this string value, bool schemeIsOptional)
    {
      return value.IsWebUrlInternal(schemeIsOptional);
    }

    [DebuggerStepThrough]
    public static bool IsEmail(this string value)
    {
      return !string.IsNullOrEmpty(value) && RegularExpressions.IsEmail.IsMatch(value.Trim());
    }

    /// <summary>
    /// A safe method for setting to a maximum size and returning the full or truncated portion or an emptry string.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static string VerifySize(this string value, int maxLength = 0)
    {
      if (value.IsNullOrEmpty()) return "";

      var ret = value.Trim();
      if ((maxLength > 0) && (ret.Length >= maxLength))
      {
        ret = ret.Substring(0, maxLength);
      }

      return (ret);
    }

    /// <summary>
    /// A safe method for determining if a value is empty and if it is throwing a <paramref name="System.ArgumentException"/> if it is.
    /// </summary>
    [DebuggerStepThrough]
    public static void ThrowIfEmpty(this string value, string name)
    {
      if (value.SafeTrim().IsNullOrEmpty())
      {
        throw new ArgumentException(string.Format("{0} was not passed with a proper value.", name));
      }
    }

    /// <summary>
    /// Deserializes a passed XML string into the type defined by T.
    /// </summary>
    /// <returns></returns>
    public static T DeserializeXML<T>(this string value) where T : class
    {
      var encoding = new UTF8Encoding();

      using (var stream = new MemoryStream(encoding.GetBytes(value)))
      {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        return serializer.Deserialize(stream) as T;
      }

    }

    /// <summary>
    /// A safe Left function that will return only a given length of characters from the left side.
    /// </summary>
    /// <returns></returns>
    public static string SafeLeft(this string str, int length)
    {
      if(str.IsNullOrEmpty()) return String.Empty;
      var strLen = str.Length;
      if (length > strLen)
      {
        return str;
      }

      if (str != string.Empty)
      {
        var result = str.Substring(0, length);
        return result;
      }

      return str;
    }

    /// <summary>
    /// A safe Right function that will return only a given length of characters from the right side.
    /// </summary>
    /// <returns></returns>
    public static string SafeRight(this string str, int length)
    {
      if(str.IsNullOrEmpty()) return String.Empty;
      var strLen = str.Length;
      if (length > strLen)
      {
        return str;
      }

      if (str != string.Empty)
      {
        var result = str.Substring(strLen - length, length);
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

    /// <summary>
    /// Sanitizes a given value to a maximum length if needed.
    /// </summary>
    /// <returns></returns>
    public static string SanitizeInputs(this string str, int maxLength)
    {
      if (str.IsNullOrEmpty())
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

    public static bool IsSameCommandLineArg(this string arg, string argExpected)
        {
            argExpected = argExpected.StripToArgument();
            arg = arg.StripToArgument();
            return arg.CompareNoCase(argExpected);
        }

        public static bool StartsWithCommandLineArg(this string arg, string argExpected)
        {
            argExpected = argExpected.StripToArgument().ToUpper();
            arg = arg.SafeToUpper();
            return arg.StartsWith($"/{argExpected}") || arg.StartsWith($"-{argExpected}") || arg.StartsWith($"--{argExpected}");

        }

        public static string StripToArgument(this string arg)
        {
            if (arg.IsNullOrEmpty()) return "";
            if (arg.StartsWith("--"))
            {
                return arg.Substring(2);
            }
            if (arg.StartsWith("/") || arg.StartsWith("-"))
            {
                return arg.Substring(1);
            }
            return arg;
        }

        public static bool IsTrue(this string arg)
        {
            if (arg.IsNullOrEmpty()) return false;
            if (arg.CompareNoCase("true") || arg == "1" || arg.CompareNoCase("t") || arg.CompareNoCase("y") || arg.CompareNoCase("yes")) return true;
            return false;
        }

        public static bool IsFalse(this string arg)
        {
            if (arg.IsNullOrEmpty()) return false;
            if (arg.CompareNoCase("false") || arg == "0" || arg.CompareNoCase("f") || arg.CompareNoCase("n") || arg.CompareNoCase("no")) return true;
            return false;
        }

        /// <summary>
        /// Converts a text value into a passed enum type if possible or the specified default value.
        /// </summary>
        [DebuggerStepThrough]
        public static T ToEnum<T>(this string value, T defaultValue)
        {
          if (!value.HasValue()) return defaultValue;
          
          try
          {
            return (T)Enum.Parse(typeof(T), value, true);
          }
          catch (ArgumentException)
          {
            return defaultValue;
          }
        }

        public static string WrapAtLines(this string text, int lineLength)
        {
            using (var reader = new StringReader(text))
                return reader.ReadToEnd(lineLength);
        }

        public static string[] SplitAtLines(this string text, int lineLength)
        {
            using (var reader = new StringReader(text))
                return reader.ReadLines(lineLength).ToArray();
        }

        public static string ReadToEnd(this TextReader reader, int lineLength)
        {
            return string.Join(System.Environment.NewLine, reader.ReadLines(lineLength));
        }

        public static IEnumerable<string> ReadLines(this TextReader reader, int lineLength)
        {
            var line = new StringBuilder();
            foreach (var word in reader.ReadWords())
                if (line.Length + word.Length <= lineLength)
                    line.Append($"{word} ");
                else
                {
                    yield return line.ToString().Trim();
                    line = new StringBuilder($"{word} ");
                }

            if (line.Length > 0)
                yield return line.ToString().Trim();
        }

        public static IEnumerable<string> ReadWords(this TextReader reader)
        {
            while (!reader.IsEof())
            {
                var word = new StringBuilder();
                while (!reader.IsBreak())
                {
                    word.Append(reader.Text());
                    reader.Read();
                }

                reader.Read();
                if (word.Length > 0)
                    yield return word.ToString();
            }
        }

        static bool IsBreak(this TextReader reader) => reader.IsEof() || reader.IsNullOrWhiteSpace();
        static bool IsNullOrWhiteSpace(this TextReader reader) => string.IsNullOrWhiteSpace(reader.Text());
        static string Text(this TextReader reader) => char.ConvertFromUtf32(reader.Peek());
        static bool IsEof(this TextReader reader) => reader.Peek() == -1;
  }
}

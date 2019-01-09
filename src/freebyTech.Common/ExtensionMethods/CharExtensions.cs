namespace freebyTech.Common.ExtensionMethods
{
  public static class CharExtensions
  {
    /// <summary>
    /// Will create a string of characters of the given length.
    /// </summary>
    /// <returns></returns>
    public static string MakeLine(this char character, int lineLength) => new string(character, lineLength);

    /// <summary>
    /// Will make a title line using the 
    /// </summary>
    /// <returns></returns>
    public static string MakeTitleLine(this char character, string title) => new string(character, title.SafeLength());
  }
}

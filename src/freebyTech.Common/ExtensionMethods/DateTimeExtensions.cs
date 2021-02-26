using System;

namespace freebyTech.Common.ExtensionMethods
{
  public static class DateTimeExtensions
  {
    public static string ToRevisionNumber(this DateTime dateTime)
    {
      return (dateTime.ToString("MMdd"));
    }

    public static DateTime EndOfDay(this DateTime date)
    {
      return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
    }

    public static DateTime StartOfDay(this DateTime date)
    {
      return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
    }

    public static DateTime EndOfPreviousMonth(int month, int year)
    {
      int prevYear;
      int prevMonth;
      if (month == 1)
      {
        prevYear = year - 1;
        prevMonth = 12;
      }
      else
      {
        prevYear = year;
        prevMonth = month - 1;
      }
      return new DateTime(prevYear, prevMonth, DateTime.DaysInMonth(prevYear, prevMonth));
    }

    public static DateTime EndOfMonth(int month, int year)
    {
      return new DateTime(year, month, DateTime.DaysInMonth(year, month));
    }
  }
}

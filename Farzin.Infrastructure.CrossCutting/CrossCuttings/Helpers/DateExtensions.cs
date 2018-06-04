using System;
using System.Collections.Generic;
using System.Globalization;

namespace Farzin.Infrastructure.CrossCutting.Helpers
{
    public static class DateExtensions
    {
        public static DateTime? GetDateFromShamsiString(string shDateStr)
        {
            if (string.IsNullOrWhiteSpace(shDateStr))
            {
                return null;
            }
            var shDateSplit = shDateStr.Split('/', '-', ' ', '.');
            if (shDateSplit.Length != 3 && shDateSplit.Length != 2)
            {
                return null;
            }

            int y, m, d;
            if (int.TryParse(shDateSplit[0], out d) == false)
            {
                return null;
            }
            if (int.TryParse(shDateSplit[1], out m) == false)
            {
                return null;
            }

            var pc = new PersianCalendar();

            if (shDateSplit.Length == 2)
            {
                y = pc.GetYear(DateTime.Today);
            }
            else if (int.TryParse(shDateSplit[2], out y) == false)
            {
                return null;
            }

            y += y < 50 ? 1400 : y < 100 ? 1300 : 0;

            try
            {
                var dt = pc.ToDateTime(y, m, d, 0, 0, 0, 0);
                return dt;
            }
            catch { return null; }
        }
        public static string GetDayAbbrivShamsi(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return "ش";
                case DayOfWeek.Sunday:
                    return "ی";
                case DayOfWeek.Monday:
                    return "د";
                case DayOfWeek.Tuesday:
                    return "س";
                case DayOfWeek.Wednesday:
                    return "چ";
                case DayOfWeek.Thursday:
                    return "پ";
                case DayOfWeek.Friday:
                    return "ج";
            }
            return "";
        }
        public static string ToShamsiDateString(this DateTime dt)
        {
            var pc = new PersianCalendar();
            var result = pc.GetYear(dt).ToString().PadLeft(2) + "/" 
                + pc.GetMonth(dt).ToString().PadLeft(2) + "/" 
                + pc.GetDayOfMonth(dt).ToString().PadLeft(2);
            return result;
        }
        public static string ToShamsiDateTimeString(this DateTime dt)
        {
            var pc = new PersianCalendar();
            var result = pc.GetYear(dt).ToString().PadLeft(2) + "/"
                + pc.GetMonth(dt).ToString().PadLeft(2) + "/"
                + pc.GetDayOfMonth(dt).ToString().PadLeft(2) + " "
                + pc.GetHour(dt).ToString().PadLeft(2) + ":"
                + pc.GetMinute(dt).ToString().PadLeft(2) + ":"
                + pc.GetSecond(dt).ToString().PadLeft(2);
            return result;
        }

        public static Dictionary<int,string> GetPersianMonthes()
        {
            var result = new Dictionary<int, string>();
            result.Add(1, "فروردین");
            result.Add(2, "اردیبهشت");
            result.Add(3, "خرداد");
            result.Add(4, "تیر");
            result.Add(5, "مرداد");
            result.Add(6, "شهریور");
            result.Add(7, "مهر");
            result.Add(8, "آبان");
            result.Add(9, "آذر");
            result.Add(10, "دی");
            result.Add(11, "بهمن");
            result.Add(12, "اسفند");

            return result;
        }
    }
}

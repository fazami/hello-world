using System;

namespace Farzin.Infrastructure.CrossCutting.Helpers
{
    public static class TimeSpanExtensions
    {
        public static string ToStringTotal(this TimeSpan t)
        {
            return ((int)t.TotalHours).ToString().PadLeft(2, '0') + ":" + t.Minutes.ToString().PadLeft(2, '0');
        }
    }
}

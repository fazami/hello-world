using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farzin.Infrastructure.CrossCutting.Helpers
{
    public static class StringExtensions
    {
        private static int ComputeLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 0 : target.Length;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 0 : source.Length;

            int sourceLength = source.Length;
            int targetLength = target.Length;

            int[,] distance = new int[sourceLength + 1, targetLength + 1];

            // Step 1
            for (int i = 0; i <= sourceLength; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetLength; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    // Step 2
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 3
                    distance[i, j] = Math.Min(
                                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceLength, targetLength];
        }
        public static string NormalizePersianChars(this string str)
        {
            return str
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                .Replace("۰", "0")
                .Replace("ي", "ی")
                .Replace("ك", "ک");
        }
        public static string ReplaceScript(this string s, string field, object value)
        {
            string v;

            if (value == null)
                v = "NULL";
            else if (value is int? ||
                     value is double? ||
                     value is float? ||
                     value is long? ||
                     value is decimal? ||
                     value is short?)
                v = value.ToString();
            else if (value is Enum)
                v = ((int)value).ToString();
            else if (value is DateTime?)
                v = "'" + ((DateTime)value).ToString("u").TrimEnd('Z') + "'";
            else if (value is string)
                v = "N'" + value.ToString().Replace("'", "''") + "'";
            else
                v = "'" + value.ToString().Replace("'", "''") + "'";

            return s.Replace(field, v);
        }
        /// <summary> 
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        public static double CalculateSimilarity(this string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 1 : 0;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 1 : 0;

            double stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - (stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }


    }
}

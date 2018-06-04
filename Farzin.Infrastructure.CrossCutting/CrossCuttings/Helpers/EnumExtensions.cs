using System;
using System.ComponentModel;
using System.Linq;

namespace Farzin.Infrastructure.CrossCutting.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            var customAttrib = type.GetField(@enum.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            if (customAttrib != null)
            {
                return ((DescriptionAttribute)customAttrib).Description;
            }
            else
            {
                return "";
            }
        }
    }
}

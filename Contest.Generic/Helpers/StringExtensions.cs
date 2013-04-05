using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contest.Generic.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
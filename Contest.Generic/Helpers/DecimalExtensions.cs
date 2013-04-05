using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contest.Generic.Helpers
{
    public static class DecimalExtensions
    {
        public static string ToMoneyString(this decimal amount)
        {
            return "$"+Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
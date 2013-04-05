using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contest.Generic.Helpers
{
    public class SessionManager
    {
        public static T GetFromSession<T>(string key)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
                return default(T);

            object obj = HttpContext.Current.Session[key];
            return obj == null ? default(T) : (T)obj;
        }

        public static void SetInSession<T>(string key, T value)
        {
            if (Equals(value, default(T)))
                HttpContext.Current.Session.Remove(key);
            else
                HttpContext.Current.Session[key] = value;
        }
    }
}
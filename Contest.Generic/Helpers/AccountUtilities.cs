using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Abstract.Entities;
using System.Text;

namespace Contest.Generic.Helpers
{
    public static class AccountUtilities
    {
        public static string ToObjectString(this IAccount account)
        {
            StringBuilder objectBuilder = new StringBuilder();
            objectBuilder.Append(account.AccountID);
            objectBuilder.Append("||");
            objectBuilder.Append(account.Token);
            objectBuilder.Append("||");
            objectBuilder.Append(account.Email);
            objectBuilder.Append("||");
            objectBuilder.Append(account.Password);
            return objectBuilder.ToString();
        }

    }
}
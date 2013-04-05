using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Concrete.Extensions
{
    public static class InterfaceTypeConverterExtension
    {
        public static T ConvertTo<T>(this T origin, Type convertType)
        {
            if (origin.GetType().GetInterface(typeof(T).FullName)!= null 
                && convertType.GetInterface(typeof(T).FullName)!= null)
            {
                var result = Activator.CreateInstance(convertType);

                var originProperties = origin.GetType().GetProperties();
                
                foreach (var property in result.GetType().GetProperties())
                {
                    var originProperty = originProperties.Where(p => p.Name == property.Name).SingleOrDefault();

                    if(originProperty != null)
                    {
                        property.SetValue(result, originProperty.GetValue(origin, null),null);
                    }
                }
                return (T)result;
            }
            return default(T);
        }


    }
}

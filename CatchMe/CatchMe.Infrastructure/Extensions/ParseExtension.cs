using System;
using System.ComponentModel;

namespace CatchMe.Infrastructure.Extensions
{
    public static class Parse
    {
        public static T To<T>(this object input) where T : IConvertible
        {
            var type = typeof(T).Name;
            TypeCode typecode;

            if (!Enum.TryParse(type, out typecode))
            {
                throw new ArgumentException(string.Format("Could not convert to type {0}", type));
            }                

            return (T)Convert.ChangeType(input, typecode);
        }        

        public static bool Is<T>(this string s)
        {
            try
            {                
                TypeDescriptor.GetConverter(typeof (T)).ConvertFromString(s);
                return true;
            }
            catch
            {
                return false;                 
            }
        }        
    }
}


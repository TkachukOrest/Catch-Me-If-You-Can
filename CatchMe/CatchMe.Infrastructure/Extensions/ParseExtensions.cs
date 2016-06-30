using System;
using System.ComponentModel;

namespace CatchMe.Infrastructure.Extensions
{
    public static class ParseExtensions
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

        public static T FromDb<T>(this object input)
        {
            var type = typeof(T);
            if (input == DBNull.Value)
            {
                if (!IsNullableType(type))
                {
                    throw new InvalidOperationException(string.Format("Cannot convert DBNull to {0}", type.Name));                 
                }

                return (T)(object)null;
            }            

            return (T)input;            
        }

        #region Helpers
        private static bool IsNullableType(Type type)
        {
            if (!type.IsValueType)
            {
                return true;
                
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;                
            } 

            return false; 
        }
        #endregion
    }
}


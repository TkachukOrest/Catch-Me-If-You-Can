using System;
using Newtonsoft.Json;

namespace CatchMe.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                var result = JsonConvert.SerializeObject(obj);
                return result;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
    }
}

using System;
using System.Configuration;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Extensions;

namespace CatchMe.Infrastructure.Concrete
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public T GetConfigurationValue<T>(string key) where T:IConvertible
        {
            return ConfigurationManager.AppSettings[key].To<T>();
        }
    }
}

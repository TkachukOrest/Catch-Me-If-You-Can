using System;

namespace CatchMe.Infrastructure.Abstract
{
    public interface IConfigurationService
    {
        string GetConfiguration(string key);

        T GetConfigurationValue<T>(string key) where T : IConvertible;
    }
}

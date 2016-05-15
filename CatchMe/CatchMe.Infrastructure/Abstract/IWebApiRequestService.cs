using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CatchMe.Infrastructure.Abstract
{
    public interface IWebApiRequestService: IDisposable
    {
        Task<T> GetAsync<T>(string webApiUrl, List<KeyValuePair<string, string>> parameters, AuthenticationHeaderValue authHeader = null);        

        Task<T> PostAsync<T>(string webApiUrl, object data, AuthenticationHeaderValue authHeader = null);        
    }
}

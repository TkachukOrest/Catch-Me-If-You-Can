using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using CatchMe.Infrastructure.Abstract;
using CatchMe.Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace CatchMe.Infrastructure.Concrete
{
    public class HttpClientApiRequestService : IWebApiRequestService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public HttpClientApiRequestService()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(1)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region IWebApiRequestService
        public async Task<T> GetAsync<T>(string webApiUrl, List<KeyValuePair<string, string>> parameters, AuthenticationHeaderValue authHeader = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(BuildUrl(webApiUrl, parameters)),
                Method = HttpMethod.Get,
            };
            
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;

            var response = await _httpClient.SendAsync(request);            
            var result = this.ParseResponse<T>(response, webApiUrl);

            return result;
        }

        public async Task<T> PostAsync<T>(string webApiUrl, object data, AuthenticationHeaderValue authHeader = null)
        {
            var url = new Uri(webApiUrl);
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;

            var response = await _httpClient.PostAsJsonAsync(url.AbsoluteUri, data);
            var result = this.ParseResponse<T>(response, webApiUrl);

            return result;
        }

        public void Dispose()
        {
            ((IDisposable) _httpClient)?.Dispose();
        }

        #endregion

        #region Helpers

        private T ParseResponse<T>(HttpResponseMessage response, string webApiUrl)
        {
            if (response != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }

            if (response != null)
            {
                throw new WebApiFailedExceptionException(webApiUrl, response.StatusCode + " - " + response.ReasonPhrase);
            }

            throw new WebApiFailedExceptionException(webApiUrl, "Service response is null");
        }

        private string BuildUrl(string methodName, List<KeyValuePair<string, string>> parameter)
        {
            var builder = new UriBuilder(methodName);
            
            var query = HttpUtility.ParseQueryString(builder.Query);
            if (parameter != null)
            {
                foreach (var specificParameter in parameter)
                {
                    query[specificParameter.Key] = specificParameter.Value;
                }
            }
            builder.Query = query.ToString();
            return builder.ToString();
        }

        #endregion        
    }
}

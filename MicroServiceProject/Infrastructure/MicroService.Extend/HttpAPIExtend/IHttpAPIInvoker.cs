using MicroService.Dto.PublicModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Extend
{
    public interface IHttpAPIInvoker
    {
        Task<string> GetAsync(string url);

        Task<string> PostAsync(string url, string data, string contentType);

        Task<string> PostFormUrlEncodedAsync(string url, Dictionary<string, string> formData);

        Task<string> PutAsync(string url, string data, string contentType);

        Task<string> DeleteAsync(string url);

        Task<HttpResult<T>> GetAsync<T>(string url);

        Task<HttpResult<T>> PostAsync<T>(string url, string data, string contentType);

        Task<HttpResult<T>> PostFormUrlEncodedAsync<T>(string url, Dictionary<string, string> formData);

        Task<HttpResult<T>> PutAsync<T>(string url, string data, string contentType);

        Task<HttpResult<T>> DeleteAsync<T>(string url);
    }
}

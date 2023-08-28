using EPRPlatform.API.Dto.PublicModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace EPRPlatform.API.Extend
{
    public class HttpAPIInvoker : IHttpAPIInvoker
    {
        private readonly HttpAPIInvokerOptions _httpInvokerOptions;
        public HttpAPIInvoker(IOptions<HttpAPIInvokerOptions> options)
        {
            _httpInvokerOptions = options.Value;
        }
        public async Task<string> GetAsync(string url)
        {
            using HttpClient httpClient = new();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, string data, string contentType)
        {
            using HttpClient httpClient = new();
            var content = new StringContent(data, Encoding.UTF8, contentType);
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostFormUrlEncodedAsync(string url, Dictionary<string, string> formData)
        {
            using HttpClient httpClient = new();
            var content = new FormUrlEncodedContent(formData);
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsync(string url, string data, string contentType)
        {
            using HttpClient httpClient = new();
            var content = new StringContent(data, Encoding.UTF8, contentType);
            var response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string url)
        {
            using HttpClient httpClient = new();
            var response = await httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResult<T>> GetAsync<T>(string url)
        {
            var result = new HttpResult<T>();
            try
            {
                using HttpClient httpClient = new();
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                result.IsSuccess = true;
                var responseData = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<T>(responseData);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<HttpResult<T>> PostAsync<T>(string url, string data, string contentType)
        {
            var result = new HttpResult<T>();
            try
            {
                using HttpClient httpClient = new();
                var content = new StringContent(data, Encoding.UTF8, contentType);
                var response = await httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                result.IsSuccess = true;
                var responseData = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<T>(responseData);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<HttpResult<T>> PostFormUrlEncodedAsync<T>(string url, Dictionary<string, string> formData)
        {
            var result = new HttpResult<T>();
            try
            {
                using HttpClient httpClient = new();
                var content = new FormUrlEncodedContent(formData);
                var response = await httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                result.IsSuccess = true;
                var responseData = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<T>(responseData);
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            
            return result;
        }

        public async Task<HttpResult<T>> PutAsync<T>(string url, string data, string contentType)
        {
            var result = new HttpResult<T>();
            try
            {
                using HttpClient httpClient = new();
                var content = new StringContent(data, Encoding.UTF8, contentType);
                var response = await httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                result.IsSuccess = true;
                var responseData = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<T>(responseData);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<HttpResult<T>> DeleteAsync<T>(string url)
        {
            var result = new HttpResult<T>();
            try
            {
                using HttpClient httpClient = new();
                var response = await httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                result.IsSuccess = true;
                var responseData = await response.Content.ReadAsStringAsync();
                result.Data = JsonConvert.DeserializeObject<T>(responseData);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}

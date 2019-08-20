using Newtonsoft.Json;
using SampleApp.Config;
using SampleApp.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface ITrustMarkDataWarehouseService
    {
        Task<CreateLodgementResponse> CreateLodgementAsync(CreateLodgementRequest lodgement);
        Task<UploadFileResponse> UploadFileAsync(UploadFileRequest uploadFileRequest);
    }

    public class TrustMarkDataWarehouseService : ITrustMarkDataWarehouseService
    {
        private readonly HttpClient _httpClient;
        private readonly TrustMarkApiConfig _config;
        
        public TrustMarkDataWarehouseService(HttpClient httpClient, TrustMarkApiConfig config)
        {
            _httpClient = httpClient;
            _config = config;

            _httpClient.DefaultRequestHeaders.Add("x-api-key", _config.ApiKey);
            _httpClient.DefaultRequestHeaders.Add("trustmarkId", _config.TrustMarkId);
            _httpClient.DefaultRequestHeaders.Add("tm-api-key", _config.TmApiKey);
        }

        public async Task<CreateLodgementResponse> CreateLodgementAsync(CreateLodgementRequest lodgement)
        {
            var url = _config.CreateLodgementUrl;
            return await SendAsync<CreateLodgementResponse>(url, lodgement);
        }

        public async Task<UploadFileResponse> UploadFileAsync(UploadFileRequest uploadFileRequest)
        {
            var url = _config.UploadFileUrl;
            return await SendAsync<UploadFileResponse>(url, uploadFileRequest);
        }

        private async Task<T> SendAsync<T>(string url, object data) where T : class
        {
            var response = await PostDataAsync(url, data);
            return JsonConvert.DeserializeObject<T>(response);
        }

        private async Task<string> PostDataAsync(string url, object data)
        {
            using (HttpResponseMessage res = await _httpClient.PostAsync(url, new JsonContent(data)))
            {
                using (HttpContent content = res.Content)
                {
                    return await content.ReadAsStringAsync();
                }
            }
        }

        public class JsonContent : StringContent
        {
            public JsonContent(object obj) :
                base(JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json")
            { }
        }
    }
}

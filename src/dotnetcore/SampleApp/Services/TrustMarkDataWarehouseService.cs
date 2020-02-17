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
        Task<AttachResponseResponse> AttachResponseAsync(AttachResponseRequest attachResponseRequest);
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
            return await SendAsync<CreateLodgementResponse>(url, lodgement, HttpMethod.Post);
        }

        public async Task<AttachResponseResponse> AttachResponseAsync(AttachResponseRequest attachResponseRequest)
        {
            var url = _config.AttachResponseUrl;
            return await SendAsync<AttachResponseResponse>(url, attachResponseRequest, HttpMethod.Put);
        }

        public async Task<AddMeasureResponse> AddMeasureAsync(AddMeasureRequest addMeasureRequest)
        {
            var url = _config.AddMeasureUrl;
            return await SendAsync<AddMeasureResponse>(url, addMeasureRequest, HttpMethod.Put);
        }

        public async Task<UploadFileResponse> UploadFileAsync(UploadFileRequest uploadFileRequest)
        {
            var url = _config.UploadFileUrl;
            return await SendAsync<UploadFileResponse>(url, uploadFileRequest, HttpMethod.Post);
        }

        private async Task<T> SendAsync<T>(string url, object data, HttpMethod httpMethod) where T : class
        {
            string response = string.Empty;
            if (httpMethod == HttpMethod.Post)
            {
                response = await PostDataAsync(url, data);
            }
            else if (httpMethod == HttpMethod.Put)
            {
                response = await PutDataAsync(url, data);
            }
            return JsonConvert.DeserializeObject<T>(response);
        }

        private async Task<string> PostDataAsync(string url, object data)
        {
            using (HttpResponseMessage res = await _httpClient.PostAsync(url, new JsonContent(data)))
            {
                using (HttpContent content = res.Content)
                {
                    var json = await content.ReadAsStringAsync();
                    return json;
                }
            }
        }

        private async Task<string> PutDataAsync(string url, object data)
        {
            using (HttpResponseMessage res = await _httpClient.PutAsync(url, new JsonContent(data)))
            {
                using (HttpContent content = res.Content)
                {
                    var json = await content.ReadAsStringAsync();
                    return json;
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

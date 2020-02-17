using Newtonsoft.Json;
using SampleApp.Models;
using System;
using System.Threading.Tasks;

namespace SampleApp
{
    public static class RequestHelpers
    {
        public static async Task<CreateLodgementRequest> GetCreateLodgementRequestFromFile()
        {
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "data", "sample_lodgement.json");
            var inputJson = await System.IO.File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<CreateLodgementRequest>(inputJson);
        }

        public static async Task<UploadFileRequest> GetCreateLodgementRequestUploadFileRequest()
        {
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "data", "SampleUploadDocument.pdf");
            var inputBytes = await System.IO.File.ReadAllBytesAsync(path);
            var result = new UploadFileRequest()
            {
                Data = inputBytes,
                Filename = $"MyUploadedFile_{System.DateTime.UtcNow.ToString("yyyyMMdd-HHmmss")}.pdf"
            };
            return await Task.FromResult(result);
        }

        public static async Task<AttachResponseRequest> GetAttachResponseRequest()
        {
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "data", "sample_attach_response.json");
            var inputJson = await System.IO.File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<AttachResponseRequest>(inputJson);
        }

        public static async Task<AddMeasureRequest> GetAddMeasureRequest()
        {
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "data", "sample_add_measure.json");
            var inputJson = await System.IO.File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<AddMeasureRequest>(inputJson);
        }
    }
}
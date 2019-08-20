using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SampleApp.Config;
using SampleApp.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static SampleApp.RequestHelpers;

namespace SampleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var config = configuration.GetSection("TrustMarkApiConfig").Get<TrustMarkApiConfig>();
            using (var httpClient = new HttpClient())
            {
                var trustMarkDataWarehouseService = new TrustMarkDataWarehouseService(httpClient, config);

                var fileUploadRequest = await GetCreateLodgementRequestUploadFileRequest();
                var fileUploadResponse = await trustMarkDataWarehouseService.UploadFileAsync(fileUploadRequest);

                Console.WriteLine("TrustMark file upload response...");
                Console.WriteLine(JsonConvert.SerializeObject(fileUploadResponse, Formatting.Indented));

                var createLodgementRequest = await GetCreateLodgementRequestFromFile();
                var createLodgementResponse = await trustMarkDataWarehouseService.CreateLodgementAsync(createLodgementRequest);

                Console.WriteLine("TrustMark create lodgement response...");
                Console.WriteLine(JsonConvert.SerializeObject(createLodgementResponse, Formatting.Indented));
            }
            Console.ReadKey();
        }
    }
}

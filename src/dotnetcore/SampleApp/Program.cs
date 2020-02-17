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
        static HttpClient httpClient = new HttpClient();
        static TrustMarkDataWarehouseService trustMarkDataWarehouseService;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var config = configuration.GetSection("TrustMarkApiConfig").Get<TrustMarkApiConfig>();

            trustMarkDataWarehouseService = new TrustMarkDataWarehouseService(httpClient, config);

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenu();
            }
        }

        private static async Task CreateLodgement()
        {
            Console.WriteLine("Sending request...");

            // Upload the sample file to a new lodgement
            var fileUploadRequest = await GetCreateLodgementRequestUploadFileRequest();
            var fileUploadResponse = await trustMarkDataWarehouseService.UploadFileAsync(fileUploadRequest);

            Console.WriteLine("TrustMark file upload response...");
            Console.WriteLine(JsonConvert.SerializeObject(fileUploadResponse, Formatting.Indented));

            var createLodgementRequest = await GetCreateLodgementRequestFromFile();
            var createLodgementResponse = await trustMarkDataWarehouseService.CreateLodgementAsync(createLodgementRequest);

            Console.WriteLine("TrustMark create lodgement response...");
            Console.WriteLine(JsonConvert.SerializeObject(createLodgementResponse, Formatting.Indented));

            Console.WriteLine("Please any key...");
            Console.ReadKey();
        }

        private static async Task AttachResponse()
        {
            Console.WriteLine("Sending request...");

            // Attach a response to an existing lodgement
            var attachResponseRequest = await GetAttachResponseRequest();
            var attachResponseResponse = await trustMarkDataWarehouseService.AttachResponseAsync(attachResponseRequest);

            Console.WriteLine("TrustMark attach PAS 2035 response...");
            Console.WriteLine(JsonConvert.SerializeObject(attachResponseResponse, Formatting.Indented));

            Console.WriteLine("Please any key...");
            Console.ReadKey();
        }

        private static async Task AddMeasure()
        {
            Console.WriteLine("Sending request...");

            // Add a measure to an existing lodgement
            var addMeasureRequest = await GetAddMeasureRequest();
            var addMeasureResponse = await trustMarkDataWarehouseService.AddMeasureAsync(addMeasureRequest);

            Console.WriteLine("TrustMark add measure to existing lodgement...");
            Console.WriteLine(JsonConvert.SerializeObject(addMeasureResponse, Formatting.Indented));

            Console.WriteLine("Please any key...");
            Console.ReadKey();
        }

        private static async Task<bool> MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Create a lodgement");
            Console.WriteLine("2) Attach PAS 2035 response to existing lodgement");
            Console.WriteLine("3) Add measure to existing lodgement");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await CreateLodgement();
                    return true;
                case "2":
                    await AttachResponse();
                    return true;
                case "3":
                    await AddMeasure();
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }
    }
}

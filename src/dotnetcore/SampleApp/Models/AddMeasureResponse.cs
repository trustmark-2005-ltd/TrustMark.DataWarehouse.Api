using Newtonsoft.Json;

namespace SampleApp.Models
{
    public class AddMeasureResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("measureId")]
        public string MeasureId { get; set; }
        [JsonProperty("umr")]
        public string UMR { get; set; }
        [JsonProperty("measure")]
        public Measure Measure { get; set; }
    }
}

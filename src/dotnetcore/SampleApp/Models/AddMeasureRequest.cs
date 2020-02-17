using Newtonsoft.Json;

namespace SampleApp.Models
{
    public class AddMeasureRequest
    {
        [JsonProperty("lodgementId")]
        public string LodgementId { get; set; }

        [JsonProperty("measure")]
        public Measure Measure { get; set; }
    }
}

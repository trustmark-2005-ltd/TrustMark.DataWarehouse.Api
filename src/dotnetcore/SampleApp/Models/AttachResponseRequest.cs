using Newtonsoft.Json;
using System;

namespace SampleApp.Models
{
    public class AttachResponseRequest
    {
        [JsonProperty("lodgementId")]
        public string LodgementId { get; set; }

        [JsonProperty("uploadToken")]
        public string UploadToken { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("documentType")]
        public string DocumentType { get; set; }

        [JsonProperty("responseDate")]
        public DateTime ResponseDate { get; set; }

        [JsonProperty("pas2035Role")]
        public string Pas2035Role { get; set; }

        [JsonProperty("pas2035RoleTMLN")]
        public string Pas2035RoleTMLN { get; set; }
    }
}

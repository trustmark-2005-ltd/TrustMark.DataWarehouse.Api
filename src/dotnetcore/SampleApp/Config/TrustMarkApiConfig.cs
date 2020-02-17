using static SampleApp.Config.TrustMarkApiConfigHelpers;

namespace SampleApp.Config
{
    public class TrustMarkApiConfig
    {
        public string Environment { get; set; }
        public string ApiKey { get; set; }
        public string TmApiKey { get; set; }
        public string TrustMarkId { get; set; }
        public string CreateLodgementUrl => GetUrl(Environment, "lodgement");
        public string UploadFileUrl => GetUrl(Environment, "uploadfile");
        public string AttachResponseUrl => GetUrl(Environment, "lodgement/attachresponse");
        public string AddMeasureUrl => GetUrl(Environment, "lodgement/addmeasure");
    }
}

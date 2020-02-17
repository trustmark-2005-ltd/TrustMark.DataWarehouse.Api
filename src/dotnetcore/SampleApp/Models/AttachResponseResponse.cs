namespace SampleApp.Models
{
    public class AttachResponseResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public LodgementDocument LodgementDocument { get; set; }
    }

    public class LodgementDocument
    {
        public string Id { get; set; }
        public string LodgementId { get; set; }
        public string DocumentType { get; set; }
        public string Role { get; set; }
        public string RoleTMLN { get; set; }
    }
}

namespace SampleApp.Models
{
    public class UploadFileResponse
    {
        public string Status { get; set; }
        public ValidationFailReason[] Reasons { get; set; }
        public UploadFileReceipt Receipt { get; set; }
    }

    public class UploadFileReceipt
    {
        public string UploadToken { get; set; }
    }
}

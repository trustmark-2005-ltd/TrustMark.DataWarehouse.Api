namespace SampleApp.Models
{
    public class CreateLodgementResponse
    {
        public string Status { get; set; }
        public ValidationFailReason[] Reasons { get; set; }
        public LodgementReceipt Receipt { get; set; }
    }

    public class LodgementReceipt
    {
        public string LodgementId { get; set; }
        public string CertificateNumber { get; set; }
        public MeasureReceipt[] Measures { get; set; }
    }

    public class MeasureReceipt
    {
        public string MeasureCategory { get; set; }
        public string MeasureType { get; set; }
        public string UMR { get; set; }
        public string InstallerReferenceNumber { get; set; }
    }

    public class ValidationFailReason
    {
        public string Field { get; set; }
        public string Reason { get; set; }
    }
}

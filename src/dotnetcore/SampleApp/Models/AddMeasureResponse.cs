namespace SampleApp.Models
{
    public class AddMeasureResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string MeasureId { get; set; }
        public Measure Measure { get; set; }
    }
}

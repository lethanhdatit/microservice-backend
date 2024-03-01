namespace stepmedia_demo.Models
{
    public class BaseResponse
    {
        public long? ID { get; set; }
        public string Status { get; set; } = null!;
        public string TS { get; set; } = null!;
    }
}

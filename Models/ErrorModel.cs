namespace MyBlog.Models
{
    public class ErrorModel
    {
        public string RequestId { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

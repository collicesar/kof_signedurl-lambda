namespace FemsaKofSignedURL.Models
{
    public class ErrorResponse
    {
        public string Error { get; set; }

        public ErrorResponse(string _error) => Error = _error;
    }
}

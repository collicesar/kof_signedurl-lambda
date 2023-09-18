using FemsaKofSignedURL.Models;

namespace FemsaKofSignedURL.Validators.Abstractions
{
    public interface IRequestValidator
    {
        bool IsValidRequest(RequestBody requestBody, out string message);
        bool IsValidFileExtension(string fileName, out string contentType);
    }
}

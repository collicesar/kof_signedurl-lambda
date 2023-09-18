using System.Text.RegularExpressions;
using FemsaKofSignedURL.Models;
using FemsaKofSignedURL.Resources;
using FemsaKofSignedURL.Validators.Abstractions;
using static FemsaKofSignedURL.Constants;

namespace FemsaKofSignedUR.Validators
{
    public class RequestValidator : IRequestValidator
    {        
        public bool IsValidRequest(RequestBody requestBody, out string message)
        {
            message = RequestValidation.InvalidFileName;

            if (requestBody == null || string.IsNullOrEmpty(requestBody.FileName))
                return false;
                              
            if (!Regex.IsMatch(requestBody.FileName, pattern))               
                return false;

            message = string.Empty;
            return true;
        }

        public bool IsValidFileExtension(string fileName, out string contentType)
        {
            contentType = fileName.ToGetFileExtension().ToGetContentType();

            if (contentType.Equals(RequestValidation.InvalidNameExtension))
                return false;
            return true;
        }
    }
}

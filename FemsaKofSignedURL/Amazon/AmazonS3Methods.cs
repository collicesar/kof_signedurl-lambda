using Amazon.S3;
using Amazon.S3.Model;
using FemsaKofSignedURL.Amazon.Abstractions;

namespace FemsaKofSignedURL.Amazon
{
    public class AmazonS3Methods : IAmazonS3Methods
    {
        private readonly string _bucketName;
        private readonly int _expiredMinutesTime;
        private readonly IAmazonS3 _s3Client;

        public AmazonS3Methods()
        {
            _s3Client = new AmazonS3Client();

            if (int.TryParse(Environment.GetEnvironmentVariable("ExpiredMinutesTime"), out int parsedExpiredMinutesTime))
                _expiredMinutesTime = parsedExpiredMinutesTime;
            else
                _expiredMinutesTime = 15;

            _bucketName = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("BucketName")) ? string.Empty : Environment.GetEnvironmentVariable("BucketName");
        }

        public async Task<string> GenerateSignedUrl(string fileName, string contentType)
        {
            return await Task.Run(() => GetSignedUrl(fileName, contentType));
        }

        private string GetSignedUrl(string key, string mimeType)
        {
            try
            {
                if (!string.IsNullOrEmpty(_bucketName))
                {
                    // Crear la solicitud para generar la URL prefirmada
                    var presignedUrlRequest = new GetPreSignedUrlRequest
                    {
                        BucketName = _bucketName,
                        Key = key,
                        Expires = DateTime.UtcNow.AddMinutes(_expiredMinutesTime),
                        Verb = HttpVerb.PUT,
                        ContentType = mimeType
                    };

                    // Generar la URL prefirmada
                    return _s3Client.GetPreSignedURL(presignedUrlRequest);
                }
            }
            catch(Exception) 
            {
                throw;
            }

            return string.Empty;
        }
    }
}

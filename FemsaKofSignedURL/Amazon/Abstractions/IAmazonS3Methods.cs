namespace FemsaKofSignedURL.Amazon.Abstractions
{
    public interface IAmazonS3Methods
    {
        Task<string> GenerateSignedUrl(string fileName, string contentType);
    }
}

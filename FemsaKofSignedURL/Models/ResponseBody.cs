using Newtonsoft.Json;

namespace FemsaKofSignedURL.Models
{
    public class ResponseBody
    {
        [JsonProperty("signed_request")]
        public string SignedRequest { get; set; }
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        public ResponseBody(string signedRequest, string fileName, string contentType)
        {
            SignedRequest = signedRequest;
            FileName = fileName;
            ContentType = contentType;
        }
    }  
}

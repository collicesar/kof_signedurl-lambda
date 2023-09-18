using Newtonsoft.Json;

namespace FemsaKofSignedURL.Models
{
    public class RequestBody
    {
        [JsonProperty("fileName")]
        public string? FileName { get; set; }
    }
}

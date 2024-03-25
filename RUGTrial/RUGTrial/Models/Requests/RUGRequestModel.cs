using System.Text.Json.Serialization;

namespace RUGTrial.Models.Requests
{
    public class RUGRequestModel
    {
        [JsonPropertyName("results")]
        public List<User>? Users { get; set; }
    }
}

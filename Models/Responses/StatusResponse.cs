using System.Text.Json.Serialization;

namespace TES_AhmadHadiJaelani.Models.Responses
{
    public class StatusResponse
    {
        [JsonPropertyName("Status")]
        public required string Status { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; } = string.Empty;
    }
}

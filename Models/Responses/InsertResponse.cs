using System.Text.Json.Serialization;

namespace TES_AhmadHadiJaelani.Models.Responses
{
    public class InsertResponse : StatusResponse
    {
        [JsonPropertyName("SalesOrderNo")]
        public string? SalesOrderNo { get; set; } = null;
    }
}

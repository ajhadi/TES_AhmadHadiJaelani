using System.Text.Json.Serialization;

namespace TES_AhmadHadiJaelani.Models.Requests
{
    public class DeleteRequest
    {
        [JsonPropertyName("SalesOrderNo")]
        public required string SalesOrderNo { get; set; }
    }
}

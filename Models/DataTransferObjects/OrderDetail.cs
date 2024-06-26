using System.Text.Json.Serialization;

namespace TES_AhmadHadiJaelani.Models.DataTransferObjects
{
    public class OrderDetail
    {
        [JsonPropertyName("ProductCode")]
        public required string ProductCode { get; set; }
        [JsonPropertyName("Qty")]
        public int Qty { get; set; }
    }
}

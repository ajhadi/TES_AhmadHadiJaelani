using System.Text.Json.Serialization;
using TES_AhmadHadiJaelani.Models.DataTransferObjects;

namespace TES_AhmadHadiJaelani.Models.Requests
{
    public class InsertRequest
    {
        [JsonPropertyName("CustId")]
        public required string CustId { get; set; }
        [JsonPropertyName("OrderDetail")]
        public required List<OrderDetail> OrderDetail { get; set; }
    }
}

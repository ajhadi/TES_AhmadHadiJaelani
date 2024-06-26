using System.Text.Json.Serialization;
using TES_AhmadHadiJaelani.Models.DataTransferObjects;

namespace TES_AhmadHadiJaelani.Models.Responses
{
    public class ViewResponse
    {
        [JsonPropertyName("CustId")]
        public required string CustId { get; set; }
        [JsonPropertyName("SalesOrderNo")]
        public required string SalesOrderNo { get; set; }
        [JsonPropertyName("OrderDetail")]
        public required List<OrderDetail> OrderDetail { get; set; }

    }
}

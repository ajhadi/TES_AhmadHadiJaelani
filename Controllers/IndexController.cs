using Microsoft.AspNetCore.Mvc;
using TES_AhmadHadiJaelani.Models.Requests;
using TES_AhmadHadiJaelani.Models.Responses;
using TES_AhmadHadiJaelani.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TES_AhmadHadiJaelani.Data;
using Azure;

namespace TES_AhmadHadiJaelani.Controllers
{
    [ApiController]
    [Route("")]
    public class IndexController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IndexController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("view")]
        public async Task<IActionResult> ViewSalesOrder(string salesOrderNo)
        {
            var salesOrder = await _context.SalesOrder
                .Where(s => s.SalesOrderNo == salesOrderNo)
                .SingleOrDefaultAsync();

            if (salesOrder == null)
                return NotFound(new StatusResponse
                {
                    Status = "failed",
                    Message = $"No Order {salesOrderNo} tidak ditemukan"
                });

            var orderDetails = await _context.SalesOrderDetail
                .Where(s => s.SalesOrderNo == salesOrderNo)
                .Select(o => new OrderDetail
                {
                    ProductCode = o.ProductCode,
                    Qty = o.Qty
                })
                .ToListAsync();

            var response = new ViewResponse
            {
                SalesOrderNo = salesOrder.SalesOrderNo,
                CustId = salesOrder.CustCode,
                OrderDetail = orderDetails
            };

            return Ok(response);
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertSalesOrder([FromBody] InsertRequest request)
        {
            DataTable orderDetailTable = new DataTable();
            orderDetailTable.Columns.Add("ProductCode", typeof(string));
            orderDetailTable.Columns.Add("Qty", typeof(int));

            foreach (var item in request.OrderDetail)
            {
                orderDetailTable.Rows.Add(item.ProductCode, item.Qty);
            }

            var result = await _context.InsertSalesOrder(request.CustId, orderDetailTable);

            var response = result.FirstOrDefault();
            if (response == null || response.Status != "success")
            {
                var errorResult = new StatusResponse
                {
                    Status = response.Status ?? "failed",
                    Message = response.Message,
                };
                return BadRequest(errorResult);
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSalesOrder([FromBody] DeleteRequest request)
        {
            var result = await _context.DeleteSalesOrder(request.SalesOrderNo);

            if (!(result.Status == "success"))
            {
                return BadRequest(new { Status = result.Status, Message = result.Message });
            }

            return Ok(new { Status = result.Status, Message = result.Message });
        }
    }
}

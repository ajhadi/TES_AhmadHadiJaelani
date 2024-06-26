using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TES_AhmadHadiJaelani.Models.DataTransferObjects;
using TES_AhmadHadiJaelani.Models.Entities;
using TES_AhmadHadiJaelani.Models.Responses;

namespace TES_AhmadHadiJaelani.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SalesOrder> SalesOrder { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }

        private DbSet<InsertResponse> InsertResponses { get; set; }
        private DbSet<DeleteResponse> DeleteResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InsertResponse>().HasNoKey();
            modelBuilder.Entity<DeleteResponse>().HasNoKey();
            modelBuilder.Entity<SalesOrder>().HasNoKey();
            modelBuilder.Entity<SalesOrderDetail>().HasNoKey();
        }

        public async Task<IEnumerable<InsertResponse>> InsertSalesOrder(string custId, DataTable orderDetails)
        {
            var custIdParam = new SqlParameter("@CustId", custId);
            var orderDetailParam = new SqlParameter("@OrderDetail", orderDetails)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "OrderDetailType"
            };

            return await this.InsertResponses
                .FromSqlRaw("EXEC sp_InsertSalesOrder @CustId, @OrderDetail", custIdParam, orderDetailParam)
                .ToListAsync();
        }

        public async Task<DeleteResponse> DeleteSalesOrder(string salesOrderNo)
        {
            var salesOrderNoParam = new SqlParameter("@SalesOrderNo", salesOrderNo);

            var result = await this.DeleteResponses
                .FromSqlRaw("EXEC sp_DeleteSalesOrder @SalesOrderNo", salesOrderNoParam)
                .ToListAsync();

            return result.FirstOrDefault();
        }
    }
}

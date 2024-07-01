using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entities;
using System.Data;

namespace MultiShop.Discount.Context
{
    public class DapperContext :DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SALIHSENTURK\\SQLEXPRESS;initial Catalog=MultiShopDiscountDb;Integrated Security=True;");
        }

        public DbSet<Coupon> Coupons { get; set; }
        public IDbConnection CreteConnection()=>new SqlConnection(_connectionString);
    }
}

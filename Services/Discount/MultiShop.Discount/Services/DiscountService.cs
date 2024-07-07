using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto)
        {
            string query = "insert into Coupons (Code,Rate,isActive,ValidDate) values (@code,@rate,@isActive,@validDate)";
            var parameters = new DynamicParameters();
            parameters.Add("@code", createCouponDto.Code);
            parameters.Add("@rate", createCouponDto.Rate);
            parameters.Add("@isActive", createCouponDto.isActive);
            parameters.Add("@validDate", createCouponDto.ValidDate);
            using (var connection = _context.CreteConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteDiscountCouponAsync(int id)
        {
            string query = "Delete From Coupons where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("couponId", id);
            using (var connection = _context.CreteConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
        {
            string query = "select * from coupons";
            using (var connection = _context.CreteConnection())
            {
                var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
        {
            string query = "select * from Coupons where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("couponId", id);
            using (var connection = _context.CreteConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query,parameters);
                return values;
            }
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCuponDto updateCuponDto)
        {
            string query = "Update Coupons set Code=@code , Rate=@rate, isActive=@isActive,ValidDate=@validDate where CouponId=@couponId";
            var parameters = new DynamicParameters();
            parameters.Add("@code", updateCuponDto.Code);
            parameters.Add("@rate", updateCuponDto.Rate);
            parameters.Add("@isActive", updateCuponDto.isActive);
            parameters.Add("@validDate", updateCuponDto.ValidDate);
            parameters.Add("@couponId", updateCuponDto.CouponId);

            using (var connection = _context.CreteConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}

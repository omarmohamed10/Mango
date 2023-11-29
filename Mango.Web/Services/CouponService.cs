using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService) 
        {
            _baseService = baseService;
        }
        public Task<ResponseDTO?> CreateCouponAsync(CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> DeleteCouponAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> GetAllCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> GetCouponAsync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> GetCouponByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO?> UpdateCouponAsync(CouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }
    }
}

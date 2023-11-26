using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDTO();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = await _db.Coupons.ToListAsync();
                if (coupons != null)
                {
                    _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(coupons);
                    _response.IsSuccess = true;
                    _response.Message = "Sucessfully";
                }
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return NotFound(_response);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
                _response.Result = _mapper.Map<CouponDTO>(coupon);
                _response.IsSuccess=true;
                _response.Message = "Sucessfully";
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return NotFound(_response);
        }

    }
}

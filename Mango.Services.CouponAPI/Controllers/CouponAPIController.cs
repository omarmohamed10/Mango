using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

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
                Coupon coupon = await _db.Coupons.FirstAsync(c => c.CouponId == id);
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
        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FirstAsync(c => c.CouponCode.ToLower() == code.ToLower());
                if(coupon == null)
                {
                    _response.IsSuccess=false;
                    _response.Message = "Faild";
                    return NotFound(_response);
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDTO>(coupon);
                    _response.IsSuccess = true;
                    _response.Message = "Sucessfully";
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return NotFound(_response);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CouponDTO couponDTO)
        {
            try
            {
               if(couponDTO == null)
                {
                    _response.IsSuccess=false;
                    _response.Message = $"Error when try to add Coupon with id @{couponDTO.CouponId}";
                    return BadRequest(_response); 
                }

               _db.Coupons.Add(_mapper.Map<Coupon>(couponDTO));
               await _db.SaveChangesAsync();

               _response.Result = _mapper.Map<CouponDTO>(couponDTO);
               _response.IsSuccess = true;
               _response.Message = "Sucessfully Added";
               return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return BadRequest(_response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CouponDTO couponDTO)
        {
            try
            {
                if (couponDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Error when try to Update Coupon with id @{couponDTO.CouponId}";
                    return BadRequest(_response);
                }

                _db.Coupons.Update(_mapper.Map<Coupon>(couponDTO));
                await _db.SaveChangesAsync();

                _response.Result = _mapper.Map<CouponDTO>(couponDTO);
                _response.IsSuccess = true;
                _response.Message = "Sucessfully Updated";
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return BadRequest(_response);
        }
        [HttpDelete("CouponId")]
        public async Task<IActionResult> Delete(int CouponId)
        {
            try
            {
                if (CouponId == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Error when try to Remove Coupon with id @{CouponId}";
                    return BadRequest(_response);
                }
                Coupon coupon = await _db.Coupons.FirstAsync(c => c.CouponId == CouponId);

                _db.Coupons.Remove(coupon);
                await _db.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.Message = "Sucessfully Removed";
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return BadRequest(_response);
        }
    }
}

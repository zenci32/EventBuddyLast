using Business.Repositories.CategoryRepository;
using Business.Repositories.RateRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateService _rateService;
        public RatesController(IRateService rateService)
        {
            _rateService = rateService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var data = await _rateService.GetList();
            if (data.Success)
            {
                return Ok(data);
            }
            return BadRequest(data);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddByPhoneNumber(string phoneNumber,decimal rate)
        {
            var result = await _rateService.AddByPhoneNumber(phoneNumber,rate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
      
    }
}

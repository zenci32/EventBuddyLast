using Business.Repositories.EmailRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailParametersController : ControllerBase
    {
        private readonly IEmailService _emailParameterService;

        public EmailParametersController(IEmailService emailParameterService)
        {
            _emailParameterService = emailParameterService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(Email emailParameter)
        {
            var result = await _emailParameterService.Add(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(Email emailParameter)
        {
            var result = await _emailParameterService.Update(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(Email emailParameter)
        {
            var result = await _emailParameterService.Delete(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _emailParameterService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _emailParameterService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}

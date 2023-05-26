using Business.Repositories.CategoryRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var data=await _categoryService.GetList();
            if (data.Success)
            {
                return Ok(data);
            }
            return BadRequest(data);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Add(Category category)
        {
            var result=await _categoryService.Add(category);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Update(Category category)
        {
            var result = await _categoryService.Update(category);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(Category category)
        {
            var result = await _categoryService.Delete(category);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

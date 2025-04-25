using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.BusinessLogic.Dtos;
using Products.BusinessLogic.ServicesContracts;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetCategory(string code)
        {
            var category = await _categoryService.GetCategoryByCode(code);
            return Ok(category);
        }
        [HttpGet("products/{code}")]
        public async Task<IActionResult> GetCategoryAndProducts(string code)
        {
            var category = await _categoryService.GetCategoryAndProductsByCode(code);
            return Ok(category);
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            var category = await _categoryService.SearchCategory(name);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto? input)
        {
            if (input == null)
                return BadRequest("Category data is required.");
            var category = await _categoryService.CreateCategory(input);
            return Ok(category);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(AddCategoryDto? category)
        {
            if (category == null)
                return BadRequest("Category data is required.");
            var result = await _categoryService.UpdateCategory(category);
            return Ok(result);
        }
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteCategory(string code)
        {
            var result = await _categoryService.DeleteCategory(code);
            return result ? NoContent() : NotFound($"Category With Name : {code} Not Found.");
        }


    }
}

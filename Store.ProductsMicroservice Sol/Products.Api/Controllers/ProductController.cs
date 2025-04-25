using Microsoft.AspNetCore.Mvc;
using Products.BusinessLogic.Dtos;
using Products.BusinessLogic.ServicesContracts;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("search/{searchPhrase}")]
        public async Task<IActionResult> Search(string? searchPhrase)
        {
            var products = await _productService.SearchProduct(searchPhrase);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto? input)
        {
            if (input == null)
                return BadRequest("Product data is required.");

            var product = await _productService.CreateProduct(input);

            return CreatedAtAction(nameof(GetProduct), new { id = product!.Id }, product);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto? product)
        {
            var result = await _productService.UpdateProduct(product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);

            return result ? NoContent() : NotFound($"Product With ID : {id} Not Found.");
        }
    }
}

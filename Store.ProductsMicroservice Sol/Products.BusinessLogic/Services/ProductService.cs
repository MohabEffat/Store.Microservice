using AutoMapper;
using Products.BusinessLogic.Dtos;
using Products.BusinessLogic.RabbitMQ;
using Products.BusinessLogic.ServicesContracts;
using Products.DataAccess.Entities;
using Products.DataAccess.RepositoriesContracts;

namespace Products.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        //private readonly IRabbitMQPublisher _rabbitMQPublisher;

        public ProductService(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            //_rabbitMQPublisher = rabbitMQPublisher;
        }
        public async Task<ProductDto?> CreateProduct(AddProductDto? input)
        {
            var product = _mapper.Map<Product>(input);
            product.Id = Guid.NewGuid();

            var category = await _categoryRepository.GetCategoryByCodeAsync(input!.CategoryCode);

            if (category == null)
                throw new ArgumentException($"Category '{input.CategoryCode}' not found.");

            product.CategoryId = category.Id;

            var success = await _productRepository.CreateProductAsync(product);

            if (!success)
                throw new InvalidOperationException("Product creation failed. Please try again.");

            var existingProduct = _mapper.Map<ProductDto>(product);
            existingProduct.Category = category.Name;

            return existingProduct;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                throw new ArgumentException($"Product with ID '{id}' not found.");

            var deleted = await _productRepository.DeleteAsync(id);
            if (!deleted) return false;

            var routingKey = "product.delete";
            var message = new UpdateProductNameMessage
            {
                ProductId = id,
                NewName = product.Name
            };

            //_rabbitMQPublisher.Publish(message, routingKey);
            return true;
        }
            


        public async Task<IReadOnlyList<ProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductById(Guid? id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null) throw new ArgumentException($"Product with ID '{id}' not found.");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IReadOnlyList<ProductDto>> SearchProduct(string? searchPhrase)
        {
            var products = await _productRepository.SearchProductAsync(searchPhrase);
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }

        public async Task<ProductDto?> UpdateProduct(ProductDto? input)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(input!.Id);

            if(existingProduct == null)
                throw new ArgumentException($"Product with ID '{input!.Id}' not found.");

            var product = _mapper.Map<Product>(input);

            var result = await _productRepository.UpdateProductAsync(product);

            bool isNameChanged = result!.Name != input.Name;

            if (isNameChanged)
            {
                var routingKey = "product.update.name";
                var message = new UpdateProductNameMessage
                {
                    ProductId = result.Id,
                    NewName = input.Name
                };

                //_rabbitMQPublisher.Publish(message, routingKey);
            }

            return _mapper.Map<ProductDto>(result);

        }

    }
}

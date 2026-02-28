using hanin.Entities;
using hanin.RepositoriesInterface;
using hanin.ServiceIntefrace;

namespace hanin.Service
{
    public class ProductService : ProductServiceInt
    {
        private readonly ProductRepoInt _repository;

        public ProductService(ProductRepoInt repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<IEnumerable<ProductEntity>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<IEnumerable<ProductEntity>>();
            try
            {
                var products = await _repository.GetAllAsync();
                response.Data = products;
                response.Message = "Products retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error fetching products: " + ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<ProductEntity>> CreateProductAsync(ProductEntity product)
        {
            var response = new ServiceResponse<ProductEntity>();
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                response.Success = false;
                response.Message = "Category name cannot be empty.";
                return response;
            }
            var nameExists = await _repository.GetByNameAsync(product.Name);

            if (nameExists != null)
            {
                response.Success = false;
                response.Message = $"Error: The name '{product.Name}' is already taken.";
                return response;
            }
            if (product.Price <= 0)
            {
                response.Success = false;
                response.Message = "Price cannot be zero or negative.";
                return response;
            }

            await _repository.AddAsync(product);
            response.Data = product;
            response.Message = "Product created successfully.";
            return response;
        }

        public async Task<ServiceResponse<ProductEntity>> UpdateProductAsync(ProductEntity product)
        {
            var response = new ServiceResponse<ProductEntity>();
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                response.Success = false;
                response.Message = "Update failed: Category name cannot be empty.";
                return response;
            }
            var nameExists = await _repository.GetByNameAsync(product.Name);

            if (nameExists != null && nameExists.Id != product.Id)
            {
                response.Success = false;
                response.Message = "Cannot update: Another item already has this name.";
                return response;
            }
            if (product.Price <= 0)
            {
                response.Success = false;
                response.Message = "Update failed: Price must be greater than zero.";
                return response;
            }

            var existingProduct = await _repository.GetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                response.Success = false;
                response.Message = $"Update failed: Product with ID {product.Id} was not found.";
                return response;
            }

            try
            {
                await _repository.UpdateAsync(product);
                response.Data = product;
                response.Message = "Product updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the database: " + ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveProductAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Error: This product does not exist in the database.";
                return response;
            }

            await _repository.DeleteAsync(id);
            response.Message = "Product successfully removed.";
            return response;
        }
        public async Task<ServiceResponse<ProductEntity>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<ProductEntity>();

            try
            {
                var product = await _repository.GetByIdAsync(id);

                if (product == null)
                {
                    response.Success = false;
                    response.Message = $"Product with ID {id} was not found.";
                    return response;
                }

                response.Data = product;
                response.Message = "Product retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred: " + ex.Message;
            }

            return response;
        }
    }
}

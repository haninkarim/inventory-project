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

        public async Task<IEnumerable<ProductEntity>> GetAvailableProductsAsync()
        {
            return (IEnumerable<ProductEntity>)await _repository.GetAllAsync();
        }
        public async Task CreateProductAsync(ProductEntity product)
    => await _repository.AddAsync(product);

        public async Task UpdateProductAsync(ProductEntity product)
            => await _repository.UpdateAsync(product);

        public async Task RemoveProductAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}

using hanin.Entities;

namespace hanin.ServiceIntefrace
{
    public interface ProductServiceInt
    {
       // Task<IEnumerable<ProductServiceInt>> GetAvailableProductsAsync();
        Task<IEnumerable<ProductEntity>> GetAvailableProductsAsync();
        Task CreateProductAsync(ProductEntity product);
        Task UpdateProductAsync(ProductEntity product);
        Task RemoveProductAsync(int id);
    }
}

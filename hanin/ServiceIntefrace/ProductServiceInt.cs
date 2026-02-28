using hanin.Entities;

namespace hanin.ServiceIntefrace
{
    public interface ProductServiceInt
    {
        Task<ServiceResponse<IEnumerable<ProductEntity>>> GetAllProductsAsync();
        Task<ServiceResponse<ProductEntity>> CreateProductAsync(ProductEntity product);
        Task<ServiceResponse<ProductEntity>> UpdateProductAsync(ProductEntity product);
        Task<ServiceResponse<bool>> RemoveProductAsync(int id);
        Task<ServiceResponse<ProductEntity>> GetProductByIdAsync(int id);
    }
}

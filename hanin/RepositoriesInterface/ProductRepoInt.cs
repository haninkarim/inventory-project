using hanin.Controller;
using hanin.Entities;

namespace hanin.RepositoriesInterface
{
    public interface ProductRepoInt
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task AddAsync(ProductEntity entity);
        Task UpdateAsync(ProductEntity entity);
        Task DeleteAsync(int id);
        Task<ProductEntity> GetByIdAsync(int id);
        Task<ProductEntity?> GetByNameAsync(string name);
    }
}


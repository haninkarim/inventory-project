using hanin.Entities;
namespace hanin.RepositoriesInterface
{
    public interface CategoryRepoInt
    {
        Task<IEnumerable<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity> GetByIdAsync(int id);
        Task AddAsync(CategoryEntity entity);
        Task UpdateAsync(CategoryEntity entity);
        Task DeleteAsync(int id);
    }
}

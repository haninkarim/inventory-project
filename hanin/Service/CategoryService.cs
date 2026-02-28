using hanin.Entities;
using hanin.RepositoriesInterface;
using hanin.ServiceIntefrace;

namespace hanin.Service
{
    public class CategoryService : CategoryServiceInt
    {
        private readonly CategoryRepoInt _repo;
        public CategoryService(CategoryRepoInt repo) => _repo = repo;

        public async Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task CreateCategoryAsync(CategoryEntity category)
        {
            await _repo.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryEntity category)
        {
            await _repo.UpdateAsync(category);
        }

        public async Task RemoveCategoryAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}

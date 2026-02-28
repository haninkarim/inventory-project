using hanin.Entities;

namespace hanin.ServiceIntefrace
{
    public interface CategoryServiceInt
    {
        Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(CategoryEntity category);
        Task UpdateCategoryAsync(CategoryEntity category);
        Task RemoveCategoryAsync(int id);
    }
}

using hanin.Entities;

namespace hanin.ServiceIntefrace
{
    public interface CategoryServiceInt
    {
      Task<ServiceResponse<IEnumerable<CategoryEntity>>> GetAllCategoriesAsync();
            Task<ServiceResponse<CategoryEntity>> CreateCategoryAsync(CategoryEntity category);
         Task<ServiceResponse<CategoryEntity>> UpdateCategoryAsync(CategoryEntity category);
        Task<ServiceResponse<bool>> RemoveCategoryAsync(int id);
    }
}

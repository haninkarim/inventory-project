using hanin.Entities;
using hanin.RepositoriesInterface;
using hanin.ServiceIntefrace;

namespace hanin.Service
{
    public class CategoryService : CategoryServiceInt
    {
        private readonly CategoryRepoInt _repo;
        public CategoryService(CategoryRepoInt repo) => _repo = repo;

        public async Task<ServiceResponse<IEnumerable<CategoryEntity>>> GetAllCategoriesAsync()
        {
            var response = new ServiceResponse<IEnumerable<CategoryEntity>>();
            var categories = await _repo.GetAllAsync();

            response.Data = categories;
            response.Message = "Categories loaded successfully.";
            return response;
        }

        public async Task<ServiceResponse<CategoryEntity>> CreateCategoryAsync(CategoryEntity category)
        {
            var response = new ServiceResponse<CategoryEntity>();

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                response.Success = false;
                response.Message = "Category name cannot be empty.";
                return response;
            }
            var nameExists = await _repo.GetByNameAsync(category.Name);

            if (nameExists != null)
            {
                response.Success = false;
                response.Message = $"Error: The name '{category.Name}' is already taken.";
                return response;
            }
           
            await _repo.AddAsync(category);
            response.Data = category;
            response.Message = "Category created successfully.";
            return response;
        }
        public async Task<ServiceResponse<CategoryEntity>> UpdateCategoryAsync(CategoryEntity category)
        {
            var response = new ServiceResponse<CategoryEntity>();

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                response.Success = false;
                response.Message = "Update failed: Category name cannot be empty.";
                return response;
            }
            var nameExists = await _repo.GetByNameAsync(category.Name);

          
            if (nameExists != null && nameExists.Id != category.Id)
            {
                response.Success = false;
                response.Message = "Cannot update: Another item already has this name.";
                return response;
            }
            var existingCategory = await _repo.GetByIdAsync(category.Id);
            if (existingCategory == null)
            {
                response.Success = false;
                response.Message = "Update failed: Category not found.";
                return response;
            }

            
            try
            {
                await _repo.UpdateAsync(category);
                response.Data = category;
                response.Message = "Category updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Database error: " + ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveCategoryAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exists = await _repo.GetByIdAsync(id);

            if (exists == null)
            {
                response.Success = false;
                response.Message = "Category not found.";
                return response;
            }

            await _repo.DeleteAsync(id);
            response.Data = true;
            response.Message = "Category and all its related products were deleted.";
            return response;
        }
    }
}

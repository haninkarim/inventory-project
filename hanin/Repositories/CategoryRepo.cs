using hanin.DBContext;
using hanin.Entities;
using hanin.RepositoriesInterface;
using Microsoft.EntityFrameworkCore;

namespace hanin.Repositories
{
    public class CategoryRepo : CategoryRepoInt
    {
        private readonly AppDbContext _context;
        public CategoryRepo(AppDbContext context) => _context = context;

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync() => await _context.Categories.ToListAsync();

        public async Task<CategoryEntity> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);

        public async Task AddAsync(CategoryEntity entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryEntity entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

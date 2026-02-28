using hanin.Controller;
using hanin.DBContext;
using hanin.Entities;
using Microsoft.EntityFrameworkCore;

namespace hanin.Repositories
{
    public class ProductRepo : RepositoriesInterface.ProductRepoInt
    {
        private readonly AppDbContext _context; 

        public ProductRepo (AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _context.ProductEntity.ToListAsync();
        }
        public async Task AddAsync(ProductEntity entity)
        {
            await _context.ProductEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductEntity entity)
        {
            _context.ProductEntity.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProductEntity.FindAsync(id);
            if (entity != null)
            {
                _context.ProductEntity.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            return await _context.ProductEntity.FindAsync(id);
        }
        public async Task<ProductEntity?> GetByNameAsync(string name)
        {
            return await _context.ProductEntity
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}

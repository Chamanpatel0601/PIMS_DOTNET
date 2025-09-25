using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                    .ThenInclude(pc => pc.Product)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                    .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
            if (existingCategory == null) return null;

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Description = category.Description;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

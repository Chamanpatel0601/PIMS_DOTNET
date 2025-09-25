using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return category == null ? null : _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO?> UpdateAsync(CategoryUpdateDTO dto)
        {
            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null) return null;

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}

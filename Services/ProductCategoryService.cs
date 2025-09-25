using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCategoryDTO>> GetAllAsync()
        {
            var productCategories = await _context.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .ToListAsync();

            return productCategories.Select(pc => new ProductCategoryDTO
            {
                ProductId = pc.ProductId,
                CategoryId = pc.CategoryId,
                ProductName = pc.Product?.Name,
                CategoryName = pc.Category?.CategoryName
            });
        }

        public async Task<IEnumerable<ProductCategoryDTO>> GetByProductIdAsync(Guid productId)
        {
            var productCategories = await _context.ProductCategories
                .Include(pc => pc.Category)
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();

            return productCategories.Select(pc => new ProductCategoryDTO
            {
                ProductId = pc.ProductId,
                CategoryId = pc.CategoryId,
                CategoryName = pc.Category?.CategoryName
            });
        }

        public async Task<IEnumerable<ProductCategoryDTO>> GetByCategoryIdAsync(int categoryId)
        {
            var productCategories = await _context.ProductCategories
                .Include(pc => pc.Product)
                .Where(pc => pc.CategoryId == categoryId)
                .ToListAsync();

            return productCategories.Select(pc => new ProductCategoryDTO
            {
                ProductId = pc.ProductId,
                CategoryId = pc.CategoryId,
                ProductName = pc.Product?.Name
            });
        }

        public async Task<ProductCategoryDTO?> AddAsync(ProductCategoryCreateDTO dto)
        {
            // Prevent duplicates
            var exists = await _context.ProductCategories
                .AnyAsync(pc => pc.ProductId == dto.ProductId && pc.CategoryId == dto.CategoryId);

            if (exists) return null;

            var entity = new ProductCategory
            {
                ProductId = dto.ProductId,
                CategoryId = dto.CategoryId
            };

            _context.ProductCategories.Add(entity);
            await _context.SaveChangesAsync();

            return new ProductCategoryDTO
            {
                ProductId = entity.ProductId,
                CategoryId = entity.CategoryId
            };
        }

        public async Task<bool> RemoveAsync(Guid productId, int categoryId)
        {
            var entity = await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

            if (entity == null) return false;

            _context.ProductCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Product> CreateAsync(Product product, IEnumerable<int> categoryIds)
        {
            if (!await IsSkuUniqueAsync(product.SKU))
                throw new Exception("SKU must be unique.");

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            foreach (var catId in categoryIds)
            {
                _context.ProductCategories.Add(new ProductCategory
                {
                    ProductId = product.ProductId,
                    CategoryId = catId
                });
            }
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(Product product, IEnumerable<int> categoryIds)
        {
            var existing = await _context.Products.FindAsync(product.ProductId);
            if (existing == null) return null;

            if (!await IsSkuUniqueAsync(product.SKU, product.ProductId))
                throw new Exception("SKU must be unique.");

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.SKU = product.SKU;

            var existingCategories = _context.ProductCategories.Where(pc => pc.ProductId == product.ProductId);
            _context.ProductCategories.RemoveRange(existingCategories);

            foreach (var catId in categoryIds)
            {
                _context.ProductCategories.Add(new ProductCategory
                {
                    ProductId = product.ProductId,
                    CategoryId = catId
                });
            }

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AdjustPriceAsync(Guid productId, decimal adjustment, bool isPercentage)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            if (isPercentage)
                product.Price -= product.Price * (adjustment / 100);
            else
                product.Price -= adjustment;

            if (product.Price < 0) product.Price = 0;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<bool> IsSkuUniqueAsync(string sku, Guid? productId = null)
        {
            return !await _context.Products.AnyAsync(p => p.SKU == sku && p.ProductId != productId);
        }
    }
}

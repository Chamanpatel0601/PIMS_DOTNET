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
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all products
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Inventory)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        // Get product by ID
        public async Task<ProductDTO?> GetByIdAsync(Guid productId)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            return product == null ? null : _mapper.Map<ProductDTO>(product);
        }

        // Create a new product
        public async Task<ProductDTO> CreateAsync(ProductCreateDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.ProductCategories = dto.CategoryIds?.Select(cId => new ProductCategory
            {
                CategoryId = cId,
                ProductId = product.ProductId
            }).ToList();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(product.ProductId);
        }

        // Update product
        public async Task<ProductDTO?> UpdateAsync(ProductUpdateDTO dto)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.ProductId == dto.ProductId);

            if (product == null) return null;

            product.Name = dto.Name;
            product.SKU = dto.SKU;
            product.Description = dto.Description;
            product.Price = dto.Price;

            // Update categories
            if (dto.CategoryIds != null)
            {
                // Remove old categories not in new list
                var toRemove = product.ProductCategories
                    .Where(pc => !dto.CategoryIds.Contains(pc.CategoryId))
                    .ToList();

                _context.ProductCategories.RemoveRange(toRemove);

                // Add new categories
                var existingIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList();
                var toAdd = dto.CategoryIds
                    .Where(cId => !existingIds.Contains(cId))
                    .Select(cId => new ProductCategory
                    {
                        ProductId = product.ProductId,
                        CategoryId = cId
                    }).ToList();

                foreach (var item in toAdd)
                {
                    product.ProductCategories.Add(item);
                }
            }

            await _context.SaveChangesAsync();
            return await GetByIdAsync(product.ProductId);
        }

        // Delete product
        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get products by category
        public async Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryId)
        {
            var products = await _context.ProductCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.Product)
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Inventory)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        // Adjust price
        public async Task<bool> AdjustPriceAsync(Guid productId, decimal amount, bool isPercentage)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            if (isPercentage)
                product.Price += product.Price * (amount / 100m);
            else
                product.Price += amount;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

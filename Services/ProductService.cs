using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;

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

        public async Task<ProductDTO> CreateAsync(ProductCreateDTO dto)
        {
            // SKU uniqueness check
            if (await _context.Products.AnyAsync(p => p.SKU == dto.SKU))
                throw new InvalidOperationException("SKU must be unique");

            var product = _mapper.Map<Product>(dto);

            // Assign categories if any
            if (dto.CategoryIds != null)
            {
                product.ProductCategories = dto.CategoryIds
                    .Select(cid => new ProductCategory { CategoryId = cid, Product = product }).ToList();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Inventory)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetByIdAsync(Guid productId)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            return product == null ? null : _mapper.Map<ProductDTO>(product);
        }

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

        public async Task<ProductDTO?> UpdateAsync(ProductUpdateDTO dto)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.ProductId == dto.ProductId);

            if (product == null) return null;

            // Check SKU uniqueness
            if (await _context.Products.AnyAsync(p => p.SKU == dto.SKU && p.ProductId != dto.ProductId))
                throw new InvalidOperationException("SKU must be unique");

            product.Name = dto.Name;
            product.SKU = dto.SKU;
            product.Description = dto.Description;
            product.Price = dto.Price;

            // Update categories
            if (dto.CategoryIds != null)
            {
                product.ProductCategories.Clear();
                foreach (var cid in dto.CategoryIds)
                {
                    product.ProductCategories.Add(new ProductCategory { ProductId = product.ProductId, CategoryId = cid });
                }
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> AdjustPriceAsync(Guid productId, decimal amount, bool isPercentage)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            if (isPercentage)
            {
                product.Price += product.Price * amount / 100;
            }
            else
            {
                product.Price += amount;
            }

            // Prevent negative price
            if (product.Price < 0) product.Price = 0;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

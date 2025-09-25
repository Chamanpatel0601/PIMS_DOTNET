using AutoMapper;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PIMS_DOTNET.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ---------------- Product ----------------
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Categories,
                           opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));

            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();

            // ---------------- Category ----------------
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            // ---------------- Inventory ----------------
            CreateMap<Inventory, InventoryDTO>();
            CreateMap<InventoryAdjustDTO, Inventory>();

            // ---------------- InventoryTransaction ----------------
            CreateMap<InventoryTransaction, InventoryTransactionDTO>();

            // ---------------- User ----------------
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleName,
                           opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<UserRegisterDTO, User>();

            // ---------------- Role ----------------
            CreateMap<Role, RoleDTO>();

            // ---------------- AuditLog ----------------
            CreateMap<AuditLog, AuditLogDTO>()
                .ForMember(dest => dest.Username,
                           opt => opt.MapFrom(src => src.User != null ? src.User.Username : null));

            // ---------------- ProductCategory ----------------
            CreateMap<ProductCategory, ProductCategoryDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<ProductCategoryCreateDTO, ProductCategory>();
        }
    }
}

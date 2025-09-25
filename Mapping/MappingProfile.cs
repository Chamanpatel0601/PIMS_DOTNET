using AutoMapper;
using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;

namespace PIMS_DOTNET.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product mapping
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Categories,
                           opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.Inventory,
                           opt => opt.MapFrom(src => src.Inventory))
                .ReverseMap();

            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();



            // Category mapping
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            // Inventory mapping
            CreateMap<Inventory, InventoryDTO>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionDTO>().ReverseMap();

            // User mapping (include RoleName)
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : null))
                .ReverseMap();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();

            // Role mapping
            CreateMap<Role, RoleDTO>().ReverseMap();

            // AuditLog mapping
            CreateMap<AuditLog, AuditLogDTO>().ReverseMap();

            // ProductCategory mapping
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
            CreateMap<ProductCategoryCreateDTO, ProductCategory>();
        }
    }
}

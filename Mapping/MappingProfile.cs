using AutoMapper;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;

namespace PIMS_DOTNET.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            CreateMap<Inventory, InventoryDTO>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();

            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<AuditLog, AuditLogDTO>().ReverseMap();

            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
            CreateMap<ProductCategoryCreateDTO, ProductCategory>();
        }
    }
}

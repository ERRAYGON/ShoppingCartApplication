using AutoMapper;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.Models.DTO;

namespace ShoppingCartApplication.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<UserRegistrationDTO, User>();
                //.ForMember(user => user.Name, )

            CreateMap<User, UserTypeDTO>()
                .ForMember(userTypeDTO => userTypeDTO.User, opt => opt.MapFrom(user => user));
            CreateMap<UserType, UserTypeDTO>().ReverseMap();
            CreateMap<ProductDTO, Products>().ReverseMap();
        }
    }
}

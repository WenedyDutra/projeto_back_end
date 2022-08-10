using AutoMapper;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Models;

namespace ProjectEllevo.API.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserModel, UserEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName)).ReverseMap()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password)).ReverseMap()
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF)).ReverseMap()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone)).ReverseMap()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)).ReverseMap();
        }
    }
}

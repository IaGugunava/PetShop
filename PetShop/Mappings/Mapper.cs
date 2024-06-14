using AutoMapper;
using PetShop.DTO;
using PetShop.Models;

namespace PetShop.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}

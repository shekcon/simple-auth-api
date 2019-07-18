using AutoMapper;
using Auth.API.Resources;
using Auth.API.Models;
using Auth.API.Responses;

namespace Auth.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterResource>();
            CreateMap<RegisterResource, User>();
            CreateMap<User, LoginResource>();
            CreateMap<LoginResource, User>();
            CreateMap<User, UserResource>();
            CreateMap<UserResource, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();
        }
    }
}
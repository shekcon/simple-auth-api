using AutoMapper;
using Auth.API.Resources;
using Auth.API.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.Common;
using System;
// using Auth.API.Format;

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
        }
    }
}
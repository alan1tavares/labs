using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoMapperAPI
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserViewModelOther>()
                .ForMember(dest => 
                    dest.FName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => 
                    dest.LName,
                    opt => opt.MapFrom(src => src.LastName));
        }
    }
}
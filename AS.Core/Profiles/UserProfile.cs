using System.IO;
using AS.Core.Models;
using AS.Core.ViewModels;
using AutoMapper;

namespace AS.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.PhotoPath
                    , opt => opt.MapFrom(src => "Photos/" + Path.GetFileName(src.PhotoPath)));
            CreateMap<UserViewModel, User>();
        }
    }
}
using System;
using System.IO;
using System.Linq;
using AS.Core.Data;
using AS.Core.Models;
using AS.Core.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AS.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.PhotoPath
                    , opt => opt.MapFrom(src => "~/Photos/" + Path.GetFileName(src.PhotoPath)))
                .ForMember(x => x.Rewards,
                    opt => opt.MapFrom(src =>
                        src.Rewards.Where(y => y.UserId == src.Id).Select(x => x.Reward).ToList()));
            CreateMap<UserViewModel, User>();
            CreateMap<User, EditUserViewModel>().ForMember(x => x.PhotoPath
                , opt => opt.MapFrom(src => "~/Photos/" + Path.GetFileName(src.PhotoPath)));
            CreateMap<EditUserViewModel, User>();
        }
    }
}
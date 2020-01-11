using System.IO;
using AS.Core.Models;
using AS.Core.ViewModels;
using AutoMapper;

namespace AS.Core.Profiles
{
    public class RewardProfile : Profile
    {
        public RewardProfile()
        {
            CreateMap<Reward, RewardViewModel>()
                .ForMember(x => x.ImagePath
                    , opt => opt.MapFrom(src => "~/Images/" + Path.GetFileName(src.ImagePath)));
            CreateMap<RewardViewModel, Reward>();
        }
    }
}
using AutoMapper;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Mappings
{
    public class ActivityMapping : Profile
    {
        public ActivityMapping()
        {
            CreateMap<ActivityModel, ActivityEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)).ReverseMap()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId)).ReverseMap();
        }
    }
}

using AutoMapper;
using ProjectEllevo.API.Entitiy;
using ProjectEllevo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Mappings
{
    public class TaskMapping : Profile
    {
        public TaskMapping()
        {
            CreateMap<TaskModel, TaskEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Generator, opt => opt.MapFrom(src => src.Generator)).ReverseMap()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)).ReverseMap()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap()
                .ForMember(dest => dest.Responsible, opt => opt.MapFrom(src => src.Responsible)).ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)).ReverseMap()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities)).ReverseMap();

            CreateMap<ActivityModel, ActivityEntity>()
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.ActivityId)).ReverseMap()
                .ForMember(dest => dest.ActivityTitle, opt => opt.MapFrom(src => src.ActivityTitle)).ReverseMap()
                .ForMember(dest => dest.ActivityDescription, opt => opt.MapFrom(src => src.ActivityDescription)).ReverseMap();
        }
    }
}

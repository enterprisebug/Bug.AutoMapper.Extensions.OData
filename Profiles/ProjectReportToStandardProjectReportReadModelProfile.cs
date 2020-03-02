using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OdataAutomapperServerQuery.Models;
using OdataAutomapperServerQuery.ReadModel;

namespace OdataAutomapperServerQuery.Profiles
{
    public class ProjectReportToStandardProjectReportReadModelProfile : Profile
    {
        public ProjectReportToStandardProjectReportReadModelProfile()
        {
            CreateMap<ProjectReport, StandardProjectReportReadModel>()
                .ForMember(
                    dest => dest.OptionNumber,
                    opt => opt.MapFrom(src => src.OptionNo)
                )
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.OptionId)
                )
                .ReverseMap();

        }
    }
}

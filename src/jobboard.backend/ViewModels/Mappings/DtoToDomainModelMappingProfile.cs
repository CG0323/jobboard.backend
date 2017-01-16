using AutoMapper;
using jobboard.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobboard.backend.ViewModels.Mappings
{
    public class DtoToDomainModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SkillDto, Skill>();
            Mapper.CreateMap<JobDto, Job>();
            Mapper.CreateMap<ContentDto, Content>();
            Mapper.CreateMap<SkillDto, Skill>();
        }
    }
}

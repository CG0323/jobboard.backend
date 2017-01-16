using AutoMapper;
using jobboard.Model.Entities;
using System.Linq;

namespace jobboard.backend.ViewModels.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Skill, SkillDto>();
            Mapper.CreateMap<JobSkill, JobSkillDto>();
            Mapper.CreateMap<Job, JobDto>()
                .ForMember(vm => vm.Skills, (map) => map.MapFrom(j => j.JobSkills));
            Mapper.CreateMap<Content, ContentDto>();
            Mapper.CreateMap<Skill, SkillDto>()
                .ForMember(vm => vm.Temperature, (map) => map.MapFrom(s => s.JobSkills.Count));
            Mapper.CreateMap<Job, JobDetailDto>()
                .ForMember(vm => vm.Content, (map) => map.MapFrom(j => j.Content.Text));
        }
    }
}
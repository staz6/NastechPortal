using AutoMapper;
using EventBus.Messages.Events;
using ProjectManagement.Dto;
using ProjectManagement.Entites;

namespace ProjectManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectCreateDto,Project>();
            CreateMap<ProjectPutDto,Project>().ReverseMap();
            CreateMap<ProjectMemberCreateDto,ProjectMember>();
            CreateMap<Project,ProjectGetDto>();
            CreateMap<GetUserByIdEventResponse,ProjectMemeberGetDto>().ForMember(dest => dest.Id, opt => opt.Ignore());;
            CreateMap<ProjectMemberCreateDto,ProjectMember>();
        }
    }
}
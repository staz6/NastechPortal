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
            CreateMap<Project,ProjectGetDto>()
                    .ForMember(dest => dest.ProjectLeadProfileImage, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectLeadName, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectLeadEmail, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectLeadEmployeeId, opt => opt.Ignore());
                    

            //ProjectMember
            CreateMap<GetUserByIdEventResponse,ProjectMemeberGetDto>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProjectMemberCreateDto,ProjectMember>();
            CreateMap<ProjectMemberCreateDtoWithId,ProjectMember>();

            //ProjectSubFOlder
            CreateMap<ProjectFolderCreateDto,ProjectSubFolder>();
            CreateMap<ProjectSubFolder,ProjectFolderGetDto>()
                            .ForMember( x=> x.CreatedAt,o => o.MapFrom(m => m.CreatedAt.ToString("MM/dd/yyyy")));

            //ProjectPhase
            CreateMap<ProjectPhaseCreateDto,ProjectPhase>();

            //ProjectTask
            CreateMap<ProjectTask,ProjectTaskGetDto>().ForMember(x => x.SubTask, o => o.MapFrom(m => m.ProjectSubTasks.Count));
            CreateMap<ProjectTaskAsignee,ProjectTaskAsigneeGetDto>();
            CreateMap<GetUserByIdEventResponse,ProjectTaskAsigneeGetDto>().ForMember(dest => dest.Id, opt => opt.Ignore());
            
        }
    }
}
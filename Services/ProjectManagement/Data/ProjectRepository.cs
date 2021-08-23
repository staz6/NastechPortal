using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using ProjectManagement.Dto;
using ProjectManagement.Entites;
using ProjectManagement.Interface;

namespace ProjectManagement.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRequestClient<GetUserByIdEventRequest> _requestClient;
        public ProjectRepository(AppDbContext context, IMapper mapper, IRequestClient<GetUserByIdEventRequest> requestClient)
        {
            _requestClient = requestClient;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IReadOnlyList<ProjectGetDto>> GetProjectListWithAsigneeInfo(IReadOnlyList<Project> obj)
        {
            try{
                var mapObject = _mapper.Map<IReadOnlyList<ProjectGetDto>>(obj);
            for(int i =0;i<obj.Count;i++)
            {
                try{
                    var request = _requestClient.Create(new GetUserByIdEventRequest{Id=obj[i].ProjectLead});
                    var response = await request.GetResponse<GetUserByIdEventResponse>();
                    mapObject[i].ProjectLeadName=response.Message.Name;
                    mapObject[i].ProjectLeadEmail=response.Message.Email;
                    mapObject[i].ProjectLeadProfileImage=response.Message.ProfileImage;
                    mapObject[i].ProjectLeadEmployeeId=response.Message.EmployeeId;
                }
                catch{

                }
                foreach(var item in obj[i].ProjectMembers)
                {
                    
                    var request =  _requestClient.Create(new GetUserByIdEventRequest{Id=item.AsigneeId});
                    try{
                       var response = await request.GetResponse<GetUserByIdEventResponse>();
                        if(response!=null)
                        {
                            var mapResponseObject = _mapper.Map<ProjectMemeberGetDto>(response.Message);
                            mapResponseObject.Id=item.Id;
                            if(mapObject[i].Asignee==null) mapObject[i].Asignee = new List<ProjectMemeberGetDto>();
                            mapObject[i].Asignee.Add(mapResponseObject);
                            
                        } 
                    }
                    catch{
                        
                    }
                    
                    
   
                }
            }
            return mapObject;
            }
            catch{
                throw new Exception();
            }
            
        }

        public async Task<ProjectGetDto> GetProjectWithAsineeInfo(Project obj)
        {
            try{
                var mapObject = _mapper.Map<ProjectGetDto>(obj);
                foreach(var item in obj.ProjectMembers)
                {
                    var request = _requestClient.Create(new GetUserByIdEventRequest{Id=item.AsigneeId});
                    try{
                        var response = await request.GetResponse<GetUserByIdEventResponse>();
                        if(response!=null)
                        {
                            var mapResponseObject = _mapper.Map<ProjectMemeberGetDto>(response.Message,opt =>  opt.AfterMap((src, dest) => dest.Id = item.Id));
                            mapResponseObject.Id=item.Id;
                            if(mapObject.Asignee==null) mapObject.Asignee = new List<ProjectMemeberGetDto>();
                            mapObject.Asignee.Add(mapResponseObject);
                            
                        }
                    }
                    catch{

                    }
                }
                return mapObject;
            }
            catch{
                throw new Exception();
            }
        }
    }
}
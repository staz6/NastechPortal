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
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRequestClient<GetUserByIdEventRequest> _requestClient;
        public TaskRepository(AppDbContext context, IMapper mapper, IRequestClient<GetUserByIdEventRequest> requestClient)
        {
            _requestClient = requestClient;
            _mapper = mapper;
            _context = context;
        }

        public Task<IReadOnlyList<ProjectTaskAsigneeGetDto>> getProjectTaskListWithAsignee(List<ProjectTask> obj)
        {
            throw new Exception();
        }
    }
}
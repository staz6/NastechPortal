using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Dto;
using ProjectManagement.Entites;

namespace ProjectManagement.Interface
{
    public interface ITaskRepository 
    {
        Task<IReadOnlyList<ProjectTaskAsigneeGetDto>> getProjectTaskListWithAsignee(List<ProjectTask> obj);
    }
}
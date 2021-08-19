using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Dto;
using ProjectManagement.Entites;

namespace ProjectManagement.Interface
{
    public interface IProjectRepository 
    {
        Task<IReadOnlyList<ProjectGetDto>> GetProjectListWithAsigneeInfo(IReadOnlyList<Project> obj);
        Task<ProjectGetDto> GetProjectWithAsineeInfo(Project obj);
    }
}
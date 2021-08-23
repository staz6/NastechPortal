using ProjectManagement.Entites;
using ProjectManagement.Interface;

namespace ProjectManagement.Specification
{
    public class ProjectFolderSpecification : BaseSpecification<ProjectSubFolder>
    {
        public ProjectFolderSpecification(int id) : base(x => x.ProjectId==id)
        {
        }
    }
}
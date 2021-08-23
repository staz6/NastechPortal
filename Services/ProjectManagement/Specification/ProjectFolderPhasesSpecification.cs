using ProjectManagement.Entites;
using ProjectManagement.Interface;

namespace ProjectManagement.Specification
{
    public class ProjectFolderPhasesSpecifcation : BaseSpecification<ProjectPhase>
    {
        public ProjectFolderPhasesSpecifcation(int id) : base(x => x.ProjectSubFolderId==id)
        {
        }
    }
}
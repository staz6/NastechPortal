using ProjectManagement.Entites;
using ProjectManagement.Interface;

namespace ProjectManagement.Specification
{
    public class ProjectPhaseTaskSpecification : BaseSpecification<ProjectTask>
    {
        public ProjectPhaseTaskSpecification(int id): base(x => x.ProjectPhaseId == id)
        {
            AddInclude(x => x.ProjectSubTasks);
        }
    }
}
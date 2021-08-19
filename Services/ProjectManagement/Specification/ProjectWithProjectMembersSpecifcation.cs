using System;
using System.Linq.Expressions;
using ProjectManagement.Entites;
using ProjectManagement.Interface;

namespace ProjectManagement.Specification
{
    public class ProjectWithProjectMembersSpecifcation : BaseSpecification<Project>
    {
        public ProjectWithProjectMembersSpecifcation()
        {
            AddInclude(x => x.ProjectMembers);
        }

        public ProjectWithProjectMembersSpecifcation(int id ) : base(x => x.Id==id)
        {
            AddInclude(x => x.ProjectMembers);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Dto;
using ProjectManagement.Entites;
using ProjectManagement.Helper;
using ProjectManagement.Interface;
using ProjectManagement.Specification;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProjectTask> _repo;
        public ProjectTaskController(IGenericRepository<ProjectTask> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("task/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProjectTaskGetDto>>> getTask(int id)
        {
            var spec =new ProjectPhaseTaskSpecification(id);
            var obj =await _repo.ListAsyncWithSpec(spec);
            if(obj==null) return NoContent();
            return Ok();

        }
}
}
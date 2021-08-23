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
    public class ProjectFolderController : ControllerBase
    {
        private readonly IGenericRepository<ProjectSubFolder> _repo;
        private readonly IMapper _mapper;

        private readonly IProjectRepository _projectRepo;

        public ProjectFolderController(IGenericRepository<ProjectSubFolder> repo, IMapper mapper, IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;


            _mapper = mapper;
            _repo = repo;
        }
        
        [HttpPost("projectFolder")]
        public async Task<ActionResult> postProjectFolder(ProjectFolderCreateDto model)
        {
            if(!ModelState.IsValid) return BadRequest();
            try{
                var mapObject=_mapper.Map<ProjectSubFolder>(model);
                _repo.Insert(mapObject);
                await _repo.Save();
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.CreateSuccess));
            }
            catch{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
        }
        [HttpGet("projectFolder/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProjectFolderGetDto>>> getProjectFolder(int id)
        {
           var spec = new ProjectFolderSpecification(id);
           var obj = await _repo.ListAsyncWithSpec(spec);
           if(obj==null) return NoContent();
           var mapObj = _mapper.Map<IReadOnlyList<ProjectFolderGetDto>>(obj);
            return Ok(mapObj);
        }

    }
}
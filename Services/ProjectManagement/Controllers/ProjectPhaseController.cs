using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entites;
using ProjectManagement.Interface;
using ProjectManagement.Helper;
using System.Collections.Generic;
using ProjectManagement.Specification;
using ProjectManagement.Dto;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectPhaseController : ControllerBase
    {
        private readonly IGenericRepository<ProjectPhase> _repo;
        private readonly IMapper _mapper;

       
        private readonly IGenericRepository<ProjectSubFolder> _projectSubRepo;

        public ProjectPhaseController(IGenericRepository<ProjectPhase> repo, IGenericRepository<ProjectSubFolder> projectSubRepo, IMapper mapper)
        {
            _projectSubRepo = projectSubRepo;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("projectPhase/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProjectPhasesGetDto>>> getProjectPhase(int id)
        {
            var spec = new ProjectFolderPhasesSpecifcation(id);
            var obj = await _repo.ListAsyncWithSpec(spec);
            var mapObject = _mapper.Map<IReadOnlyList<ProjectPhasesGetDto>>(obj);
            return Ok(mapObject);
        }

        [HttpPost("ProjectPhase/{id}")]
        public async Task<ActionResult> postProjectPhase(ProjectPhaseCreateDto model)
        {
            if (!ModelState.IsValid) BadRequest();
            var project = await _projectSubRepo.GetById(model.ProjectSubFolderId);
            if(project==null) return NotFound();
            var mapObject=_mapper.Map<ProjectPhase>(model);
            _repo.Insert(mapObject);
            await _repo.Save();
            return Ok(new ApiErrorResponse(ErrorStatusCode.CreateSuccess));
        }

        [HttpPost("defaultProjectPhase/{id}")]
        public async Task<ActionResult> defaultProjectPhase(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var project = await _projectSubRepo.GetById(id);
            if(project==null) return NotFound();
            var spec = new ProjectFolderPhasesSpecifcation(id);
            var check = await _repo.ListAsyncWithSpec(spec);
            if(check.Count==0)
            {
                ProjectPhase obj = new ProjectPhase{
                    Name="To Do",Color="grey",ProjectSubFolderId=project.Id
                };
                _repo.Insert(obj);
                ProjectPhase obj1 = new ProjectPhase{
                    Name="Pending",Color="yello",ProjectSubFolderId=project.Id
                };
                _repo.Insert(obj1);
                ProjectPhase obj2 = new ProjectPhase{
                    Name="In Progress",Color="orange",ProjectSubFolderId=project.Id
                };
                _repo.Insert(obj2);
                ProjectPhase obj3 = new ProjectPhase{
                    Name="In Review",Color="blue",ProjectSubFolderId=project.Id
                };
                _repo.Insert(obj3);
                ProjectPhase obj4 = new ProjectPhase{
                    Name="Done",Color="green",ProjectSubFolderId=project.Id
                };
                _repo.Insert(obj4);
                await _repo.Save();
                return Ok(new ApiErrorResponse(ErrorStatusCode.CreateSuccess));

            }
            return BadRequest("Project Phases already exist");
            
        }

    }
}
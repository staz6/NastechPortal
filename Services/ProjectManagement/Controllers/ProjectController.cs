using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Dto;
using ProjectManagement.Entites;
using ProjectManagement.Helper;
using ProjectManagement.Helpers;
using ProjectManagement.Interface;
using ProjectManagement.Specification;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IGenericRepository<Project> _repo;
        private readonly IMapper _mapper;

        private readonly IProjectRepository _projectRepo;

        public ProjectController(IGenericRepository<Project> repo, IMapper mapper, IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;


            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// GetAllProject
        /// </summary>
        /// <returns>IReadOnlyList Project</returns>
        [HttpGet("project")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]

        
        public async Task<ActionResult<IReadOnlyList<ProjectGetDto>>> getAllProject()
        {
            if (!ModelState.IsValid) return BadRequest();
            var spec = new ProjectWithProjectMembersSpecifcation();
            var obj = await _repo.ListAsyncWithSpec(spec);
            try{
                var result = await _projectRepo.GetProjectListWithAsigneeInfo(obj);

                return Ok(result);
            }
            catch{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
            

        }

        /// <summary>
        /// GetAllProject
        /// </summary>
        /// <returns>IReadOnlyList Project</returns>
        [HttpGet("project/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]

        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Project>> getProjectById(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var spec = new ProjectWithProjectMembersSpecifcation(id);
            var obj = await _repo.GetEntityWithSpec(spec);
            if(obj==null) return NotFound("No Project matching the specify id was found");
            try{
                var result = await _projectRepo.GetProjectWithAsineeInfo(obj);
                return Ok(result);
            }
            catch{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
    
        }


        /// <summary>
        /// POST Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("project")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> postProject(ProjectCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = _mapper.Map<Project>(model);
            _repo.Insert(obj);
            await _repo.Save();
            return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.CreateSuccess));

        }

        [HttpPut("project/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        public async Task<ActionResult> putProject(int id, ProjectPutDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = await _repo.GetById(id);
            _mapper.Map(model, obj);
            await _repo.Save();
            return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.UpdateSuccess));
        }


        ///Patch request to update specific fields see microsoft documentation for more info
        [HttpPatch("project/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        public async Task<ActionResult> patchProject(int id, JsonPatchDocument<ProjectPutDto> model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = await _repo.GetById(id);
            if (obj == null) return NotFound();
            var objToPatch = _mapper.Map<ProjectPutDto>(obj);
            model.ApplyTo(objToPatch);
            _mapper.Map(objToPatch, obj);
            _repo.Update(obj);
            await _repo.Save();
            return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.UpdateSuccess));

        }


    }
}
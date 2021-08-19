using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Dto;
using ProjectManagement.Entites;
using ProjectManagement.Helper;
using ProjectManagement.Helpers;
using ProjectManagement.Interface;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectMemberController : ControllerBase
    {
        private readonly IGenericRepository<ProjectMember> _repo;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projecrRepo;
        public ProjectMemberController(IGenericRepository<ProjectMember> repo, IMapper mapper, IProjectRepository projecrRepo)
        {
            _projecrRepo = projecrRepo;
            _mapper = mapper;
            _repo = repo;
        }

         /// <summary>
        /// POST ProjectMember
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("projectMember")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> postProject(ProjectMemberCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = _mapper.Map<ProjectMember>(model);
            _repo.Insert(obj);
            await _repo.Save();
            return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.CreateSuccess));

        }


        [HttpDelete("projectMember/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
         [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        public async Task<ActionResult> deleteProjectMemeber(int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            if(_repo.GetById(id)==null) return NotFound();
            _repo.Delete(id);
            await _repo.Save();
            return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.DeleteSuccess));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using InventoryMangement.Dto;
using InventoryMangement.Helpers;
using InventoryMangement.Interface;
using InventoryMangment.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMangement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryRequestController : ControllerBase
    {
        private readonly IGenericRepository<InventoryRequest> _requestRepo;
        private readonly IMapper _mapper;
        private readonly ISpecificRepository _repo;
        public InventoryRequestController(IGenericRepository<InventoryRequest> requestRepo, IMapper mapper, ISpecificRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
            _requestRepo = requestRepo;
        }

        /// <summary>
        /// GET all inventory request
        /// </summary>
        /// <returns>List of Inventory Request</returns>
        [HttpGet("inventoryRequest")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<IReadOnlyList<InventoryRequest>> getInventoryRequest()
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_requestRepo.GetAll());
        }

        /// <summary>
        /// GET indiviual employee inventory request
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of inventory request</returns>

        [HttpGet("inventoryRequest/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<IReadOnlyList<InventoryRequest>> getInventoryRequestByUserId(string userId)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(_repo.getEmployeeInventoryRequest(userId));
        }

        /// <summary>
        /// PUT api to approved inventory request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("approvedInventoryRequest/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public ActionResult<IReadOnlyList<InventoryRequest>> approvedInventoryRequest(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var obj = _requestRepo.GetById(id);
            if(obj == null) return BadRequest();
            obj.Status=true;
            obj.DateApproved=DateTime.Now;
            obj.ApprovedBy=userId;
            _requestRepo.Update(obj);
            _requestRepo.Save();
            return Accepted();
        }

        /// <summary>
        /// PUT api to enable returned status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut("returnedInventoryRequest/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public ActionResult<IReadOnlyList<InventoryRequest>> getInventoryRequestByUserId(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var obj = _requestRepo.GetById(id);
            if(obj == null) return BadRequest();
            obj.Returned=true;
            _requestRepo.Update(obj);
            _requestRepo.Save();
            return Accepted();
        }


        /// <summary>
        /// POST Inventory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("inventoryRequest")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "")]

        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult postInventoryRequest(EmployeePostInventoryRequest model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model == null) return BadRequest();
            var obj = _mapper.Map<InventoryRequest>(model);
            obj.Date = DateTime.Now;
            obj.Status = false;
            obj.Returned=false;

            _requestRepo.Insert(obj);
            _requestRepo.Save();
            return Accepted();

        }


    }
}
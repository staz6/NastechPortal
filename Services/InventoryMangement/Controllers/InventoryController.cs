using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using InventoryMangement.Dto;
using InventoryMangement.Helpers;
using InventoryMangement.Interface;
using InventoryMangment.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMangment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IGenericRepository<Inventory> _inventoryRepo;
        private readonly IMapper _mapper;
        public InventoryController(IGenericRepository<Inventory> inventoryRepo, IMapper mapper)
        {
            _mapper = mapper;
            _inventoryRepo = inventoryRepo;
        }

        /// <summary>
        /// Get All Inventory
        /// </summary>
        /// <returns></returns>

        [HttpGet("inventory")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IReadOnlyList<Inventory>>> getInventory()
        {
            if(!ModelState.IsValid) return BadRequest();
            return Ok(await _inventoryRepo.GetAll());
        }


        /// <summary>
        /// GET Inventory by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Inventory</returns>
        [HttpGet("inventory/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Inventory>> getInventoryById(int id)
        { 
              if(!ModelState.IsValid) return BadRequest();
            return Ok(await _inventoryRepo.GetById(id));
        }

        /// <summary>
        /// POST Inventory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("inventory")]
        // [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> postInventory(AdminPostInventory model)
        {
            if(!ModelState.IsValid) return BadRequest();
            if(model == null) return BadRequest();
            var obj = _mapper.Map<Inventory>(model);
             _inventoryRepo.Insert(obj);
            await _inventoryRepo.Save();
            return Accepted();
            
        }


        /// <summary>
        /// EDIT Inventory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("inventory/{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> putInventory(int id,EditInventory model)
        {
            if(!ModelState.IsValid) return BadRequest();
            if(model == null) return BadRequest();
            var obj = await _inventoryRepo.GetById(id);
            _mapper.Map(model,obj);
            //  _inventoryRepo.Update(obj);
            await _inventoryRepo.Save();
            return Accepted();
            
        }

        [HttpPut("inventoryQuantity/{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> putInventory(int id,InventoryQuantity model)
        {
            if(!ModelState.IsValid) return BadRequest();
            if(model == null) return BadRequest();
            var obj = await _inventoryRepo.GetById(id);
            if(model.value==true) obj.Quantity++;
            else obj.Quantity--;
             _inventoryRepo.Update(obj);
            await _inventoryRepo.Save();
            return Accepted();
            
        }

        /// <summary>
        /// Delete Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("inventory/{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> putInventory(int id)
        {
            if(!ModelState.IsValid) return BadRequest();
             _inventoryRepo.Delete(id);
            await _inventoryRepo.Save();
            
            return Accepted();
            
        }
    }
}
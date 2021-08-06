using AutoMapper;
using InventoryMangement.Dto;
using InventoryMangment.Entities;

namespace InventoryMangement.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {  
            CreateMap<AdminPostInventory,Inventory>();
            CreateMap<EmployeePostInventoryRequest,InventoryRequest>();
            CreateMap<EditInventory,Inventory>();
        }
    }
}
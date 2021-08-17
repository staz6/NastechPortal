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
            CreateMap<InventoryRequest,EmployeeInventoryRequest>()
            .ForMember(x => x.Date, o => o.MapFrom(m => m.Date.ToString("MM/dd/yyyy")))
            .ForMember(x => x.DateApproved, o => o.MapFrom(m => m.DateApproved.Year==01 ? "null" : m.DateApproved.ToString("MM/dd/yyyy")))
            .ForMember(x => x.Status, o => o.MapFrom(m => m.Status == false ? "Pending" : "Approved"))
            .ForMember(x => x.InventoryName, o => o.MapFrom(m => m.Inventorys.Item));
            
        }
    }
}
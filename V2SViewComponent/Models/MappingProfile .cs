using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2SViewComponent.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap method is used to map data between Employee and EmployeeModel.
            CreateMap<Employee, EmployeeModel>().ForMember(dest => dest.Address, opts => opts.MapFrom(src => new Address
            {
                City = src.City,
                State = src.State
            }));
            CreateMap<EmployeeModel, Employee>();
        }
    }
}

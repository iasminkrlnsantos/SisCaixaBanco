using AutoMapper;
using SisCaixaBanco.DTO;
using SisCaixaBanco.Models;

namespace SisCaixaBanco.AutoMapper
{
    public class ContaLogProfile : Profile
    {
        protected void Configure()
        {
            CreateMap<ContaLogDTO, ContaLog>();
            CreateMap<ContaLog, ContaLogDTO>();
      
        }
    }
}

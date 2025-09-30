using AutoMapper;
using SisCaixaBanco.DTO;
using SisCaixaBanco.Models;

namespace SisCaixaBanco.AutoMapper
{
    public class ContaProfile : Profile
    {
        public ContaProfile()
        {
            CreateMap<Conta, ContaDTO>();
            CreateMap<ContaDTO, Conta>();
        }

    }
}

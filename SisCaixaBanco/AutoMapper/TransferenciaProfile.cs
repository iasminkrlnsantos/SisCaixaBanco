using AutoMapper;
using SisCaixaBanco.DTO;
using SisCaixaBanco.Models;

namespace SisCaixaBanco.AutoMapper
{
    internal class TransferenciaProfile : Profile
    {
        protected void Configure()
        {
            CreateMap<Transferencia, TransferenciaDTO>();
            CreateMap<TransferenciaDTO, Transferencia>();
        }
    }
}

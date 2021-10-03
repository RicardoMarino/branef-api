using AutoMapper;
using Branef.API.Contratos;
using Branef.Negocio.Models;

namespace Branef.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Cliente, ClienteContrato>().ReverseMap();
        }
    }
}
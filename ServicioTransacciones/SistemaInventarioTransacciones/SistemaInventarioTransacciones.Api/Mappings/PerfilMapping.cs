using AutoMapper;
using SistemaInventarios.Dominio.Entidades;
using SistemaInventarioTransacciones.Aplicacion.DTOs;

namespace SistemaInventarioTransacciones.Api.Mappings
{
    public class PerfilMapping : Profile
    {
        public PerfilMapping() 
        {
            CreateMap<EntidadRegistroTransaccion, Transaccion>();
            CreateMap<Transaccion, EntidadRegistroTransaccion>();
        }
    }
}

using AutoMapper;
using SistemaInventarios.Aplicacion.DTOs;
using SistemaInventarios.Dominio.Entidades;

namespace SistemaInventarios.ProductosApi.Mappings
{
    public class PerfilMappings: Profile
    {
        public PerfilMappings() 
        {
            CreateMap<EntidadRegistroProducto, Producto>();
            CreateMap<Producto, EntidadRegistroProducto>();
        } 
    }
}

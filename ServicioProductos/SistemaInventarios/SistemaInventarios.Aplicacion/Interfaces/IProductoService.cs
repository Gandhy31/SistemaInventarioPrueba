using SistemaInventarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarios.Aplicacion.Interfaces
{
    public interface IProductoService
    {
        public Task<bool> AgregarProducto(Producto producto);
        public Task<Producto> Buscar(int id);
        public Task<List<Producto>> BuscarPor(Expression<Func<Producto, bool>> filtro);
        public Task<bool> Eliminar(int id);
        public Task<bool> Actualizar(Producto producto);
        public Task<List<Producto>> ObtenerTodos();
        public Task<bool> ValidarTransaccion(int idProducto, int cantidad);
        public Task<bool> AgregarStock(int idProducto, int cantidad);
        public Task<bool> ReducirStock(int idProducto, int cantidad);
        //public Task<Producto> BuscarPor()
    }
}

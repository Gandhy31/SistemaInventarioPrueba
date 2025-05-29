using SistemaInventarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarios.Dominio.Interfaces
{
    public interface IProductoRepository
    {
        public Task<Producto> Buscar(int id);
        public Task<List<Producto>> ObtenerTodos();
        public Task<bool> Insertar(Producto producto);
        public Task<bool> Eliminar(Producto producto);
        public Task<bool> Actualizar(Producto producto);
        public Task<List<Producto>> BuscarPor(Expression<Func<Producto, bool>> filtro);
    }
}

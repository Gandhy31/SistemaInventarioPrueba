using SistemaInventarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarios.Dominio.Interfaces
{
    public interface ITransaccionRepository
    {
        public Task<Transaccion> Buscar(int id);
        public Task<List<Transaccion>> ObtenerTodos();
        public Task<bool> Insertar(Transaccion producto);
        public Task<bool> Eliminar(Transaccion producto);
        public Task<bool> Actualizar(Transaccion producto);
        public Task<List<Transaccion>> BuscarPor(Expression<Func<Transaccion, bool>> filtro);
    }
}

using SistemaInventarios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioTransacciones.Aplicacion.Interfaces
{
    public interface ITransaccionService
    {
        public Task<Transaccion> Buscar(int id);
        public Task<List<Transaccion>> ObtenerTodas();
        public Task<bool> AgregarTransaccionVenta(Transaccion transaccion);
        public Task<bool> AgregarTransaccionCompra(Transaccion transaccion);
        public Task<bool> Eliminar(int idTransaccion);
        public Task<bool> Actualizar(Transaccion transaccion);
    }
}

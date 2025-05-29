using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioTransacciones.Aplicacion.Interfaces
{
    public interface IProductoService
    {
        Task<bool> ValidarTransaccion(int idProducto, int cantidad);
        Task<bool> AgregarStock(int idProducto, int cantidad);
        Task<bool> ReducirStock(int idProducto, int cantidad);
    }
}

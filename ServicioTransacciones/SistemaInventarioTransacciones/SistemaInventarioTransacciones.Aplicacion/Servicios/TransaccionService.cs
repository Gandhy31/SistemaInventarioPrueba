using SistemaInventarios.Dominio.Entidades;
using SistemaInventarios.Dominio.Interfaces;
using SistemaInventarioTransacciones.Aplicacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioTransacciones.Aplicacion.Servicios
{
    public class TransaccionService : ITransaccionService
    {
        private readonly IProductoService _productoService;
        private readonly ITransaccionRepository _transaccionRepository;
        public TransaccionService(IProductoService productoService, ITransaccionRepository transaccionRepository) 
        {
            _productoService = productoService;
            _transaccionRepository = transaccionRepository;
        }

        public async Task<bool> AgregarTransaccionVenta(Transaccion transaccion)
        {
            try 
            {
                int idProducto = transaccion.IdProducto;
                int cantidad = transaccion.Cantidad;
                bool hayStock = await _productoService.ValidarTransaccion(idProducto, cantidad);

                if (hayStock) 
                {
                    bool actualizacionInventario = await _productoService.ReducirStock(idProducto, cantidad);
                    if (actualizacionInventario) 
                    {
                        transaccion.FechaCreacion = DateTime.Now;
                        transaccion.FechaModificacion = DateTime.Now;
                        bool response = await _transaccionRepository.Insertar(transaccion);
                        return response;
                    }
                    else
                    {
                        return false;
                    }   
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> AgregarTransaccionCompra(Transaccion transaccion)
        {
            try
            {
                int idProducto = transaccion.IdProducto;
                int cantidad = transaccion.Cantidad;
                bool actualizacionInventario = await _productoService.AgregarStock(idProducto, cantidad);
                if (actualizacionInventario)
                {
                    transaccion.FechaCreacion = DateTime.Now;
                    transaccion.FechaModificacion = DateTime.Now;

                    bool response = await _transaccionRepository.Insertar(transaccion);
                    return response;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Actualizar(Transaccion transaccion)
        {
            try
            {
                transaccion.FechaModificacion = DateTime.Now;
                bool response = await _transaccionRepository.Actualizar(transaccion);
                return response;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task<bool> Eliminar(int idTransaccion)
        {
            try
            {
                Transaccion transaccion = await _transaccionRepository.Buscar(idTransaccion);
                bool response = await _transaccionRepository.Eliminar(transaccion);
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Transaccion> Buscar(int idTransaccion)
        {
            try
            {
                Transaccion transaccion = await _transaccionRepository.Buscar(idTransaccion);
                return transaccion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Transaccion>> ObtenerTodas()
        {
            try
            {
                List<Transaccion> transacciones = await _transaccionRepository.ObtenerTodos();
                return transacciones;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Transaccion>> BuscarPorTipo(string tipo)
        {
            try
            {
                List<Transaccion> transacciones = await _transaccionRepository.BuscarPor(x => x.TipoTransaccion == tipo);
                return transacciones;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Transaccion>> BuscarPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<Transaccion> transacciones = await _transaccionRepository.BuscarPor(x => x.Fecha >= fechaInicio && x.Fecha <= fechaFin);
                return transacciones;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

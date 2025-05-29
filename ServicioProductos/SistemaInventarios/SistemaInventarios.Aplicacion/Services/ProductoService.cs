using SistemaInventarios.Aplicacion.Interfaces;
using SistemaInventarios.Dominio.Entidades;
using SistemaInventarios.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SistemaInventarios.Aplicacion.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        //private readonly IMapper _mapper;
        public ProductoService(IProductoRepository productoRepository) 
        {
            _productoRepository = productoRepository;
        }

        public async Task<bool> AgregarProducto(Producto producto)
        {
            producto.FechaCreacion = DateTime.Now;
            producto.FechaModificacion = DateTime.Now;

            return await _productoRepository.Insertar(producto);
        }

        public async Task<Producto> Buscar(int id)
        {
            try
            {
                return await _productoRepository.Buscar(id);

            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        public async Task<List<Producto>> BuscarPor(Expression<Func<Producto, bool>> filtro)
        {
            try
            {
                return await _productoRepository.BuscarPor(filtro);
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                Producto producto = await Buscar(id);
                return await _productoRepository.Eliminar(producto);
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        public async Task<bool> Actualizar(Producto producto)
        {
            try
            {
                producto.FechaModificacion = DateTime.Now;
                return await _productoRepository.Actualizar(producto);
                
            }catch(Exception e)
            {
                return false;
            }
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                productos = await _productoRepository.ObtenerTodos();
                return productos;
            }catch(Exception e)
            {
                return null;
            }
        }

        public async Task<bool> ValidarTransaccion(int idProducto, int cantidad)
        {
            try
            {
                Producto producto = await _productoRepository.Buscar(idProducto);
                if (producto.Stock >= cantidad)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AgregarStock(int idProducto, int cantidad)
        {
            try
            {
                Producto producto = await _productoRepository.Buscar(idProducto);
                producto.FechaModificacion = DateTime.Now;
                producto.Stock += cantidad; 
                return await _productoRepository.Actualizar(producto);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ReducirStock(int idProducto, int cantidad)
        {
            try
            {
                Producto producto = await _productoRepository.Buscar(idProducto);
                producto.FechaModificacion = DateTime.Now;
                producto.Stock -= cantidad;
                return await _productoRepository.Actualizar(producto);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

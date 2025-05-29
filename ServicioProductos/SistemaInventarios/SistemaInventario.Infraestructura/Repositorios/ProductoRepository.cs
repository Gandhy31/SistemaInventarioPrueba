using SistemaInventarios.Dominio.Entidades;
using SistemaInventarios.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaInventarios.Infraestructura.Contextos;

namespace SistemaInventarios.Infraestructura.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly SistemaInventariosContext _sistemaInventariosContext;
        public ProductoRepository(SistemaInventariosContext sistemaInventariosContext)
        {
            _sistemaInventariosContext = sistemaInventariosContext;
        }
        public async Task<bool> Actualizar(Producto producto)
        {
            try
            {
                _sistemaInventariosContext.Productos.Update(producto);
                await _sistemaInventariosContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        public async Task<Producto> Buscar(int id)
        {
            
            try
            {
                return await _sistemaInventariosContext.Productos.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Producto>> BuscarPor(Expression<Func<Producto, bool>> filtro)
        {
            try
            {
                List<Producto> resultado = null;
                IQueryable<Producto> query = _sistemaInventariosContext.Set<Producto>().AsNoTracking();
                if (filtro != null)
                    query = query.Where(filtro);
                resultado = await query.ToListAsync<Producto>();
                return resultado;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public async Task<bool> Eliminar(Producto producto)
        {
            try
            {
                _sistemaInventariosContext.Productos.Remove(producto);
                await _sistemaInventariosContext.SaveChangesAsync();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Insertar(Producto producto)
        {
            try
            {
                _sistemaInventariosContext.Add(producto);
                await _sistemaInventariosContext.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                productos = await _sistemaInventariosContext.Productos.AsNoTracking().ToListAsync<Producto>();
                return productos;
            }catch (Exception e)
            {
                return null;
            }
        }
    }
}

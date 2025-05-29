using Microsoft.EntityFrameworkCore;
using SistemaInventarios.Dominio.Entidades;
using SistemaInventarios.Dominio.Interfaces;
using SistemaInventarios.Infraestructura.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarioTransacciones.Infraestructura.Repositorios
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly SistemaInventariosContext _sistemaInventariosContext;
        public TransaccionRepository(SistemaInventariosContext sistemaInventariosContext)
        {
            _sistemaInventariosContext = sistemaInventariosContext;
        }
        public async Task<bool> Actualizar(Transaccion transaccion)
        {
            try
            {
                _sistemaInventariosContext.Transacciones.Update(transaccion);
                await _sistemaInventariosContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Transaccion> Buscar(int id)
        {
            try
            {
                return await _sistemaInventariosContext.Transacciones.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Transaccion>> BuscarPor(Expression<Func<Transaccion, bool>> filtro)
        {
            try
            {
                List<Transaccion> resultado = null;
                IQueryable<Transaccion> query = _sistemaInventariosContext.Set<Transaccion>().AsNoTracking();
                if (filtro != null)
                    query = query.Where(filtro);
                resultado = await query.ToListAsync<Transaccion>();
                return resultado;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Eliminar(Transaccion transaccion)
        {
            try
            {
                _sistemaInventariosContext.Transacciones.Remove(transaccion);
                await _sistemaInventariosContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Insertar(Transaccion transaccion)
        {
            try
            {
                _sistemaInventariosContext.Transacciones.Add(transaccion);
                await _sistemaInventariosContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Transaccion>> ObtenerTodos()
        {
            List<Transaccion> productos = new List<Transaccion>();
            try
            {
                productos = await _sistemaInventariosContext.Transacciones.AsNoTracking().ToListAsync<Transaccion>();
                return productos;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

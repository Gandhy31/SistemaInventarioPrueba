using Microsoft.AspNetCore.Mvc;
using SistemaInventarios.Aplicacion.Interfaces;
using SistemaInventarios.Dominio.Entidades;
using AutoMapper;
using SistemaInventarios.Aplicacion.DTOs;

namespace SistemaInventarios.ProductosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;
        public ProductosController(IProductoService productoService, IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        [HttpGet("ObtenerProducto/{id}")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            try
            {
                var response = await _productoService.Buscar(id);
                if (response == null)
                {
                    return NotFound(new { message = "Recurso no encontrado" });
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "No autorizado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }

        }

        [HttpGet("ObtenerProductos")]
        public async Task<IActionResult> ObtenerProductos()
        {
            try
            {
                var response = await _productoService.ObtenerTodos();
                if (response == null)
                {
                    return NotFound(new { message = "Recurso no encontrado" });
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "No autorizado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
            
        }

        [HttpPost("AgregarProducto")]
        public async Task<IActionResult> AgregarProducto(EntidadRegistroProducto productoDTO)
        {
            Producto producto = _mapper.Map<Producto>(productoDTO);
            var status = await _productoService.AgregarProducto(producto);
            return Ok(status);
        }

        [HttpPut("ActualizarProducto")]
        public async Task<IActionResult> ActualizarProducto(EntidadRegistroProducto productoDTO)
        {
            Producto producto = _mapper.Map<Producto>(productoDTO);
            var status = await _productoService.Actualizar(producto);
            return Ok(status);
        }

        [HttpDelete("EliminarProducto/{idProducto}")]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            var status = await _productoService.Eliminar(idProducto);
            return Ok(status);
        }

        [HttpGet("ValidarTransaccion")]
        public async Task<IActionResult> ValidarTransaccion(int idProducto, int cantidad)
        {
            var status = await _productoService.ValidarTransaccion(idProducto, cantidad);
            return Ok(status);
        }


        [HttpPut("AgregarStock")]
        public async Task<IActionResult> AgregarStock(EntidadRegistroStock registroStock)
        {
            
            var response = await _productoService.AgregarStock(registroStock.IdProducto, registroStock.Cantidad);
            return Ok(response);
        }

        [HttpPut("ReducirStock")]
        public async Task<IActionResult> ReducirStock(EntidadRegistroStock registroStock)
        {
            var response = await _productoService.ReducirStock(registroStock.IdProducto, registroStock.Cantidad);
            return Ok(response);
        }
    }
}

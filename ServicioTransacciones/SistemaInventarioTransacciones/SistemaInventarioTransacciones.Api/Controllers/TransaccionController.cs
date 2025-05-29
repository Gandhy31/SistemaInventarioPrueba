using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaInventarios.Dominio.Entidades;
using SistemaInventarioTransacciones.Aplicacion.DTOs;
using SistemaInventarioTransacciones.Aplicacion.Interfaces;

namespace SistemaInventarioTransacciones.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;
        private readonly IMapper _mapper;
        public TransaccionController(ITransaccionService transaccionService, IMapper maper) 
        {
            _transaccionService = transaccionService;
            _mapper = maper;
        }

        [HttpPost("AgregarCompra")]
        public async Task<IActionResult> AgregarCompra(EntidadRegistroTransaccion transaccionDTO)
        {
            Transaccion transaccion = _mapper.Map<Transaccion>(transaccionDTO);
            var resultado = await _transaccionService.AgregarTransaccionCompra(transaccion);
            return Ok(resultado);
        }

        [HttpPost("AgregarVenta")]
        public async Task<IActionResult> AgregarVenta(EntidadRegistroTransaccion transaccionDTO)
        {
            Transaccion transaccion = _mapper.Map<Transaccion>(transaccionDTO);
            var resultado = await _transaccionService.AgregarTransaccionVenta(transaccion);
            return Ok(resultado);
        }

        [HttpGet("BuscarTransaccion/{idTransaccion}")]
        public async Task<IActionResult> BuscarTransaccion(int idTransaccion)
        {
            var resultado = await _transaccionService.Buscar(idTransaccion);
            return Ok(resultado);
        }

        [HttpGet("ObtenerTransacciones")]
        public async Task<IActionResult> ObtenerTransaccioin()
        {
            var resultado = await _transaccionService.ObtenerTodas();
            return Ok(resultado);
        }

        [HttpDelete("EliminarTransaccion/{idTransaccion}")]
        public async Task<IActionResult> EliminarTransaccion(int idTransaccion)
        {
            var resultado = await _transaccionService.Eliminar(idTransaccion);
            return Ok(resultado);
        }

        [HttpPut("ActualizarTransaccion")]
        public async Task<IActionResult> ActualizarTransaccion(EntidadRegistroTransaccion transaccionDTO)
        {
            Transaccion transaccion = _mapper.Map<Transaccion>(transaccionDTO);
            var resultado = await _transaccionService.Actualizar(transaccion);
            return Ok(resultado);
        }
    }
}

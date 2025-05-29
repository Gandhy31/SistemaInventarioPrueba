using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SistemaInventarioTransacciones.Aplicacion.Interfaces;

namespace SistemaInventarioTransacciones.Infraestructura.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;
        
        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AgregarStock(int idProducto, int cantidad)
        {
            try
            {
                var stock = new
                {
                    IdProducto = idProducto,
                    Cantidad = cantidad
                };

                var body = new StringContent(
                    JsonSerializer.Serialize(stock),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"/api/Productos/AgregarStock", body);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var content = await response.Content.ReadAsStringAsync();
                return bool.TryParse(content, out var resultado) && resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ReducirStock(int idProducto, int cantidad)
        {
            try
            {
                var stock = new
                {
                    IdProducto = idProducto,
                    Cantidad = cantidad
                };

                var body = new StringContent(
                    JsonSerializer.Serialize(stock),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"/api/Productos/ReducirStock", body);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var content = await response.Content.ReadAsStringAsync();
                return bool.TryParse(content, out var resultado) && resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ValidarTransaccion(int idProducto, int cantidad)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Productos/ValidarTransaccion?idProducto={idProducto}&cantidad={cantidad}");
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var content = await response.Content.ReadAsStringAsync();
                return bool.TryParse(content, out var resultado) && resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

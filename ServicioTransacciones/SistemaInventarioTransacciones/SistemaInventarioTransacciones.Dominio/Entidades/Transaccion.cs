using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventarios.Dominio.Entidades
{
    public class Transaccion
    {
        public int IdTransaccion {  get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTransaccion { get; set; }
        public int Cantidad {  get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public string Detalle {  get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion {  get; set; }
    }
}
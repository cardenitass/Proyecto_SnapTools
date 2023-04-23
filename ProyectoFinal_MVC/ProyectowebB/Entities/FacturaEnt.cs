using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectowebB.Entities
{
    public class FacturaEnt
    {
        public int IdUsuario { get; set; }

        public int IdFactura { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }
}
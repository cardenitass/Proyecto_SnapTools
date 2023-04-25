using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Entities
{
    public class DetalleFacturaEnt
    {
        public int IdFactura { get; set; }

        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        
    }
}
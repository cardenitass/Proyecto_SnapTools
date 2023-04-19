using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Entities
{
    public class CarritoDetalleEnt
    {
        public string   NombreProducto    { get; set; }
        public int      CantidadCarrito   { get; set; }
        public decimal  PrecioProducto    { get; set; }
        public decimal?  SubTotal          { get; set; }
        public decimal?  Impuesto          { get; set; }
        public decimal?  Total             { get; set; }
    }
}
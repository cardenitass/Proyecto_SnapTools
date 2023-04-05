using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectowebB.Entities
{
    public class ProductoEnt
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public string ImagenURL { get; set; }
        public int IdTienda { get; set; }


       //Esto es del carrito, tambien el id Producto
        public int IdUsuario { get; set; }

        public int CantidadProducto { get; set; }
    }
}
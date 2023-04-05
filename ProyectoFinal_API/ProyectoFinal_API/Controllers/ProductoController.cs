
using ProyectoFinal_API.Entities;
using ProyectoFinal_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoFinal_API.Controllers
{
    [Authorize]
    public class ProductoController : ApiController
    {

        ProductoModel ProductoModel  = new ProductoModel();


        [HttpPost]
        [Route("api/AgregarProducto")]
        public int AgregarProducto(ProductoEnt entidad)
        {
            return ProductoModel.AgregarProducto(entidad);
        }


        [HttpGet]
        [Route("api/ConsultarProductos")]
        public List<ProductoEnt> ConsultarProductos()
        {
            return ProductoModel.ConsultarProductos();
        }

        [HttpGet]
        [Route("api/ConsultarProducto")]
        public ProductoEnt ConsultarProducto(int q)
        {
            return ProductoModel.ConsultarProducto(q);
        }

        [HttpPut]
        [Route("api/ActualizarProducto")]
        public void ActualizarProducto(ProductoEnt entidad)
        {
            ProductoModel.ActualizarProducto(entidad);
        }


        [HttpDelete]
        [Route("api/EliminarProducto")]
        public void EliminarProducto(int id)
        {
            ProductoModel.EliminarProducto(id);
        }

        [HttpPut]
        [Route("api/ActualizarCarrito")]
        public void ActualizarCarrito(ProductoEnt entidad)
        {
            ProductoModel.ActualizarCarrito(entidad);
        }

    }
}

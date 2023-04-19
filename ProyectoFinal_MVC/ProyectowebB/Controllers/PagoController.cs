using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Controllers
{  
    public class PagoController : Controller
    {
        ProductoModel ProductoModel = new ProductoModel();

        [HttpGet]
        public ActionResult DesgloceCarrito()
        {
            var datosCarritoTotal = ProductoModel.MostrarCarritoTotal();
            return View(datosCarritoTotal);
        }
    }
}
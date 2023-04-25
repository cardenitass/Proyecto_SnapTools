using ProyectowebB.App_Start;
using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Controllers
{
    [SessionFilter]
    [OutputCache(NoStore = true, Duration = 0)]
    public class FacturaController : Controller
    {

        FacturaModel FacturaModel = new FacturaModel(); 
        ProductoModel ProductoModel= new ProductoModel();

        [HttpPost]
        public ActionResult ConfirmarPago()
        {
            FacturaModel.ConfirmarPago();

            var datosCarritoTemporal = ProductoModel.MostrarCarritoTemporal();
            Session["CantidadCarritoTemporal"] = datosCarritoTemporal.CantidadCarrito;
            Session["PrecioCarritoTemporal"] = datosCarritoTemporal.PrecioCarrito;

            if (int.Parse(Session["CantidadCarritoTemporal"].ToString()) > 0)
            {
                return RedirectToAction("DesgloceCarrito", "Producto");
            }
            else
            {
                return RedirectToAction("MostrarFactura", "Factura");
            }
        }

        [HttpGet]
        public ActionResult MostrarFactura() { 

            var datos = FacturaModel.MostrarFactura();
            return View(datos);
        }


        [HttpGet]
        public ActionResult MostrarDetalleFactura(int IdFactura)
        {
            var datos = FacturaModel.MostrarDetalleFactura(IdFactura);
            ViewBag.IdFactura = IdFactura; 
            return View(datos);
        }

    }
}
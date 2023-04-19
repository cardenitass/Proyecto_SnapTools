using ProyectowebB.App_Start;
using ProyectowebB.Entities;
using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Controllers
{
    [SessionFilter]
    [OutputCache(NoStore = true, Duration = 0)]
    public class ProductoController : Controller
    {
        ProductoModel ProductoModel = new ProductoModel();
        LogsModel LogsModel = new LogsModel();
        StoreModel StoreModel = new StoreModel();   

        // Ver lista de Productos
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var resultado = ProductoModel.ConsultarProductos();
                return View(resultado);

            }catch(Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        // Redirige a la pantalla de agregar y carga lista de tiendas
        [HttpGet]
        public ActionResult AgregarProducto()
        {
            try
            {
                ViewBag.ListaTiendas = StoreModel.ConsultarTiendas(); 
                return View();
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        // Al darle al boton agregar llama al metodo del model
        [HttpPost]
        public ActionResult AgregarProducto(ProductoEnt entidad)
        {
            try
            {
                var respuesta = ProductoModel.AgregarProducto(entidad);
                if (respuesta > 0)
                    return RedirectToAction("Index", "Producto");
                else
                {
                    ViewBag.mensajeError = "El producto no se pudo agregar";
                    return RedirectToAction("Index", "Producto");
                }
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        // Cargar vista de Actualizar Producto
        [HttpGet]
        public ActionResult ActualizarProducto(int q)
        {
            try
            {
                var resultado = ProductoModel.ConsultarProducto(q);

                ViewBag.ListaTiendas = StoreModel.ConsultarTiendas();
                return View(resultado);
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        // Llamar al metodo que hace el insert en el API
        [HttpPost]
        public ActionResult ActualizarProducto(ProductoEnt entidad)
        {
            try
            {
                ProductoModel.ActualizarProducto(entidad);
                return RedirectToAction("Index", "Producto");
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        // LLamar al metodo que hace el delete en el API
        [HttpPost]
        public ActionResult EliminarProducto(int id)
        {
            try
            {
                ProductoModel.EliminarProducto(id);
                return RedirectToAction("Index", "Producto");

            }catch(Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }


        [HttpPost]
        public ActionResult ActualizarCarrito(int IdProducto, int CantidadProducto)
        {
            ProductoModel.ActualizarCarrito(IdProducto, CantidadProducto);
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}
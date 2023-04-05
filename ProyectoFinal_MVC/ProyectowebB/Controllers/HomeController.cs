using ProyectowebB.App_Start;
using ProyectowebB.Entities;
using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProyectowebB.Controllers
{
    public class HomeController : Controller
    {
        LogsModel LogsModel = new LogsModel();
        UsuarioModel UsuarioModel = new UsuarioModel();
        ProvinciaModel ProvinciaModel = new ProvinciaModel();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                Session.Clear();
                return View();
            }
            catch(Exception ex) 
            {
                LogsModel.RegistrarBitacora(ControllerContext, ex.Message);
                return View("Index");
            }
            
        }

        // Se crean las variables de sesion y se redirige
        [HttpPost]
        public ActionResult Principal(UsuarioEnt entidad) {
            try
            {
                var resultado = UsuarioModel.ValidarUsuario(entidad); 
                if (resultado != null)
                {
                    Session["IdUsuario"] = resultado.IdUsuario;
                    Session["CorreoUsuario"] = resultado.CorreoElectronico;
                    Session["TokenUsuario"] = resultado.Token;
                    return RedirectToAction("PantallaPrincipal","Home");  
                }
                else{
                    ViewBag.mensajeError = "Sus credenciales son incorrectas";
                    return View("Index");
                }

            }catch(Exception ex)
            {
                LogsModel.RegistrarBitacora(ControllerContext, ex.Message);
                ViewBag.mensajeError = "Sus credenciales no fueron validadas";
                return View("Index");
            }
        
        }


        // Redirigir a la vista RegistrarUsuario 

        [HttpGet]
        public ActionResult RegistrarUsuario()
        {
            try
            {
                ViewBag.ListaProvincias = ProvinciaModel.ConsultarProvincias();
                return View();
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarBitacora(ControllerContext, ex.Message);
                return View("Index");
            }
        }

        // Conceder o denegar permiso si el correo existe

        [HttpPost]
        public ActionResult BuscarCorreo(string correo)
        {
                var resultado = UsuarioModel.BuscarCorreo(correo);
                return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        // Registrar el usuario nuevo y mandarlo al login 

        [HttpPost]
        public ActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            try
            {
                var respuesta = UsuarioModel.RegistrarUsuario(entidad);
                if (respuesta > 0)
                    return View("Index");
                else
                {
                    ViewBag.mensajeError = "El usuario no se pudo registrar"; 
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarBitacora(ControllerContext, ex.Message);
                return View("Index");
            }
        }

        [HttpGet]
        [SessionFilter]
        public ActionResult PantallaPrincipal()
        {
            try
            {
                return View("Principal");

            }
            catch (Exception ex)
            {
                LogsModel.RegistrarBitacora(ControllerContext, ex.Message);
                ViewBag.mensajeError = "Sus credenciales no fueron validadas";
                return View("Index");
            }
        }

        [HttpGet]
        [SessionFilter]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }

}
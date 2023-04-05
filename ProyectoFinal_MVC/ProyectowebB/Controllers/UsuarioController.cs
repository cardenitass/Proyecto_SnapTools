using ProyectowebB.App_Start;
using ProyectowebB.Entities;
using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Controllers
{
    [SessionFilter]
    public class UsuarioController : Controller
    {
        LogsModel LogsModel = new LogsModel();
        UsuarioModel UsuarioModel = new UsuarioModel();
        ProvinciaModel ProvinciaModel = new ProvinciaModel();
        RoleModel RoleModel = new RoleModel();

        // Ver lista de Usuarios
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var resultado = UsuarioModel.ConsultarUsuarios();
                return View(resultado);

            }
            catch(Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult ActualizarUsuario(int q)
        {
            try
            {
                var resultado = UsuarioModel.ConsultarUsuario(q);

                ViewBag.ListaProvincias = ProvinciaModel.ConsultarProvincias();
                ViewBag.ListaRoles = RoleModel.ConsultarRoles();
                return View(resultado);
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(UsuarioEnt entidad)
        {
            try
            {
                UsuarioModel.ActualizarUsuario(entidad);
                return RedirectToAction("Index","Usuario");
            }
            catch (Exception ex)
            {
                LogsModel.RegistrarErrores(Session["IdUsuario"], ControllerContext, ex.Message);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CambiarEstado(int id)
        {
                UsuarioModel.CambiarEstado(id);
                return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}
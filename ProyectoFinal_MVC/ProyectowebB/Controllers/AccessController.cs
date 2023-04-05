using ProyectowebB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Controllers
{
    public class AccessController : Controller
    {
       
        RecoveryModel RecoveryModel = new RecoveryModel();

        [HttpGet]
        public ActionResult StartRecovery()
        {
            Models.RecoveryModel model = new Models.RecoveryModel();
            return View(model);
        }

        // Enviar el token generado a BD

        [HttpPost]
        public ActionResult StartRecovery(Models.RecoveryModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string token = model.GetSha256(Guid.NewGuid().ToString());

                model.UpdateUserToken(model.Email, token);

                //Enviar el Correo 
                model.SendEmail(model.Email, token);

                return View();

            }
            catch (Exception ex)
            {
                LogsModel LogsModel = new LogsModel();
                LogsModel.RegistrarBitacora(ControllerContext, ex.Message);
                return View("/Views/Home/Index.cshtml");

            }
        }


        //Este Action va a recibir el token

        [HttpGet]
        public ActionResult Recovery(string token)
        {
            Models.RecoveryPassword model = new Models.RecoveryPassword();
            model.token = token;

            var vUsuario = model.GetUserByRecoveryToken(token);

            if (vUsuario == null)
            {
                ViewBag.Error = "Tu token ha expirado";
                return View("/Views/Home/Index.cshtml");
            }

            return View(model);
        }



        // Hacer el cambio de password en la BD

        [HttpPost]
        public ActionResult Recovery(Models.RecoveryPassword model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.UpdateUserPassword(model.token, model.Password);
            }
            catch (Exception ex)
            {
                LogsModel LogsModel = new LogsModel();
                LogsModel.RegistrarBitacora(ControllerContext, ex.Message);
                return View("/Views/Home/Index.cshtml");
            }

            ViewBag.Message = "Contraseña recuperada exitosamente";
            return View("/Views/Home/Index.cshtml");
        }

     


        [HttpPost]
        public ActionResult BuscarCorreo(string correo)
        {
                var resultado = RecoveryModel.BuscarCorreoRecovery(correo);
                return Json(resultado, JsonRequestBehavior.AllowGet);        
        }

    }

}
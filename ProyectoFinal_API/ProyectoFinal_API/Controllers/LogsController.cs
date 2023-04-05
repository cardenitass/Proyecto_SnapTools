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
    public class LogsController : ApiController
    {
        LogsModel LogsModel = new LogsModel();


        [HttpPost]
        [AllowAnonymous]
        [Route("api/RegistrarBitacora")]
        public void RegistrarBitacora(LogsEnt entidad)
        {
           LogsModel.RegistrarBitacora(entidad);
        }

        [HttpPost]
        [Authorize]
        [Route("api/RegistrarErrores")]
        public void RegistrarErrores(LogsEnt entidad)
        {
           LogsModel.RegistrarErrores(entidad);
        }
    }
}

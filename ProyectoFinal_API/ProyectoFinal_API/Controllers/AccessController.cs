using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using ProyectoFinal_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace ProyectoFinal_API.Controllers
{
    public class AccessController : ApiController
    {

        RecoveryModel RecoveryModel = new RecoveryModel();
        RecoveryPassword RecoveryPassword = new RecoveryPassword();

        [HttpGet]
        [AllowAnonymous]
        [Route("api/BuscarCorreoRecovery")]
        public string BuscarCorreoRecovery(string correoElectronico)
        {
            return RecoveryModel.BuscarCorreoRecovery(correoElectronico);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("api/UpdateUserToken")]
        public void UpdateUserToken(RecoveryEnt recovery)
        {
            RecoveryModel.UpdateUserToken(recovery);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/GetUserByRecoveryToken")]
        public RecoveryEnt GetUserByRecoveryToken(string token)
        {
            return RecoveryPassword.GetUserByRecoveryToken(token);
        }


        [HttpPut]
        [AllowAnonymous]
        [Route("api/UpdateUserPassword")]
        public void UpdateUserPassword(RecoveryEnt recovery)
        {
            RecoveryPassword.UpdateUserPassword(recovery);
        }



    }
}


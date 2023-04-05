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
    public class RoleController : ApiController
    {
        RoleModel RoleModel = new RoleModel();

        [HttpGet]
        [Authorize]
        [Route("api/ConsultarRoles")]
        public List<RoleEnt> ConsultarRoles()
        {
            return RoleModel.ConsultarRoles();
        }
    }
}

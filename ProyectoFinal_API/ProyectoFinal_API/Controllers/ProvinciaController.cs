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
    public class ProvinciaController : ApiController
    {
        ProvinciaModel ProvinciaModel = new ProvinciaModel();

        [HttpGet]
        [AllowAnonymous]
        [Route("api/ConsultarProvincias")]
        public List<ProvinciaEnt> ConsultarProvincias()
        {
            return ProvinciaModel.ConsultarProvincias();
        }
    }
}

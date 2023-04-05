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
    public class StoreController : ApiController
    {
        StoreModel StoreModel = new StoreModel(); 

        [HttpGet]
        [Authorize]
        [Route("api/ConsultarTiendas")]
        public List<StoreEnt> ConsultarTiendas()
        {
            return StoreModel.ConsultarTiendas();
        }
    }
}

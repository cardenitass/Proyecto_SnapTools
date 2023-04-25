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
    [Authorize]
    public class FacturaController : ApiController
    {
        FacturaModel FacturaModel = new FacturaModel();

        [HttpPost]
        [Route("api/ConfirmarPago")]
        public void ConfirmarPago(FacturaEnt entidad)
        {
            FacturaModel.ConfirmarPago(entidad);
        }


        [HttpGet]
        [Route("api/MostrarFactura")]
        public List<FacturaEnt> MostrarFactura(int IdUsuario)
        {
            return FacturaModel.MostrarFactura(IdUsuario);
        }


        [HttpGet]
        [Route("api/MostrarDetalleFactura")]
        public List<DetalleFacturaEnt> MostrarDetalleFactura(int IdFactura)
        {
            return FacturaModel.MostrarDetalleFactura(IdFactura);
        }
    }
}

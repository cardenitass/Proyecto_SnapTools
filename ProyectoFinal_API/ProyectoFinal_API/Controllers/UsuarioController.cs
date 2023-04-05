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
    public class UsuarioController : ApiController
    {
        UsuarioModel UsuarioModel = new UsuarioModel();

        [HttpPost]
        [AllowAnonymous]
        [Route("api/ValidarUsuario")]
        public UsuarioEnt ValidarUsuario(UsuarioEnt entidad)
        {
            return UsuarioModel.ValidarUsuario(entidad); 
        }

        [HttpGet]
        [Authorize]
        [Route("api/ConsultarUsuarios")]
        public List<UsuarioEnt> ConsultarUsuarios()
        {
            return UsuarioModel.ConsultarUsuarios();
        }

        [HttpGet]
        [Authorize]
        [Route("api/ConsultarUsuario")]
        public UsuarioEnt ConsultarUsuario(int q)
        {
            return UsuarioModel.ConsultarUsuario(q);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("api/BuscarCorreo")]
        public string BuscarCorreo(string correoElectronico)
        {
            return UsuarioModel.BuscarCorreo(correoElectronico);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/RegistrarUsuario")]
        public int RegistrarUsuario(UsuarioEnt entidad)
        {
            return UsuarioModel.RegistrarUsuario(entidad);
        }

        [HttpPut]
        [Authorize]
        [Route("api/ActualizarUsuario")]
        public void ActualizarUsuario(UsuarioEnt entidad)
        {
            UsuarioModel.ActualizarUsuario(entidad);
        }

        [HttpDelete]
        [Authorize]
        [Route("api/CambiarEstado")]
        public void CambiarEstado(int q)
        {
            UsuarioModel.CambiarEstado(q);
        }


    }
}

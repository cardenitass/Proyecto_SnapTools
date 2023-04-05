using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Entities
{
    public class UsuarioEnt
    {
        //Todos los atributos

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
        public bool Estado { get; set; }
        public int Rol { get; set; }
        public string Identificacion { get; set; }
        public int IdProvincia { get; set; }
        public string Token { get; set; }
    }
}
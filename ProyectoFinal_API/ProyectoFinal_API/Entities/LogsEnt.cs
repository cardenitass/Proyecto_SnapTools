using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Entities
{
    public class LogsEnt
    {
        public int IdUsuario { get; set; }

        public string Origen { get; set; }

        public DateTime FechaHora { get; set; }

        public string Descripcion { get; set; }
    }
}
using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_API.Models
{
    public class LogsModel
    {
        // Registrar errores de usuarios NO logueados en el sistema
        public void RegistrarBitacora(LogsEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                Binnacle bitacora = new Binnacle();
                bitacora.date_time = entidad.FechaHora;
                bitacora.origin = entidad.Origen;
                bitacora.description = entidad.Descripcion;

                conexion.Binnacle.Add(bitacora);
                conexion.SaveChanges();
            }
        }

        // Registrar errores de usuarios logueados en el sistema
        public void RegistrarErrores(LogsEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                Errors error = new Errors();
                error.id_user = entidad.IdUsuario; 
                error.date_time = entidad.FechaHora;
                error.origin = entidad.Origen; 
                error.description = entidad.Descripcion;

                conexion.Errors.Add(error);
                conexion.SaveChanges();
            }
        }
    }
}
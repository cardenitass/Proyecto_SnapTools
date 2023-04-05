using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class RecoveryModel
    {

        public string BuscarCorreoRecovery(string correoElectronico)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var resultado = (from x in conexion.User_tb
                                 where x.email == correoElectronico
                                 select x).FirstOrDefault();

                if (resultado == null)
                    return string.Empty;
                else
                {
                    if (resultado.active)
                        return "";
                    else
                        return "Esta cuenta de correo se encuentra inactiva";
                }
            }
        }

        public void UpdateUserToken(RecoveryEnt recovery)
        {
            using (ModeloBD.ProyectoFinal_KN_BDEntities db = new ModeloBD.ProyectoFinal_KN_BDEntities())
            {
                var vUsuario = db.User_tb.Where(d => d.email == recovery.Email).FirstOrDefault();
                if (vUsuario != null)
                {
                    vUsuario.token_recovery = recovery.TokenRecovery;
                    db.Entry(vUsuario).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

    }
}
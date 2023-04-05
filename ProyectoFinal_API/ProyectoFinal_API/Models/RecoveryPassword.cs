using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class RecoveryPassword
    {


        public RecoveryEnt GetUserByRecoveryToken(string token)
        {
            using (ModeloBD.ProyectoFinal_KN_BDEntities db = new ModeloBD.ProyectoFinal_KN_BDEntities())
            {
                return db.User_tb
                         .Where(d => d.token_recovery == token)
                         .Select(u => new RecoveryEnt
                         {
                             Id = u.id_user,
                             Email = u.email,
                             Password = u.password,
                             TokenRecovery = u.token_recovery
                         })
                         .FirstOrDefault();
            }
        }


        public void UpdateUserPassword(RecoveryEnt recovery)
        {
            using (ModeloBD.ProyectoFinal_KN_BDEntities db = new ModeloBD.ProyectoFinal_KN_BDEntities())
            {
                var vUsuario = db.User_tb.Where(d => d.token_recovery == recovery.TokenRecovery).FirstOrDefault();

                if (vUsuario != null)
                {
                    vUsuario.password = recovery.Password;
                    vUsuario.token_recovery = null;
                    db.Entry(vUsuario).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
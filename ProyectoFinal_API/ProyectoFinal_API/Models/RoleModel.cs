using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class RoleModel
    {
        // Consulta los roles existentes de la bd
        public List<RoleEnt> ConsultarRoles()
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.Role
                             select x).ToList();

                List<RoleEnt> listaEntidadResultado = new List<RoleEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new RoleEnt
                    {
                        IdRol = item.id_role,
                        NombreRol = item.role_name
                    });
                }

                return listaEntidadResultado;
            }
        }
    }
}
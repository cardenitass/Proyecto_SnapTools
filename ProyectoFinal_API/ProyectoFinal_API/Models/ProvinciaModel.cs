using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class ProvinciaModel
    {
        // Consulta las provincias existentes de la bd
        public List<ProvinciaEnt> ConsultarProvincias()
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.Province
                             select x).ToList();

                List<ProvinciaEnt> listaEntidadResultado = new List<ProvinciaEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new ProvinciaEnt
                    {
                        IdProvincia = item.id_province,
                        NombreProvincia = item.province_name
                    });
                }

                return listaEntidadResultado;
            }
        }
    }
}
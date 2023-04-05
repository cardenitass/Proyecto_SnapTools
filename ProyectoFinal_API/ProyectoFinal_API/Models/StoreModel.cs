using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class StoreModel
    {
        // Consulta las tiendas existentes de la bd
        public List<StoreEnt> ConsultarTiendas()
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.Store
                             select x).ToList();

                List<StoreEnt> listaEntidadResultado = new List<StoreEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new StoreEnt
                    {
                        IdStore = item.id_store,
                        Nombre = item.name
                    });
                }

                return listaEntidadResultado;
            }
        }
    }
}
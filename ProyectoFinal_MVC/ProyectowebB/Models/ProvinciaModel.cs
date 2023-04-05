using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Net.Http.Json;
using System.Web.Mvc;

namespace ProyectowebB.Models
{
    public class ProvinciaModel
    {
        // Consulta las provincias existentes de la bd para el actualizar y registro
        public List<SelectListItem> ConsultarProvincias()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ConsultarProvincias";

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    var listaProvincias = res.Content.ReadFromJsonAsync<List<ProvinciaEnt>>().Result;

                    List<SelectListItem> listaComboBox = new List<SelectListItem>();
                    foreach (var item in listaProvincias)
                    {
                        listaComboBox.Add(new SelectListItem
                        {
                            Text = item.NombreProvincia,
                            Value = item.IdProvincia.ToString()
                        });
                    }

                    return listaComboBox;
                }
                // Una lista no puede retornar null
                return new List<SelectListItem>();
            }
        }

    }     
}
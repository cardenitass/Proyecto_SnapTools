using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Models
{
    public class StoreModel
    {
        // Consulta las tiendas existentes de la bd para el actualizar y agregar producto
        public List<SelectListItem> ConsultarTiendas()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ConsultarTiendas";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    var listaTiendas = res.Content.ReadFromJsonAsync<List<StoreEnt>>().Result;

                    List<SelectListItem> listaComboBox = new List<SelectListItem>();
                    foreach (var item in listaTiendas)
                    {
                        listaComboBox.Add(new SelectListItem
                        {
                            Text = item.Nombre,
                            Value = item.IdStore.ToString()
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
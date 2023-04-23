using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;

namespace ProyectowebB.Models
{
    public class FacturaModel
    {
        public void ConfirmarPago()
        {
            using (var client = new HttpClient())
            {
                FacturaEnt entidad = new FacturaEnt();
                entidad.IdUsuario = int.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                // Serializar
                JsonContent body = JsonContent.Create(entidad);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                string url = "https://localhost:44345/api/ConfirmarPago";

                client.PostAsync(url, body).GetAwaiter().GetResult();

            }
        }

        public List<FacturaEnt> MostrarFactura()
        {
            using (var client = new HttpClient())
            {
                int IdUsuario = int.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = "https://localhost:44345/api/MostrarFactura?IdUsuario=" + IdUsuario;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<List<FacturaEnt>>().Result;

                // Una lista no puede retornar null
                return new List<FacturaEnt>();
            }
        }
    }
}
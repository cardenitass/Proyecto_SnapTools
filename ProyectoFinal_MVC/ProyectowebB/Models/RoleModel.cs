using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Json;

namespace ProyectowebB.Models
{
    public class RoleModel
    {
        // Consulta los roles existentes de la bd
        public List<SelectListItem> ConsultarRoles()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ConsultarRoles";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    var listaRoles = res.Content.ReadFromJsonAsync<List<RoleEnt>>().Result;

                    List<SelectListItem> listaComboBox = new List<SelectListItem>();
                    foreach (var item in listaRoles)
                    {
                        listaComboBox.Add(new SelectListItem
                        {
                            Text = item.NombreRol,
                            Value = item.IdRol.ToString()
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
using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;

namespace ProyectowebB.Models
{
    public class LogsModel
    {
        // Registrar errores de usuarios NO logueados en el sistema
        public void RegistrarBitacora(ControllerContext origen, string descripcion)
        {
            LogsEnt entidad = new LogsEnt();
            entidad.FechaHora = DateTime.Now;
            entidad.Origen = origen.RouteData.Values["controller"].ToString() + "-" + origen.RouteData.Values["action"].ToString();
            entidad.Descripcion = descripcion;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(entidad);
                string url = "https://localhost:44345/api/RegistrarBitacora";

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();
            }
        }

        // Registrar errores de usuarios logueados en el sistema
        public void RegistrarErrores(object usuario, ControllerContext origen, string descripcion)
        {

            LogsEnt entidad = new LogsEnt();
            entidad.IdUsuario = int.Parse(usuario.ToString());
            entidad.FechaHora = DateTime.Now;
            entidad.Origen = origen.RouteData.Values["controller"].ToString() + "-" + origen.RouteData.Values["action"].ToString();
            entidad.Descripcion = descripcion;

            using (var client = new HttpClient())
            {
                JsonContent body = JsonContent.Create(entidad);
                string url = "https://localhost:44345/api/RegistrarErrores";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();
            }

        }
    }
}
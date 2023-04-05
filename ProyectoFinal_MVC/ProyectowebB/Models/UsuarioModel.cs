using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;

namespace ProyectowebB.Models
{
    public class UsuarioModel
    {
      
        // Metodo para validar que el usuario exista
        public UsuarioEnt ValidarUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ValidarUsuario";

                // Serializar
                JsonContent body = JsonContent.Create(entidad);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<UsuarioEnt>().Result;

                return null; 
            }
        }

        // Metodo de query para agarrar los datos del usuario 
        public List<UsuarioEnt> ConsultarUsuarios()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ConsultarUsuarios";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<List<UsuarioEnt>>().Result;
                
                // Una lista no puede retornar null
                return new List<UsuarioEnt>();
            }
        }

        // Consultar usuario en especifico para el Actualizar
        public UsuarioEnt ConsultarUsuario(int q)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ConsultarUsuario?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<UsuarioEnt>().Result;

                return null; 
            }
        }

        // Metodo para verificar si el correo existe en la bd
        public string BuscarCorreo(string correoElectronico)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/BuscarCorreo?correoElectronico=" + correoElectronico;

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<string>().Result;

                return "ERROR";
            }
        }

        // Metodo para realizar el insert del nuevo usuario en la bd

        public int RegistrarUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/RegistrarUsuario";

                // Serializar
                JsonContent body = JsonContent.Create(entidad);

                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<int>().Result;

                return 0;
            }
        }

        public void ActualizarUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                
                // Serializar
                JsonContent body = JsonContent.Create(entidad);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                string url = "https://localhost:44345/api/ActualizarUsuario";

                client.PutAsync(url, body).GetAwaiter().GetResult();

            }
        }

        public void CambiarEstado(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                string url = "https://localhost:44345/api/CambiarEstado?q=" + id;

                client.DeleteAsync(url).GetAwaiter().GetResult();

            }
        }

    }

}

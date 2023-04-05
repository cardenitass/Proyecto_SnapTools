using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Net.Http.Json;

namespace ProyectowebB.Models
{
    public class ProductoModel
    {
        // Metodo de query para agarrar los datos de los productos 
        public List<ProductoEnt> ConsultarProductos()
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ConsultarProductos";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<List<ProductoEnt>>().Result;

                // Una lista no puede retornar null
                return new List<ProductoEnt>();
            }
        }

        // Consultar producto en especifico para el Actualizar
        public ProductoEnt ConsultarProducto(int q)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/ConsultarProducto?q=" + q;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<ProductoEnt>().Result;

                return null;
            }
        }

        public int AgregarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/AgregarProducto";

                // Serializar
                JsonContent body = JsonContent.Create(entidad);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                HttpResponseMessage res = client.PostAsync(url, body).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<int>().Result;

                return 0;
            }
        }


        public void ActualizarProducto(ProductoEnt entidad)
        {
            using (var client = new HttpClient())
            {

                // Serializar
                JsonContent body = JsonContent.Create(entidad);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                string url = "https://localhost:44345/api/ActualizarProducto";

                client.PutAsync(url, body).GetAwaiter().GetResult();

            }
        }

        public void EliminarProducto(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["TokenUsuario"].ToString());
                string url = "https://localhost:44345/api/EliminarProducto?id=" + id;

                client.DeleteAsync(url).GetAwaiter().GetResult();

            }
        }
    }
}
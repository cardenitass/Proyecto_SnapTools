using Newtonsoft.Json;
using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProyectowebB.Models
{
    public class RecoveryPassword
    {
        public string token { get; set; }

        public string email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        public string Password2 { get; set; }

        public RecoveryEnt GetUserByRecoveryToken(string token)
        {        
                using (var client = new HttpClient())
                {
                    string url = "https://localhost:44345/api/GetUserByRecoveryToken?token=" + token;
             
                    HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                    if (res.IsSuccessStatusCode)
                        return res.Content.ReadFromJsonAsync<RecoveryEnt>().Result;

                    return null;
                }   
        }
        public void UpdateUserPassword(string token, string password)
        {
            using (var client = new HttpClient())
            {
                var recovery = new RecoveryEnt()
                {
                    TokenRecovery = token,
                    Password = password
                };
                string url = "https://localhost:44345/api/UpdateUserPassword";
                client.PutAsJsonAsync(url, recovery).GetAwaiter().GetResult();
            }
        }

    }
}
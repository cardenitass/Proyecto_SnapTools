using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ProyectowebB.Controllers;
using ProyectowebB.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectowebB.Models
{
    public class RecoveryModel
    {

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string BuscarCorreoRecovery(string correoElectronico)
        {
            using (var client = new HttpClient())
            {
                string url = "https://localhost:44345/api/BuscarCorreoRecovery?correoElectronico=" + correoElectronico;

                HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                    return res.Content.ReadFromJsonAsync<string>().Result;

                return "ERROR";
            }
        }

        public void UpdateUserToken(string email, string token)
        {
            using (var client = new HttpClient())
            {
                var recovery = new RecoveryEnt()
                {
                    Email = email,
                    TokenRecovery = token
                };
                string url = "https://localhost:44345/api/UpdateUserToken";
                client.PutAsJsonAsync(url, recovery).GetAwaiter().GetResult();
            }
        }



        #region HELPERS

        // Metodo de Encriptacion del Token
        public string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        // Metodo para enviar correo por SMTP
        public void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = ConfigurationManager.AppSettings["DireccionCorreo"]; 
            string Contrasenna = ConfigurationManager.AppSettings["ClaveCorreo"];
            string url = ConfigurationManager.AppSettings["UrlRecovery"] + "?token=" + token;

            MailMessage vMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperación de Contraseña",
                "<p>Correo para recuperación de contraseña </p><br/>" +
                "<a href ='" + url + "'>Click para recuperar </a>"); ;

            vMailMessage.IsBodyHtml = true;
            SmtpClient vSmtpClient = new SmtpClient("smtp.office365.com");
            vSmtpClient.EnableSsl = true;
            vSmtpClient.UseDefaultCredentials = true;
            vSmtpClient.Port = 587;
            vSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contrasenna);

            vSmtpClient.Send(vMailMessage);
            vSmtpClient.Dispose();
        }

        #endregion

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Entities
{
    public class RecoveryEnt
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TokenRecovery { get; set; }
    }
}
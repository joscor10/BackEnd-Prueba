using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba_Back_End.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string RUT { get; set;}
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int TipoUsuario { get; set; }
    }
}
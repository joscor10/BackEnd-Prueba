using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba_Back_End.Models
{
    public class ViewReserva
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
    }
}
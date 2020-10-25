using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba_Back_End.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdPelicula { get; set; }
        public int Estado { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba_Back_End.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Director { get; set; }
        public int Disponible { get; set; }
    }
}
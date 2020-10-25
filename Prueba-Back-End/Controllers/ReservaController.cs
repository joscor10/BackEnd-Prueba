using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prueba_Back_End.data;
using Prueba_Back_End.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Prueba_Back_End.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReservaController : ApiController
    {
        private Contexto db = new Contexto();

        // POST: api/Reserva
        [ResponseType(typeof(Reserva))]
        public IHttpActionResult PostRreserva(Reserva reserva)
        {          
            db.Database.ExecuteSqlCommand($"proc_reservas @operacion='Reserva',@idUsuario={reserva.IdUsuario},@idPelicula={reserva.IdPelicula},@estado={reserva.Estado}");
            
            
            return Ok("Operación Exitosa");
        }

        // PUT: api/Reserva/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPelicula(int id, Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserva.Id)
            {
                return BadRequest();
            }


            try
            {
                
                db.Database.ExecuteSqlCommand($"proc_reservas @operacion='Devolver',@id={id},@estado={reserva.Estado}");
                

                return Ok("Operación Exitosa");
            }
            catch (DbUpdateConcurrencyException)
            {
                
                return StatusCode(HttpStatusCode.NoContent);

            }

           
        }
    }
}

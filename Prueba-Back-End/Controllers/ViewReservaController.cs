using Prueba_Back_End.data;
using Prueba_Back_End.Models;
using System;
using System.Collections.Generic;
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
    public class ViewReservaController : ApiController
    {
        private Contexto db = new Contexto();
        // GET: api/ViewReserva
        public IHttpActionResult GetReservas()
        {
            return Ok(db.Database.SqlQuery<ViewReserva>("proc_reservas @operacion='S'"));
        }


        // GET: api/ViewReserva/id
        [ResponseType(typeof(ViewReserva))]
        public IHttpActionResult GetReserva(int id)
        {
            return Ok(db.Database.SqlQuery<ViewReserva>($"proc_reservas @operacion='SI', @id={id}"));
        }


    }
}

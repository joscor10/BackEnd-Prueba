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
    public class LoginController : ApiController
    {
        private Contexto db = new Contexto();
        // POST: api/Login
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostLogin(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
           var datos = db.usuarios.FirstOrDefault(e => e.email == usuario.Email && e.pass==usuario.Pass);

            if (datos== null)
            {
                return Ok(false);
            }
            else {

                return Ok(datos);
            }

            

        }

       
    }
}

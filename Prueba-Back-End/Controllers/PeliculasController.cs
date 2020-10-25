using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Prueba_Back_End.Models;
using Prueba_Back_End.data;
using System.Data.SqlClient;
using System.Web.Http.Cors;

namespace Prueba_Back_End.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PeliculasController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Peliculas
        public IHttpActionResult GetPeliculas()
        {
            return Ok(db.Database.SqlQuery<Pelicula>("proc_peliculas @operacion", new SqlParameter("@operacion", "S")));
        }

        // GET: api/Peliculas/5
        [ResponseType(typeof(Pelicula))]
        public IHttpActionResult GetPelicula(int id)
        {
            SqlParameter[] parameters = new[]{
                    new SqlParameter("@operacion","SI"),
                    new SqlParameter("@id",id)
            };
            var usuario = db.Database.SqlQuery<Pelicula>("proc_peliculas @operacion, @id", parameters);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Peliculas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPelicula(int id, Pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pelicula.Id)
            {
                return BadRequest();
            }
           

            try
            {
                SqlParameter[] parameters = new[]{
                    new SqlParameter("@operacion","U"),
                    new SqlParameter("@id",id),
                    new SqlParameter("@titulo",pelicula.Titulo),
                    new SqlParameter("@descripcion",pelicula.Descripcion),
                    new SqlParameter("@director",pelicula.Director),
                    new SqlParameter("@disponible",pelicula.Disponible),
                   
                    };
                db.Database.ExecuteSqlCommand("proc_peliculas @operacion,@id,@titulo,@descripcion,@director,@disponible", parameters);
                return Ok("Exitoso");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Peliculas
        [ResponseType(typeof(Pelicula))]
        public IHttpActionResult PostPelicula(Pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SqlParameter[] parameters = new[]{
                    new SqlParameter("@operacion","I"),
                    new SqlParameter("@id",1),
                    new SqlParameter("@titulo",pelicula.Titulo),
                    new SqlParameter("@descripcion",pelicula.Descripcion),
                    new SqlParameter("@director",pelicula.Director),
                    new SqlParameter("@disponible",pelicula.Disponible),

                    };
            db.Database.ExecuteSqlCommand("proc_peliculas @operacion,@id,@titulo,@descripcion,@director,@disponible", parameters);
            return Ok("Exitoso");
        }

        // DELETE: api/Peliculas/5
        [ResponseType(typeof(Pelicula))]
        public IHttpActionResult DeletePelicula(int id)
        {
            SqlParameter[] parameters = new[]{
                    new SqlParameter("@operacion","SI"),
                    new SqlParameter("@id",id)
            };
            var pelicula = db.Database.SqlQuery<Pelicula>("proc_peliculas @operacion, @id", parameters);

            if (pelicula == null)
            {
                return NotFound();
            }

            SqlParameter[] parameters2 = new[]{
                    new SqlParameter("@operacion","D"),
                    new SqlParameter("@id",id)
            };
            db.Database.ExecuteSqlCommand("proc_peliculas @operacion, @id", parameters2);

            return Ok("Exitoso");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PeliculaExists(int id)
        {
            return db.Peliculas.Count(e => e.Id == id) > 0;
        }
    }
}
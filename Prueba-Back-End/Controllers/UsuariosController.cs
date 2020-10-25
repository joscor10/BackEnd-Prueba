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
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class UsuariosController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Usuarios
        public IHttpActionResult GetUsuarios()
        {
            return Ok(db.Database.SqlQuery<Usuario>("proc_usuarios @operacion",new SqlParameter("@operacion","S")));
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            SqlParameter[] parameters = new[]{
                    new SqlParameter("@operacion","SI"),
                    new SqlParameter("@id",id)
            };
            var usuario = db.Database.SqlQuery<Usuario>("proc_usuarios @operacion, @id", parameters);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

           

            try
            {
                SqlParameter[] parameters = new[]{
                    new SqlParameter("@operacion","U"),
                    new SqlParameter("@id",id),
                    new SqlParameter("@email",usuario.Email),
                    new SqlParameter("@pass",usuario.Pass),
                    new SqlParameter("@RUT",usuario.RUT),
                    new SqlParameter("@nombre",usuario.Nombre),
                    new SqlParameter("@direccion",usuario.Direccion),
                    new SqlParameter("@telefono",usuario.Telefono)
                    };
                db.Database.ExecuteSqlCommand("proc_usuarios @operacion,@id,@email,@pass,@RUT,@nombre,@direccion,@telefono", parameters);
                return Ok("Exitoso");
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

           
        }

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Database.ExecuteSqlCommand($"proc_usuarios @operacion='I',@RUT='{usuario.RUT}',@email='{usuario.Email}'" +
                $",@pass='{usuario.Pass}',@nombre='{usuario.Nombre}',@direccion='{usuario.Direccion}',@telefono={usuario.Telefono}");

            return Ok("Registro Exitoso");
           
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {            
            SqlParameter[] parameters = new[]{
                    new SqlParameter("@operacion","SI"),
                    new SqlParameter("@id",id)
            };
            var usuario = db.Database.SqlQuery<Usuario>("proc_usuarios @operacion, @id", parameters);
            if (usuario == null)
            {
                return NotFound();
            }

            SqlParameter[] parameters2 = new[]{
                    new SqlParameter("@operacion","D"),
                    new SqlParameter("@id",id)
            };
            db.Database.ExecuteSqlCommand("proc_usuarios @operacion, @id", parameters2);

            return Ok("Exitoso");
        }

       

        private bool UsuarioExists(int id)
        {
            return db.usuarios.Count(e => e.id == id) > 0;
        }
    }
}
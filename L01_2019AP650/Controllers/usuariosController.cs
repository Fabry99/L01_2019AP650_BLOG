using Microsoft.AspNetCore.Http;
using L01_2019AP650.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2019AP650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {

        private readonly entidadesContext _entidades;

        public usuariosController(entidadesContext entidad)
        {
            _entidades = entidad;
        }

        //obtener todos los datos

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<usuarios> listadousuarios = (from e in _entidades.usuarios
                                              select e).ToList();

            if (listadousuarios.Count() == 0)
            {
                return NotFound();

            }
            return Ok(listadousuarios);

        }
        // obtener datos por Id de usuario
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            usuarios? usuario = (from e in _entidades.usuarios
                                 where e.usuarioId == id
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);

        }

        // filtro por nombre y apellido
        [HttpGet]
        [Route("find/(filtro)")]
        public IActionResult findbynameandlastname(string filtro)
        {
            usuarios? usuario = (from e in _entidades.usuarios
                                 where e.nombre.Contains(filtro)
                                || e.apellido.Contains(filtro)
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        //filtrar por rol
        [HttpGet]
        [Route("find/{filtroRolId}")]
        public IActionResult findbyrol(int filtroRolId)
        {
            usuarios? usuario = (from e in _entidades.usuarios
                                 where e.rolId == filtroRolId
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuarios([FromBody] usuarios usuario)
        {
            try
            {
                _entidades.usuarios.Add(usuario);
                _entidades.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //actualizar usuarios por Id
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarusuario(int id, [FromBody] usuarios modificarusuario)
        {
            usuarios? usuarioActual = (from e in _entidades.usuarios
                                      where e.usuarioId == id
                                      select e).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }
            usuarioActual.rolId = modificarusuario.rolId;
            usuarioActual.nombreUsuario = modificarusuario.nombreUsuario;
            usuarioActual.clave = modificarusuario.clave;
            usuarioActual.nombre = modificarusuario.nombre;
            usuarioActual.apellido = modificarusuario.apellido;

            _entidades.Entry(usuarioActual).State = EntityState.Modified;
            _entidades.SaveChanges();

            return Ok(modificarusuario);


        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {

            usuarios? usuario = (from e in _entidades.usuarios
                               where e.usuarioId == id
                               select e).FirstOrDefault();

            if (usuario == null)
                return NotFound();

            _entidades.usuarios.Attach(usuario);
            _entidades.usuarios.Remove(usuario);
            _entidades.SaveChanges();

            return Ok(usuario);
        }

    }
}
